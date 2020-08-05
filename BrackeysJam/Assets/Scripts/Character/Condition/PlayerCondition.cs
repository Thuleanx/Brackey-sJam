

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : Condition
{
	[HideInInspector]
	public bool dashing, wallJumping, wallLeaping, attacking, down;

	[HideInInspector]
	public Timers timers;

	[HideInInspector]
	public IncrementalTimers itimers;

	void Awake() {
		InitTimers();
	}

	void OnEnable() {
		down = false;
	}

	void InitTimers() {
		timers = new Timers();
		itimers = new IncrementalTimers();

		timers.RegisterTimer("coyoteBuffer");
		timers.RegisterTimer("platformFallThrough");
		timers.RegisterTimer("controlTime");
		timers.RegisterTimer("wallJumpBuffer");
		timers.RegisterTimer("wallJumpToLeap");
		itimers.RegisterTimer("wallGlideHang");
	}

	void LateUpdate() {
		if (!LockedMovement)
			InputManager.Instance.DecrementTimers();
	}

	public bool LockedMovement {
		get { return attacking || timers.ActiveAndNotExpired("controlTime") || down; }
	}
}
