
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(Image))]
public class AbilityCoolDownDisplay : MonoBehaviour
{
	[SerializeField] string playerTag = "Player";
	[SerializeField] PlayerState ability;
	[SerializeField] Color fade, full;

	TMP_Text text;

	PlayerCombat combat;
	Image image;

	void Awake() {
		combat = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerCombat>();
		image = GetComponent<Image>();
		text = GetComponentInChildren<TextMeshProUGUI>();
	}

	void Update() {
		float cd = combat.GetCoolDown(ability);
		image.fillAmount = Mathf.Clamp01(1 - (float) cd / combat.GetTotalCoolDown(ability));

		if (text != null)
			text.text = "";
		if (cd > 0) {
			image.color = fade;
			// if (cd <= 9)
				text.text = Mathf.CeilToInt(cd).ToString();
		} else {
			image.color = full;
		}
	}
}
