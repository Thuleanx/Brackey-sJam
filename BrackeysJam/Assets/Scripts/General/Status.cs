
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
	public int baseHealth, baseDamage;
	public float baseSpeed;

	[HideInInspector]
	public float damage, health, maxHealth, speed;

	public float Health {
		get { return health; }
		set { 
			health = Mathf.Clamp(value, 0, maxHealth); 
		}
	}

	public void CommitDie() {
		gameObject.SetActive(false);
	}
}
