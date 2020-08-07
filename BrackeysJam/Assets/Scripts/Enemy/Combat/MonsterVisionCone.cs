

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobCondition))]
[RequireComponent(typeof(WormMovement))]
public class MonsterVisionCone : MonoBehaviour
{
	MobCondition condition;
	WormMovement movement;

	[SerializeField] float visionCone = 30f;
	[SerializeField] float visionRange = 5f;

	[SerializeField] float speedMultiplierWhileInSight = 2f;

	GameObject player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		condition = GetComponent<MobCondition>();
	}

	float RadClamp(float rad) {
		if (rad < 0) rad += 2*Mathf.PI;
		rad -= Mathf.Floor(rad / (2*Mathf.PI)) * 2 * Mathf.PI;
		return rad;
	}

	void LateUpdate() {

			Vector2 desiredVelocity = ((Vector2)(player.transform.position - transform.position)).normalized;

			float desiredOrientation = Mathf.Atan2(desiredVelocity.y, desiredVelocity.x);

			float accel = desiredOrientation - movement.orientation;

			accel = RadClamp(accel);
			if (accel >= Mathf.PI)
				accel -= 2 * Mathf.PI;

			condition.playerSighted  = accel > -visionCone / 2 * Mathf.Deg2Rad && accel < visionCone / 2 * Mathf.Deg2Rad &&
				((Vector2)(player.transform.position - transform.position)).magnitude < visionRange;
				
	}
}

