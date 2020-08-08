
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerCondition))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerCombat : CombatManager {

	static PlayerState[] atkStates = {PlayerState.S4, PlayerState.S3, PlayerState.S2, PlayerState.S1};

	#region Components
	PlayerCondition condition;
	PlayerAnimationController anim;
	#endregion

	Ability[] abilities;
	PlayerState[] alt;

	IncrementalTimers itimers;

	[SerializeField] float altAttackBufferTimeSeconds = 1f;

	public override void Awake() {
		base.Awake();
		int enumLength = Enum.GetNames(typeof(PlayerState)).Length;
		abilities = new Ability[enumLength];
		alt = new PlayerState[enumLength];
		anim = GetComponent<PlayerAnimationController>();
		condition = GetComponent<PlayerCondition>();

		InitTimers();
	}

	void InitTimers() {
		itimers = new IncrementalTimers();

		itimers.RegisterTimer("S1");
		itimers.RegisterTimer("S2");
		itimers.RegisterTimer("S3");
		itimers.RegisterTimer("S4");

		itimers.RegisterTimer("S1Alt");
		itimers.RegisterTimer("S2Alt");
		itimers.RegisterTimer("S3Alt");
		itimers.RegisterTimer("S4Alt");
	}

	public void RegisterAttack(Ability ability) {
		abilities[(int) ability.attackState] = ability;
	}	

	public void RegisterAltAttack(Ability ability) {
		// abilities[(int) ability.attackState] = ability;
		alt[(int) ability.originalState] = ability.attackState;
	}

	public float GetCoolDown(PlayerState state) {
		string name = Enum.GetName(typeof(PlayerState), state);
		return itimers.TimeLeft(name);
	}

	public float GetTotalCoolDown(PlayerState state) {
		return abilities[(int) state].cooldown;
	}

	void Update() {
		foreach (var attack in atkStates) if (!condition.LockedAttack) {
			string name = Enum.GetName(typeof(PlayerState), attack);

			Ability ability = abilities[(int) attack];

			if (itimers.Expired(name) && InputManager.Instance.timers.ActiveAndNotExpired(name + "Buffer")) {
				condition.attacking = true;
				
				bool refreshCD = false;
				if (alt[(int) attack] != 0 && !itimers.Expired(name + "Alt")) {
					// trigger alt attack	
					int rattack = (int) alt[(int) attack];

					abilities[rattack].Execute();
					anim.State = (PlayerState) rattack;

					if (abilities[(int) rattack].commital)
						condition.lockAttacking = true;
					if (abilities[(int) rattack].iframe)
						condition.immune = true;
					refreshCD = abilities[rattack].cooldownRefresh;

					itimers.Exhaust(name + "Alt");
				} else {
					// trigger attack
					abilities[(int) attack].Execute();
					anim.State = attack;

					if (abilities[(int) attack].commital)
						condition.lockAttacking = true;

					if (abilities[(int) attack].iframe)
						condition.immune = true;

					refreshCD = abilities[(int) attack].cooldownRefresh;
					

					itimers.StartTimer(name + "Alt", altAttackBufferTimeSeconds);
				}


				itimers.Exhaust(name);
				InputManager.Instance.timers.SetActive(name + "Buffer", false);

				if (refreshCD) itimers.ExhaustAll();

				if (ability.cooldown > 0)
					itimers.StartTimer(name, ability.cooldown);
			}
		}	
	}

	void LateUpdate() {
		itimers.Increment("S1", Time.deltaTime);
		itimers.Increment("S2", Time.deltaTime);
		itimers.Increment("S3", Time.deltaTime);
		itimers.Increment("S4", Time.deltaTime);

		itimers.Increment("S1Alt", Time.deltaTime);
		itimers.Increment("S2Alt", Time.deltaTime);
		itimers.Increment("S3Alt", Time.deltaTime);
		itimers.Increment("S4Alt", Time.deltaTime);
	}
}