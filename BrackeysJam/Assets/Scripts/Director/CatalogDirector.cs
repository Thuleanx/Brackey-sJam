
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogDirector : MonoBehaviour {
	public static CatalogDirector Instance;

	public MonsterSpawnInfo[] info;

	public int numberOfEnemies;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	void OnDestroy() {
		if (Instance == this) Instance = null;
	}
}