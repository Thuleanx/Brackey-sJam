

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status
{
	static float baseHealthGainPerLevel = .3f, baseDamageGainPerLevel = .2f;

	[SerializeField] float healthRegen;

	public int Level = 1;

	void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	void OnEnable() {
		damage = baseDamage + Mathf.FloorToInt(baseDamage * (Level - 1) * baseHealthGainPerLevel);
		maxHealth = baseHealth + Mathf.CeilToInt(baseDamage * (Level - 1) * baseDamageGainPerLevel);
		health = maxHealth;
	}

	void LateUpdate() {
		damage = Mathf.FloorToInt(baseDamage * (Level - 1) * baseHealthGainPerLevel);
		health += healthRegen * Time.deltaTime;
		speed = baseSpeed;
	}
}
