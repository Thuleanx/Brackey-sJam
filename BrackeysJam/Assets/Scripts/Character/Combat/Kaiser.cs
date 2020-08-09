

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kaiser : MonoBehaviour {
	[SerializeField]
	bool shealthDefault = true;

	[HideInInspector]
	public bool shealth;

	static Kaiser Instance;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else if (Instance != this) Destroy(gameObject);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		transform.position = (Vector2) GameObject.FindGameObjectWithTag("Player").transform.position;	
	}

	void OnDestroy() {
		if (Instance == this) Instance = null;
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnEnable() {
		shealth = shealthDefault;
	}
}
