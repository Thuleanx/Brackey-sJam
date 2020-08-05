

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(MobCondition))]
public class FlyingEnemyAnimator : MonoBehaviour
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
		State = FlyingEnemyState.Spawn;
	}

	public FlyingEnemyState State {
		get { return (FlyingEnemyState) anim.GetInteger("state"); }
		set { anim.SetInteger("state", (int) value); }
	}

	void LateUpdate() {
		sprite.flipX = (condition.faceDir > 0) ^ flipX;

		if (!condition.LockedMovement) {
			State = FlyingEnemyState.Idle;
		}

		if (State != FlyingEnemyState.Attack)
			condition.attacking = false;

		if (State != FlyingEnemyState.Spawn)
			condition.spawning = false;
	}

	public void Reset() {
		anim.SetInteger("state", 0);
	}
}
