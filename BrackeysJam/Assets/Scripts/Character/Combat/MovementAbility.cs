


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(PlayerCondition))]
public class MovementAbility : Ability {

	MovementController movement;
	PlayerCondition condition;

	[SerializeField] float dashDuration = .2f;
	[SerializeField] Vector2 displacement;

	float horizontalVelocity, verticalVelocity;

	void Awake() {
		movement = GetComponent<MovementController>();
		condition = GetComponent<PlayerCondition>();


	}

	public override void Start() {
		base.Start();
		horizontalVelocity = displacement.x / dashDuration;
		verticalVelocity = Mathf.Sqrt(displacement.y * movement.gravity);
	}

	public override void Execute() {
		movement.velocity = new Vector2(
			horizontalVelocity, verticalVelocity
		);
		movement.velocity.x *= condition.faceDir;
		condition.onGround = false;
		condition.timers.StartTimer("controlTime", dashDuration);
	}
}
