

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(PlayerItemHandler))]
public class PlayerStatus : Status
{

	static float baseHealthGainPerLevel = .3f, baseDamageGainPerLevel = .2f, healthRegenPerLevel = .2f;

	[SerializeField] float shakeOnHit = .2f;
	[SerializeField] string playerAnchorTag = "Anchor";
	[SerializeField] string popUpTag = "Message: Damage Popup";

	[SerializeField] float baseHealthRegen;

	public float healthRegen;

	public int Level = 1;
	public float XP;

	PlayerItemHandler handler;

	public override void Awake() {
		base.Awake();
		handler = GetComponent<PlayerItemHandler>();
	}

	void OnEnable() {
		damage = baseDamage + Mathf.FloorToInt(baseDamage * (Level - 1) * baseHealthGainPerLevel);
		maxHealth = baseHealth + Mathf.CeilToInt(baseDamage * (Level - 1) * baseDamageGainPerLevel);
		health = maxHealth;
		speed = baseSpeed;
		healthRegen = baseHealthRegen;
	}

	float GetItemEffect(Item item) {
		return ItemEffect.GetItemEffect(item, handler.GetStacks(item));
	}

	// on hit effects
	public override void OnHit(Hurtbox hurtbox, float damage, bool crit) {
		base.OnHit(hurtbox, damage, crit);

		health += GetItemEffect(Item.Thirst);
		SleepManager.Sleep(.02f, this);

		GameObject obj = ObjectPool.Instance.Instantiate(popUpTag, hurtbox.transform.parent.transform.position, Quaternion.identity);
		obj.GetComponent<DamagePopup>().SetUp(Mathf.FloorToInt(damage), crit);
	}	

	public override void OnGettingHit(Hitbox hitbox) {
		base.Awake();
		CameraShake.Instance.IncreaseTrauma(shakeOnHit);
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
			health = maxHealth;
		}
	}
}
