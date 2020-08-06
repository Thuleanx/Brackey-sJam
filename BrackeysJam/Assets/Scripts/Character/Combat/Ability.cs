

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public abstract class Ability : MonoBehaviour {

	public PlayerState attackState;	
	public float cooldown;

	public bool commital;

	[SerializeField]
	bool alt = false;
	public PlayerState originalState;

	public virtual void Start() {
		GetComponent<PlayerCombat>().RegisterAttack(this);		
		if (alt) GetComponent<PlayerCombat>().RegisterAltAttack(this);		
	}

	public abstract void Execute();
}