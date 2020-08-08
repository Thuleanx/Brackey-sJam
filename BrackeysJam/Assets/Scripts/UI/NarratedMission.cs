
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NarratedMission : MonoBehaviour
{
	TMP_Text text;
	void Awake() {
		text = GetComponent<TextMeshProUGUI>();
	}

	void Update() {
		if (!Teleporter.Instance.active)
			text.text = "Find the ancient device";
		else if (Teleporter.Instance.completed)	{
			if (CatalogDirector.Instance.numberOfEnemies > 0) {
				text.text = "Enemies to hunt down: " + CatalogDirector.Instance.numberOfEnemies;
			} else {
				text.text = "Use the ancient device";
			}
		} else if (Teleporter.Instance.active)
			text.text = "Survive!";
	}
}
