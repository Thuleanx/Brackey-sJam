

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : Status
{
	static float baseHealthGainPerLevel = .3f, baseDamageGainPerLevel = .2f;

	public float baseValue;
	public float monsterValue;

	float coefOnSpawn;

	public int Level {
		get { return Mathf.FloorToInt(LevelProgress(coefOnSpawn)); }
	}

	public static float LevelProgress(float coef) {
		return 1 + (coef - (AssistantDirector.Instance == null ? 1 : AssistantDirector.Instance.playerFactor)) * 3;
	}

	void OnEnable() {
		coefOnSpawn = (AssistantDirector.Instance == null ? 1 : AssistantDirector.Instance.masterCoef);	
		damage = baseDamage + Mathf.CeilToInt(baseDamage * (Level - 1) * baseHealthGainPerLevel);
		maxHealth = baseHealth + Mathf.CeilToInt(baseDamage * (Level - 1) * baseDamageGainPerLevel);
		health = maxHealth;
		speed = baseSpeed;
		if (monsterValue == 0)
			monsterValue = 1;
	}

	public void AssignValue(float multiplier) {
		monsterValue = baseValue * multiplier;
	}
}
