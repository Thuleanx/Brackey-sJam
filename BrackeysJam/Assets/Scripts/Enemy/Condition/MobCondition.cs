


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCondition : Condition
{
	[HideInInspector]
	public bool playerSighted;

	[HideInInspector]
	public bool attacking, spawning;

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

	void OnEnable() {
		playerSighted = attacking = onGround = onWall = dead = false;
		spawning = true;
	}

	void LateUpdate() {
	}

	public bool LockedMovement {
		get { return attacking || spawning || dead; }
	}
}
