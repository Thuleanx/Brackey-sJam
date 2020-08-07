

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStatus))]
[RequireComponent(typeof(MobCondition))]
public class WormMovement : MonoBehaviour
{
	EnemyStatus status;
	MobCondition condition;

	[HideInInspector]
	public Vector2 velocity;

	[HideInInspector]
	public float orientation = 0;

	[SerializeField] float angularAccelerationPerSecond = 30f;

	[SerializeField] float visionCone = 30f;
	[SerializeField] float visionRange = 5f;

	[SerializeField] float speedMultiplierWhileInSight = 2f;

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
		if (!condition.LockedMovement) {
			float speed = status.speed;

			Vector2 desiredVelocity = speed * ((Vector2)(player.transform.position - transform.position)).normalized;

			float desiredOrientation = Mathf.Atan2(desiredVelocity.y, desiredVelocity.x);

			float accel = desiredOrientation - orientation;

			accel = RadClamp(accel);

			if (accel >= Mathf.PI)
				accel -= 2 * Mathf.PI;

			bool inVision = accel > -visionCone / 2 * Mathf.Deg2Rad && accel < visionCone / 2 * Mathf.Deg2Rad &&
				((Vector2)(player.transform.position - transform.position)).magnitude < visionRange;

			float maxAngularAccel = (Time.deltaTime * angularAccelerationPerSecond * Mathf.Deg2Rad);
			accel = Mathf.Clamp(accel, -maxAngularAccel, maxAngularAccel);

			orientation += accel;
			orientation = RadClamp(orientation);

			velocity = new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation)) * speed;

			if (inVision)
			{
				velocity *= speedMultiplierWhileInSight;
			}

		}
		transform.Translate(velocity * Time.deltaTime);
	}

	void OnGizmosDraw() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(transform.position, transform.position + (Vector3) velocity);
	}
}

