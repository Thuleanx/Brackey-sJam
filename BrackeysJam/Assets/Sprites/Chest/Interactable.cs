using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
	[HideInInspector]
	public bool inRange;

	public UnityEvent interactAction;

	void Update() {
		if (inRange)
			if (InputManager.Instance.interactDown)
				interactAction.Invoke();
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			inRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			inRange = false;
		}
	}
}
