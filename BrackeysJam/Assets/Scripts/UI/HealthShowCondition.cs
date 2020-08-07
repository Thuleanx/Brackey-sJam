

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Status))]
public class HealthShowCondition : MonoBehaviour
{
	MobHealthBar bar;
	Status status;

	void Awake() {
		bar = GetComponentInChildren<MobHealthBar>();
		status = GetComponent<Status>();
	}

	void Update() {
		bar?.gameObject.SetActive(status.Health != status.maxHealth && status.Health != 0);
	}
}
