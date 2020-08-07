
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantDirector : MonoBehaviour
{
	public static AssistantDirector Instance;

	[HideInInspector]
	public float timeElapsedSeconds = 0;

	#region Calculations 

	[SerializeField] float timeScale;
	float difficultyScale = 1;
	int stagesCompleted;

	[HideInInspector]
	public float masterCoef;
	[HideInInspector]
	public float playerFactor = 1;

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
		masterCoef = (1 + timeElapsedSeconds / 60f * timeScale * difficultyScale) * Mathf.Pow(1.15f, stagesCompleted);
	}
}
