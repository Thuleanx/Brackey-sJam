

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerItemHandler))]
public class PlayerStatus : Status
{
	static float baseHealthGainPerLevel = .3f, baseDamageGainPerLevel = .2f, healthRegenPerLevel = .2f;

	[SerializeField] float baseHealthRegen;

	public float healthRegen;

	public int Level = 1;
	public float XP;

	PlayerItemHandler handler;

	public override void Awake() {
		base.Awake();
		handler = GetComponent<PlayerItemHandler>();
		DontDestroyOnLoad(gameObject);
	}

	void OnEnable() {
		damage = baseDamage + Mathf.FloorToInt(baseDamage * (Level - 1) * baseHealthGainPerLevel);
		maxHealth = baseHealth + Mathf.CeilToInt(baseDamage * (Level - 1) * baseDamageGainPerLevel);
		health = maxHealth;
	}

	float GetItemEffect(Item item) {
		return ItemEffect.GetItemEffect(item, handler.GetStacks(item));
	}

	// on hit effects
	public override void OnHit(Hurtbox hurtbox) {
		base.OnHit(hurtbox);
		health += GetItemEffect(Item.Thirst);
	}	

	public override void LateUpdate() {
		base.LateUpdate();

		// Damage Calculations
		damage = Mathf.FloorToInt(baseDamage * (1 +  (Level - 1) * baseHealthGainPerLevel)) * 
			(1 + GetItemEffect(Item.Menace));
		critRate = 1 - GetItemEffect(Item.Assassination);

		maxHealth = baseHealth + Mathf.CeilToInt(baseDamage * (Level - 1) * baseDamageGainPerLevel) + GetItemEffect(Item.Constitution);
		
		// low health
		if (Health < .2 * maxHealth)
			damage = damage * (1 + GetItemEffect(Item.PainAttunement));
		
		healthRegen = baseHealthRegen + (Level - 1) * healthRegenPerLevel + GetItemEffect(Item.GolemHeart);

		Health += healthRegen * Time.deltaTime;

		speed = baseSpeed * (1 + GetItemEffect(Item.Hooves));

		critMultiplier = baseCritMultiplier;

		dmgReduction = GetItemEffect(Item.Teddy);

		maxShield = GetItemEffect(Item.ArcaneShield) * maxHealth;
	}

	public float XPFormula(int level) {
		return 20f * Mathf.Pow(1.55f, level - 2);
	}

	public float XPTotFormula(int level) {
		return 20f * (1 - Mathf.Pow(1.55f, level - 1)) / (1 - 1.55f);
	}

	public void AcquireXP(float amt) {
		XP += amt;
		while (XPTotFormula(Level + 1) <= XP) {
			// level up here
			Level++;
		}
	}
}
