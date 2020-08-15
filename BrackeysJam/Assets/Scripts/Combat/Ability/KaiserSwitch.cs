

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Kaiser))]
public class KaiserSwitch : Ability {

	Kaiser kaiser;

	public override void Awake() {
		base.Awake();
		kaiser = GetComponent<Kaiser>();
	}

	public override void Execute() {
		kaiser.shealth ^= true;
		Vector2 knock = knockback;
		knock.x *= condition.faceDir;
		movement.ApplyKnockback(knock);
	}
}
