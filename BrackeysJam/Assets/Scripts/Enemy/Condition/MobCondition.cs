


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCondition : Condition
{
	[HideInInspector]
	public bool playerSighted;

	[HideInInspector]
	public bool dashing, wallJumping, wallLeaping, attacking;

	[HideInInspector]
	public Timers timers;
	[HideInInspector]
	public IncrementalTimers itimers;

	void Awake() {
		InitTimers();
	}

	void InitTimers() {
		timers = new Timers();
		itimers = new IncrementalTimers();

		timers.RegisterTimer("TurnCD");
	}

	void LateUpdate() {
	}

	public bool LockedMovement {
		get { return attacking; }
	}
}
