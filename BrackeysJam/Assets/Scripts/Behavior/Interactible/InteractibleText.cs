
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Interactable))]
public class InteractibleText : MonoBehaviour
{
	Interactable inter;

	[SerializeField] string interactionTextTag;
	[SerializeField] Vector2 interactTextOffset;

	[SerializeField] float retentionTime = 1f;

	GameObject interactText;

	Timers timer;

	void Awake() {
		inter = GetComponent<Interactable>();
		timer = new Timers();

		timer.RegisterTimer("retention");
	}

	void Update() {
		if (inter.interactable && inter.inRange) {
			EnableText();
		} else if (!inter.interactable || !timer.ActiveAndNotExpired("retention"))
			DisableText();
	}

	void EnableText() {
		if (interactText == null) {
			interactText = ObjectPool.Instance.Instantiate(interactionTextTag);
			interactText.transform.position = interactTextOffset + (Vector2)transform.position;
			timer.StartTimer("retention", retentionTime);
		}
	}

	void DisableText() {
		if (interactText != null) {
			interactText.gameObject.SetActive(false);
			interactText = null;
		}
	}
}
