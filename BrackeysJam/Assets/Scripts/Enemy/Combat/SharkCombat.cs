

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using seek
[RequireComponent(typeof(MobCondition))]
[RequireComponent(typeof(WormMovement))]
[RequireComponent(typeof(SharkAnimator))]
[RequireComponent(typeof(Status))]
public class SharkCombat : CombatManager {

	#region Components
	MobCondition condition;
	Movement movement;
	SharkAnimator anim;
	[HideInInspector]
	Status status;
	Timers timers;
	#endregion

	[SerializeField] float attackCoolDown = 2f;	
	[SerializeField] float speedMultiplierWhileAttacking = 0f;

	public override void Awake() {
		base.Awake();
		condition = GetComponent<MobCondition>();
		anim = GetComponent<SharkAnimator>();
		status = GetComponent<Status>();
		movement = GetComponent<WormMovement>();

		timers = new Timers();
		timers.RegisterTimer("attackCD");
	}

	void Update() {
		if (!condition.LockedMovement) {
			if (condition.playerSighted && !timers.ActiveAndNotExpired("attackCD")) {
				// trigger animator to attack
				anim.State = SharkState.Attack;
				condition.attacking = true;

				if (movement.velocity != Vector2.zero)
					movement.velocity = (movement.velocity).normalized * status.speed * speedMultiplierWhileAttacking;
				else
					movement.velocity = Vector2.right * condition.faceDir * status.speed * speedMultiplierWhileAttacking;

				timers.StartTimer("attackCD", attackCoolDown);
			}	
		}
	}
}