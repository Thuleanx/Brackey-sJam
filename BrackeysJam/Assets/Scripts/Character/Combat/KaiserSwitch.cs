

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Kaiser))]
public class KaiserSwitch : Ability {

	Kaiser kaiser;

	void Awake() {
		kaiser = GetComponent<Kaiser>();
	}

	public override void Execute() {
		kaiser.shealth ^= true;
	}
}
