using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	// follow player by default. leave none to follow player
	[SerializeField] float offsetY;

	Transform followPosition; // player
	
	float camHeight, camWidth;

	void Start() {
		if (followPosition == null) 
			SeekFollowPosition();

		camHeight = 2 * Camera.main.orthographicSize;
		camWidth = camHeight * Camera.main.aspect;
	}

	void SeekFollowPosition() {
		followPosition = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		SeekFollowPosition();
		Vector2 target = followPosition.position + Vector3.up * offsetY * camHeight;
		Vector2 pos = transform.position;

		if (target.y < pos.y)
			pos.y = target.y;

		// keep in frame

		transform.position = new Vector3(
			Calculate.AsympEase(pos.x, target.x, .1f),
			Calculate.AsympEase(pos.y, target.y, .02f),
			transform.position.z
		);
	}
}
