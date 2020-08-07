
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(MobCondition))]
public class GroundEnemyAnimator : MonoBehaviour
{
	Animator anim;
	SpriteRenderer sprite;
	MobCondition condition;
	[SerializeField] bool flipX;

	void Awake() {
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		condition = GetComponent<MobCondition>();
	}

	void OnEnable() {
		condition.spawning = true;
		State = GroundEnemyState.Spawn;
	}

	public GroundEnemyState State {
		get { return (GroundEnemyState) anim.GetInteger("state"); }
		set { anim.SetInteger("state", (int) value); }
	}


	void LateUpdate() {
		sprite.flipX = condition.faceDir > 0 ^ flipX;

		if (!condition.LockedMovement) {
			if (condition.onGround)
				State = GroundEnemyState.Run;
		}

		if (State != GroundEnemyState.Attack && (int) State < 7)
			condition.attacking = false;
		if (State != GroundEnemyState.Spawn)
			condition.spawning = false;
		if (condition.dead)
			State = GroundEnemyState.Death;
	}

	public void Reset() {
		anim.SetInteger("state", 0);
	}
}
