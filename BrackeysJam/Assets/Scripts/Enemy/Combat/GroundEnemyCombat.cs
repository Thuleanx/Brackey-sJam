using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MobCondition), typeof(GroundEnemyAnimator))]
public class GroundEnemyCombat : CombatManager
{
	// raycast for hit detection

	#region Components

	protected GroundMovement movement;
	protected MobCondition condition;
	protected GameObject player;
	protected GroundEnemyAnimator anim;

	Timers timers;

	#endregion

	[SerializeField] LayerMask walls, friendly;

	[SerializeField] float attackCoolDown = 2f;
	[SerializeField] Vector2 attackRayStart = Vector2.zero;
	[SerializeField] float attackRange = 3f;

	public override void Awake() {
		condition = GetComponent<MobCondition>();
		player = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<GroundEnemyAnimator>();
		movement = GetComponent<GroundMovement>();

		timers = new Timers();
		timers.RegisterTimer("attackCD");
	}

	public virtual void Update() {
		if (!condition.LockedMovement) {
			if (!timers.ActiveAndNotExpired("attackCD")) {
				Vector2 rayStart = attackRayStart;
				if (condition.faceDir < 0)
					rayStart.x *= -1;
				rayStart += (Vector2) transform.position;

				RaycastHit2D hitWall = Physics2D.Raycast(rayStart, Vector2.right * condition.faceDir, attackRange, walls);
				RaycastHit2D hitPlayer = Physics2D.Raycast(rayStart, Vector2.right * condition.faceDir, attackRange, friendly);

				if ((hitPlayer && !hitWall) || (hitPlayer && hitWall.distance > hitPlayer.distance)) {
					// trigger attack
					condition.attacking = true;
					anim.State = GroundEnemyState.Attack;

					movement?.ResetHorizontalVelocity();
					timers.StartTimer("attackCD", attackCoolDown);
				}
			}
		}
	}

	void OnDrawGizmos() {
		Vector2 rayStart = attackRayStart;
		if (condition != null && condition.faceDir < 0)
			rayStart.x *= -1;
		rayStart += (Vector2)transform.position;

		Gizmos.color = Color.red;
		Gizmos.DrawLine(rayStart, rayStart + Vector2.right * (condition != null ? condition.faceDir : 1) * attackRange);
	}
}
