
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
	public int baseHealth, baseDamage;

	[HideInInspector]
	public float damage, health, maxHealth;

	public float Health {
		get { return health; }
		set { health = Mathf.Clamp(value, 0, maxHealth); }
	}
}
