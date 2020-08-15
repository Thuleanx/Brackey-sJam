

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(MobCondition))]
public class WormMovement : Movement
{
	EnemyStatus status;
	MobCondition condition;

	[HideInInspector]
	public float orientation = 0;

	[SerializeField] float angularAccelerationPerSecond = 30f;

	GameObject player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
		status = GetComponent<EnemyStatus>();
		condition = GetComponent<MobCondition>();
	}

	float RadClamp(float rad) {
		if (rad < 0) rad += 2*Mathf.PI;
		rad -= Mathf.Floor(rad / (2*Mathf.PI)) * 2 * Mathf.PI;
		return rad;
	}

	void Update() {
		condition.spawning = false;

		if (!condition.LockedMovement) {
			float speed = status.speed;

			Vector2 desiredVelocity = speed * ((Vector2)(player.transform.position - transform.position)).normalized;

			float desiredOrientation = Mathf.Atan2(desiredVelocity.y, desiredVelocity.x);

			float accel = desiredOrientation - orientation;

			accel = RadClamp(accel);

			if (accel >= Mathf.PI)
				accel -= 2 * Mathf.PI;

			float maxAngularAccel = (Time.deltaTime * angularAccelerationPerSecond * Mathf.Deg2Rad);
			accel = Mathf.Clamp(accel, -maxAngularAccel, maxAngularAccel);

			orientation += accel;
			orientation = RadClamp(orientation);

			velocity = new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation)) * speed;
		}

		condition.faceDir = Mathf.Sign(velocity.x);
		if (condition.dead) velocity = Vector2.zero;

		transform.Translate(velocity * Time.deltaTime, Space.World);
	}

	public override void ApplyKnockback(Vector2 displacement) {
		transform.Translate(displacement);
	}

	void OnGizmosDraw() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(transform.position, transform.position + (Vector3) velocity);
	}
}

