
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Teleporter : MonoBehaviour
{
	public static Teleporter Instance;
	public float teleporterDurationSeconds = 5f;

	Interactable interactable;

	[HideInInspector]
	public bool active, completed, queueNextStage;

	[SerializeField]
	Canvas textBeforeActivation, textAfterActivation;

	IncrementalTimers itimers;

	void Awake() {
		Instance = this;
		interactable = GetComponent<Interactable>();

		active = completed = queueNextStage = false;

		itimers = new IncrementalTimers();
		itimers.RegisterTimer("tpTimer");

		textBeforeActivation.gameObject.SetActive(false);
		textAfterActivation.gameObject.SetActive(false);
	}

	void Update() {
		interactable.interactable = (!active || (completed && !queueNextStage && (CatalogDirector.Instance == null || CatalogDirector.Instance.numberOfEnemies == 0)));
		textBeforeActivation.gameObject.SetActive(!active && interactable.inRange);
		textAfterActivation.gameObject.SetActive(completed && interactable.inRange && (CatalogDirector.Instance == null || CatalogDirector.Instance.numberOfEnemies == 0));
	}

	void LateUpdate() {
		itimers.Increment("tpTimer", Time.deltaTime);
		completed = active && itimers.Expired("tpTimer");
	}

	public float GetTimeLeft() {
		return itimers.TimeLeft("tpTimer");
	}

	public void ActivateTeleporter() {
		if (!active) {
			active = true;
			itimers.StartTimer("tpTimer", teleporterDurationSeconds);
			interactable.interactable = false;
		} else {
			interactable.interactable = false;
			queueNextStage = true;
			TransitionManager.Instance.TransitionToNext();
		}
	}
}
