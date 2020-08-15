
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeleportingEnemyCombat : GroundEnemyCombat
{
	[SerializeField] float tpCD = 10f;
	[SerializeField] float range = 6f;

	Timers timers;

	public override void Awake() {
		base.Awake();
		timers = new Timers();
		timers.RegisterTimer("TPCD");
		timers.StartTimer("TPCD", tpCD);
	}

	public override void Update() {
		base.Update();

		if (!condition.LockedMovement) {
			if (!timers.ActiveAndNotExpired("TPCD") && condition.playerSighted && ((Vector2) (transform.position - player.transform.position)).magnitude > range) {

				condition.attacking = true;
				anim.State = GroundEnemyState.TP;

				movement?.ResetHorizontalVelocity();
				timers.StartTimer("TPCD", tpCD);
			}	
		}
	}

	public void ExecuteTeleport() {
		transform.position = player.transform.position;
	}
}
