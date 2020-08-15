using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualForwardFocus : CameraController
{
	[SerializeField, Range(0f, .5f)] float leftOffset;
	[SerializeField, Range(0f, .5f)] float rightOffset;

	[SerializeField] float deadZoneTime;
	[SerializeField] float focusRate;
	float currentXOffset;
	float desiredOffset;

	Movement movement;
	Condition condition;

	Timers timers;

	public override void Awake() {
		base.Awake();
		timers = new Timers();
		timers.RegisterTimer("offsetChange");
		timers.StartTimer("offsetChange", deadZoneTime);
		movement = followPosition.GetComponent<Movement>();
		condition = followPosition.GetComponent<Condition>();
		currentXOffset = 0;
	}

	void Update() {
		SeekFollowPosition();

		if (desiredOffset == Mathf.Sign(condition.faceDir))
			timers.StartTimer("offsetChange", deadZoneTime);
		else if (timers.Expired("offsetChange")) {
			desiredOffset = Mathf.Sign(condition.faceDir);
			timers.StartTimer("offsetChange", deadZoneTime);
		}

		if (movement.velocity.x != 0) {
			// adjust camera	
			currentXOffset = Calculate.AsympEase(currentXOffset, desiredOffset > 0 ? rightOffset : -leftOffset, focusRate);
		}


		transform.position = new Vector3(
			followPosition.position.x + currentXOffset * camWidth,
			transform.position.y,
			transform.position.z
		);
	}

	void OnDrawGizmos() {
		UpdateSize();

		Gizmos.color = Color.red;
		Gizmos.DrawLine(
			transform.position - Vector3.down * camHeight * .5f + Vector3.left * leftOffset * camWidth, 
			transform.position + Vector3.up * camHeight * .5f + Vector3.left * leftOffset * camWidth
		);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(
			transform.position - Vector3.down * camHeight * .5f + Vector3.right * leftOffset * camWidth, 
			transform.position + Vector3.up * camHeight * .5f + Vector3.right * leftOffset * camWidth
		);
	}
}
