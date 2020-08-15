

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public abstract class Ability : MonoBehaviour {

	public PlayerState attackState;	
	public float cooldown;
	public bool iframe, cooldownRefresh;
	public bool commital;

	public Vector2 knockback;

	[SerializeField]
	bool alt = false;
	public PlayerState originalState;

	[HideInInspector]
	public MovementController movement;
	[HideInInspector]
	public PlayerCondition condition;

	public virtual void Awake() {
		movement = GetComponent<MovementController>();
		condition = GetComponent<PlayerCondition>();
	}

	public virtual void Start() {
		GetComponent<PlayerCombat>().RegisterAttack(this);		
		if (alt) GetComponent<PlayerCombat>().RegisterAltAttack(this);		
	}

	public abstract void Execute();
}