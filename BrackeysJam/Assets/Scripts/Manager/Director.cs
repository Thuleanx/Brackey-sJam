
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
	public static Director Instance;

	float timeElapsedSeconds = 0;

	#region Calculations 

	[SerializeField] float timeScale;
	float difficultyScale = 0;
	int stagesCompleted;

	[HideInInspector]
	public float masterCoef;

	#endregion

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		DontDestroyOnLoad(gameObject);

		masterCoef = 1;
	}

	void Update() {
		timeElapsedSeconds += Time.deltaTime;
	}	

	void LateUpdate() {
		masterCoef = (1 + timeElapsedSeconds / 60f * timeScale) * Mathf.Pow(1.15f, stagesCompleted);
	}
}
