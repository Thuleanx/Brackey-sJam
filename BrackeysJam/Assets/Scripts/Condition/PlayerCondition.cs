

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : Condition
{
	[HideInInspector]
	public bool dashing, wallJumping, wallLeaping, attacking, lockAttacking, lockVelocityDuringAttack, jumping;

	[HideInInspector]
	public Timers timers;

	[HideInInspector]
	public IncrementalTimers itimers;


	void Awake() {
		InitTimers();
	}

	void OnEnable() {
		dashing = wallJumping = wallLeaping = attacking = lockAttacking = lockVelocityDuringAttack = false;
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
		if (!LockedMovement) {
			InputManager.Instance.DecrementTimers();
		}
	}

	public bool LockedVelocity {
		get { return lockVelocityDuringAttack || timers.ActiveAndNotExpired("controlTime"); }
	}

	public bool LockedMovement {
		get { return lockAttacking || timers.ActiveAndNotExpired("controlTime"); }
	}

	public bool LockedAttack {
		get { return attacking || timers.ActiveAndNotExpired("controlTime"); }
	}
}
