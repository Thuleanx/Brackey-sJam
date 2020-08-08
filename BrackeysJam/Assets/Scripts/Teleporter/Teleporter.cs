
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Interactable))]
public class Teleporter : MonoBehaviour
{
	public static Teleporter Instance;
	[SerializeField] float teleporterDurationSeconds = 90f;

	[SerializeField]
	Scene nextScene;

	Interactable interactable;

	bool active, completed, queueNextStage;

	IncrementalTimers itimers;

	void Awake() {
		Instance = this;
		interactable = GetComponent<Interactable>();

		active = completed = false;
		queueNextStage = true;
		itimers.RegisterTimer("tpTimer");
	}

	void Update() {
		interactable.interactable = (!active || (completed && !queueNextStage));
	}

	void LateUpdate() {
		itimers.Increment("tpTimer", Time.deltaTime);
		completed = active && itimers.Expired("tpTimer");
	}

	public void ActivateTeleporter() {
		if (!active) {
			active = true;
		}	
	}
}
