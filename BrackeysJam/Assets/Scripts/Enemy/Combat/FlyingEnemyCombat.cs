
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Using seek
[RequireComponent(typeof(MobCondition))]
[RequireComponent(typeof(FlyingMovement))]
[RequireComponent(typeof(FlyingEnemyAnimator))]
[RequireComponent(typeof(Status))]
public class FlyingEnemyCombat : CombatManager {

	#region Components
	MobCondition condition;
	FlyingMovement movement;
	FlyingEnemyAnimator anim;
	Status status;

	Timers timers;
	#endregion

	[SerializeField] float attackCoolDown = 2f;	
	[SerializeField] float speedMultiplierWhileAttacking = 2f;

	void Awake() {
		condition = GetComponent<MobCondition>();
		movement = GetComponent<FlyingMovement>();
		anim = GetComponent<FlyingEnemyAnimator>();
		status = GetComponent<Status>();

		timers = new Timers();
		timers.RegisterTimer("attackCD");
	}

	void Update() {
		if (!condition.LockedMovement) {
			if (condition.playerSighted && !timers.ActiveAndNotExpired("attackCD")) {
				// trigger animator to attack
				anim.State = FlyingEnemyState.Attack;
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