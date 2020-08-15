


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(MobCondition))]
public class SharkAnimator : MonoBehaviour
{
	Animator anim;
	MobCondition condition;

	void Awake() {
		anim = GetComponent<Animator>();
		condition = GetComponent<MobCondition>();
	}

	void OnEnable() {
	}

	public SharkState State {
		get { return (SharkState) anim.GetInteger("state"); }
		set { anim.SetInteger("state", (int) value); }
	}

	void LateUpdate() {
		if (!condition.LockedMovement)
			State = SharkState.Idle;

		if (State != SharkState.Attack)
			condition.attacking = false;
	}

	public void Reset() {
		anim.SetInteger("state", 0);
	}
}
