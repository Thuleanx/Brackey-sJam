
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public class ChestManager : MonoBehaviour
{
	Animator anim;

	[HideInInspector]
	public bool isOpen;

	void Awake() {
		anim = GetComponent<Animator>();
	}

	void OnEnable() {
		isOpen = false;
		anim.SetInteger("state", (int) ChestStates.Close);
	}

	public void Open() {
		if (!isOpen) {
			isOpen = true;
			anim.SetInteger("state", (int) ChestStates.Open);
		}
	}
}
