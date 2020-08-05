

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
	static float baseHealthGainPerLevel = .3f, baseDamageGainPerLevel = .2f;

	float coefOnSpawn;

	public int Level {
		get { return Mathf.FloorToInt(1 + (coefOnSpawn - 1) * 3); }
	}

	void OnEnable() {
		coefOnSpawn = (Director.Instance == null ? 1 : Director.Instance.masterCoef);	

		damage = baseDamage + Mathf.CeilToInt(baseDamage * (Level - 1) * baseHealthGainPerLevel);
		maxHealth = baseHealth + Mathf.CeilToInt(baseDamage * (Level - 1) * baseDamageGainPerLevel);
		health = maxHealth;

		speed = baseSpeed;
	}
}
