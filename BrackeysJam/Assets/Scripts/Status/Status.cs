
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
	public float baseHealth, baseDamage;
	public float baseSpeed, baseCritMultiplier = 2f;

	[SerializeField] float shieldRechargeRate = .1f;
	[SerializeField] float timeToShieldRecharge = 5f;

	[SerializeField] string impactTag;
	[SerializeField] bool impactFlip = false;
	[SerializeField] float knockBackForce = 0.2f;


	[HideInInspector]
	public float damage, health, maxHealth, speed, critRate, critMultiplier, dmgReduction;
	[HideInInspector]
	public float shield, maxShield;

	TurnWhiteShader shader;

	IncrementalTimers itimers;

	public virtual void Awake() {
		itimers = new IncrementalTimers();
		itimers.RegisterTimer("shieldRegen");
		shader = GetComponent<TurnWhiteShader>();
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

	public virtual void OnHit(Hurtbox hurtbox, float damage, bool crit) {
		// spawn impact particle
		if (impactTag != null && impactTag.Length > 0) {
			GameObject obj = ObjectPool.Instance.Instantiate(impactTag, hurtbox.transform.position, Quaternion.identity);
			obj.GetComponent<SpriteRenderer>().flipX = (transform.position.x > hurtbox.transform.position.x) ^ impactFlip;
		}

		hurtbox.GetComponentInParent<Movement>()?.ApplyKnockback(Vector2.right * Mathf.Sign(hurtbox.transform.position.x - transform.position.x) * knockBackForce);
	}

	public virtual void OnGettingHit(Hitbox hitbox) {
		shader?.TurnWhite(.05f);
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
