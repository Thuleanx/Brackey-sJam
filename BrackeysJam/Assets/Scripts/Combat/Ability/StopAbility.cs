

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class StopAbility : Ability {
	public override void Execute() {
		movement.ResetHorizontalVelocity();
		Vector2 knock = knockback;
		knock.x *= condition.faceDir;
		movement.ApplyKnockback(knock);
	}
}