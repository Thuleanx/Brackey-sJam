
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombat : MonoBehaviour
{
	[SerializeField] GameObject[] hitboxes;

	public void TriggerHitbox(int index) {
		hitboxes[index].SetActive(true);
	}

	public void DisableHitbox(int index) {
		hitboxes[index].SetActive(false);
	}

	public void Start() {
		foreach (GameObject obj in hitboxes)
			obj.SetActive(false);
	}
}
