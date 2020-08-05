

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using seek
[RequireComponent(typeof(MobCondition))]
[RequireComponent(typeof(Status))]
public class FlyingMovement : MonoBehaviour {

	Status status;
	MobCondition condition;	
	GameObject player;

	[SerializeField] float maxAcceleration;
	[SerializeField] float enrageRange = 3;
	[SerializeField] float enrageSpeedMultiplier = 2f;

	[HideInInspector]
	public Vector2 velocity;

	void Awake() {
		condition = GetComponent<MobCondition>();
		status = GetComponent<Status>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void OnEnable() {
		ResetVelocity();
	}

	void Update() {

		if (!condition.LockedMovement) {
			float speed = status.speed * (condition.playerSighted ?  enrageSpeedMultiplier : 1);
			Vector2 desiredVelocity = speed * ((Vector2) (player.transform.position - transform.position)).normalized;
			Vector2 steer = Vector2.ClampMagnitude(desiredVelocity - velocity, maxAcceleration * Time.deltaTime);

			velocity += steer;
			velocity = Vector2.ClampMagnitude(velocity, speed);
		}

		transform.Translate(velocity * Time.deltaTime);
	}

	void LateUpdate() {
		condition.faceDir = Mathf.Sign(velocity.x);
		condition.playerSighted = ((Vector2) (transform.position - player.transform.position)).sqrMagnitude <= enrageRange * enrageRange;
	}

	void OnGizmosDraw() {
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(transform.position, enrageRange);
	}

	public void ResetVelocity() {
		velocity = Vector2.zero;
	}
}