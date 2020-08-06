

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class StopAbility : Ability {
	MovementController movement;

	void Awake() {
		movement = GetComponent<MovementController>();
	}

	public override void Execute() {
		movement.ResetHorizontalVelocity();
	}
}