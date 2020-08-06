

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCondition))]
[RequireComponent(typeof(MovementController))]
public class LockMovementAbility : Ability {

	PlayerCondition condition;
	MovementController movement;

	void Awake() {
		condition = GetComponent<PlayerCondition>();
		movement = GetComponent<MovementController>();
	}

	public override void Execute() {
		condition.lockVelocityDuringAttack = true;
		movement.velocity = Vector2.zero;
	}
}