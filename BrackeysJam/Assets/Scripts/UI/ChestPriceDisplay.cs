
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class ChestPriceDisplay : MonoBehaviour
{
	ChestManager chest;
	TMP_Text text;
	Interactable inter;

	void Awake() {
		chest = GetComponentInParent<ChestManager>();
		text = GetComponent<TextMeshProUGUI>();
		inter = GetComponent<Interactable>();
	}

	void Update() {
		text.text = chest.GetCost().ToString(); 	
	}
}
