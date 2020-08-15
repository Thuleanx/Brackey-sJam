

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationAbility : Ability {
	public override void Execute() {
		// do nothing oof
		Vector2 knock = knockback;
		knock.x *= condition.faceDir;
		movement.ApplyKnockback(knock);
	}
}