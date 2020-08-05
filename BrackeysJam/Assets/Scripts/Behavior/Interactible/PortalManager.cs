
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PortalManager : MonoBehaviour
{
	Animator anim;

	int lifeCycle; // 0 if spawning, 1 if idle, 2 if despawned	

	void Awake() {
		anim = GetComponent<Animator>();
	}

	void OnEnable() {
		Close();
	}

	public void Activate() {
		if (lifeCycle != 0) {
			lifeCycle = 0;
			anim.SetInteger("state", (int)PortalStates.Spawn);
		}
	}

	public void Reset() {
		lifeCycle = 1;
		anim.SetInteger("state", (int) PortalStates.Idle);
	}

	public void Close() {
		lifeCycle = 2;
		anim.SetInteger("state", (int) PortalStates.Despawn);
	}
}
