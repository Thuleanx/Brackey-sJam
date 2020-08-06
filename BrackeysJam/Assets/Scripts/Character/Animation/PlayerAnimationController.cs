

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerCondition))]
public class PlayerAnimationController : MonoBehaviour
{
	Animator anim;
	SpriteRenderer sprite;
	PlayerCondition condition;

	void Awake() {
		anim = GetComponent<Animator>();
		sprite = GetComponent<SpriteRenderer>();
		condition = GetComponent<PlayerCondition>();
	}

	PlayerState cache;

	public PlayerState State {
		get { return cache; }
		set { cache = value; }
		// get { return (PlayerState) anim.GetInteger("state"); }
		// set { 
		// 	anim.SetInteger("state", (int) value);
		// }
	}

	void LateUpdate() {
		sprite.flipX = condition.faceDir < 0;

		if (State != PlayerState.S1 && State != PlayerState.S2 && State != PlayerState.S3 
			&& State != PlayerState.S4 && State != PlayerState.S1Alt) {
			
			condition.attacking = false;
			condition.lockAttacking = false;
			condition.lockVelocityDuringAttack = false;
		}
		UpdateCache();
	}

	public bool LockedAnimation() {
		return  State == PlayerState.S1 ||
				State == PlayerState.S2 ||
				State == PlayerState.S3 ||
				State == PlayerState.S4 ||
				State == PlayerState.S1Alt;
	}

	void UpdateCache() {
		anim.SetInteger("state", (int) cache);
	}

	public void Reset() {
		anim?.SetInteger("state", 0);
		cache = 0;
	}
}
