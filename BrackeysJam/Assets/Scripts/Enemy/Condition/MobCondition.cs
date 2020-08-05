


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCondition : Condition
{
	[HideInInspector]
	public bool playerSighted;

	[HideInInspector]
	public bool dashing, wallJumping, wallLeaping, attacking, spawning;

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

		timers.RegisterTimer("turnCD");
		timers.RegisterTimer("jumpCD");
	}

	void LateUpdate() {
	}

	public bool LockedMovement {
		get { return attacking || spawning || dead; }
	}
}
