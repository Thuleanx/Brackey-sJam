
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
	public float baseHealth, baseDamage;
	public float baseSpeed, baseCritMultiplier = 2f;

	[SerializeField] float shieldRechargeRate = .1f;
	[SerializeField] float timeToShieldRecharge = 5f;

	[HideInInspector]
	public float damage, health, maxHealth, speed, critRate, critMultiplier, dmgReduction;
	[HideInInspector]
	public float shield, maxShield;

	IncrementalTimers itimers;

	public virtual void Awake() {
		itimers = new IncrementalTimers();
		itimers.RegisterTimer("shieldRegen");
	}

	public float Health {
		get { return health; }
		set { 
			health = Mathf.Clamp(value, 0, maxHealth); 
			
		}
	}

	public void CommitDie() {
		gameObject.SetActive(false);
	}

	public virtual void DealDamage(float damage) {
		// dreduction
		damage *= 1 - dmgReduction;

		// shield
		if (shield > damage)
			shield -= damage;
		else {
			damage -= shield;
			shield = 0;
		}

		// health
		Health -= damage;
	}

	public virtual void OnHit(Hurtbox hurtbox) {
	}

	public virtual void OnGettingHit(Hitbox hitbox) {
		itimers.StartTimer("shieldRegen", timeToShieldRecharge);
	}

	public virtual void LateUpdate() {
		if (itimers.Expired("shieldRegen")) {
			shield += maxShield * shieldRechargeRate * Time.deltaTime;
			shield = Mathf.Clamp(shield, 0f, maxShield);
		}
		itimers.Increment("shieldRegen", Time.deltaTime);
	}
}
