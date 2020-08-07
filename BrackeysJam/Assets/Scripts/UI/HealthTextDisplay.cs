

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthTextDisplay : MonoBehaviour
{
	TMP_Text text;
	Status status;

	[SerializeField] string entityTag;


	void OnEnable() {
		text = GetComponent<TextMeshProUGUI>();
		status = GameObject.FindGameObjectWithTag(entityTag).GetComponent<Status>();
	}

	void Update() {
		text.text = Mathf.FloorToInt(status.Health) + " / " + Mathf.FloorToInt(status.maxHealth);
	}
}
