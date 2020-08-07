
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
	Status status;
	[SerializeField] GameObject[] hitboxes;
	[SerializeField] Projectile[] projectiles;

	[System.Serializable]
	public class Projectile {
		public string projectileTag;
		public GameObject source;
		public float rotationOffset = 0f;
	}

	public virtual void Awake() {
		status = GetComponent<Status>();
	}

	public void SpawnProjectile(int index) {
		GameObject obj = ObjectPool.Instance.Instantiate(projectiles[index].projectileTag);
		obj.transform.position = projectiles[index].source.transform.position;
		obj.transform.rotation = projectiles[index].source.transform.rotation * Quaternion.Euler(0f, 0f, projectiles[index].rotationOffset);
		if (status != null) {
			obj.GetComponent<CombatManager>()?.AttachStatus(status);
			obj.GetComponentInChildren<Hitbox>()?.AttachStatus(status);
		}
	}

	public void AttachStatus(Status status) {
		for (int i =0 ; i < hitboxes.Length; i++) {
			TriggerHitbox(i);
			hitboxes[i].GetComponent<Hitbox>().AttachStatus(status);
			DisableHitbox(i);
		}
	}

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
