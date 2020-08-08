
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UIPersistent : MonoBehaviour
{
	static UIPersistent Instance;

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