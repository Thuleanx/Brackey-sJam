using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
	[HideInInspector]
	public bool inRange, interactable;

	[SerializeField] bool interOnAwake = true;

	public UnityEvent interactAction;

	void OnEnable() {
		inRange = false;
		interactable = interOnAwake;
	}

	public void EnableInteraction() {
		interactable = true;
	}

	public void DisableInteraction() {
		interactable = false;
	}

	void Update() {
		if (interactable && inRange) {
			if (InputManager.KeyDown(InputManager.Instance.interact))
				interactAction.Invoke();
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (interactable && collision.gameObject.CompareTag("Player")) {
			inRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (interactable && collision.gameObject.CompareTag("Player")) {
			inRange = false;
		}
	}
}
