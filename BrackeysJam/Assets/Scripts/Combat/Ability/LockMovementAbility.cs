

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCondition))]
[RequireComponent(typeof(MovementController))]
public class LockMovementAbility : Ability {
	public override void Execute() {
		condition.lockVelocityDuringAttack = true;
		movement.velocity = Vector2.zero;

		Vector2 knock = knockback;
		knock.x *= condition.faceDir;
		movement.ApplyKnockback(knock);
	}
}