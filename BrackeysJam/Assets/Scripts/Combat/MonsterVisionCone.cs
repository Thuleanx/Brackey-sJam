

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobCondition))]
[RequireComponent(typeof(Movement))]
public class MonsterVisionCone : MonoBehaviour
{
	MobCondition condition;
	Movement movement;

	[SerializeField] float visionCone = 30f;
	[SerializeField] float visionRange = 5f;
	[SerializeField] float memorySeconds = 2f;

	GameObject player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		condition = GetComponent<MobCondition>();
		movement = GetComponent<Movement>();
	}

	float RadClamp(float rad) {
		if (rad < 0) rad += 2*Mathf.PI;
		rad -= Mathf.Floor(rad / (2*Mathf.PI)) * 2 * Mathf.PI;
		return rad;
	}

	void LateUpdate() {
		Vector2 desiredVelocity = ((Vector2)(player.transform.position - transform.position)).normalized;

		float desiredOrientation = Mathf.Atan2(desiredVelocity.y, desiredVelocity.x);
		float orientation = Mathf.Atan2(movement.velocity.y, movement.velocity.x);

		float accel = desiredOrientation - orientation;

		accel = RadClamp(accel);
		if (accel >= Mathf.PI)
			accel -= 2 * Mathf.PI;


		condition.playerSighted = accel > -visionCone / 2 * Mathf.Deg2Rad && accel < visionCone / 2 * Mathf.Deg2Rad &&
			((Vector2)(player.transform.position - transform.position)).magnitude < visionRange;
		if (condition.playerSighted) {
			condition.timers.StartTimer("playerSighted", memorySeconds);
		}

	}
}

