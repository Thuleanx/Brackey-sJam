using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyDisplay : MonoBehaviour
{
	TMP_Text text;
	[SerializeField] string playerTag = "Player";

	PlayerBank bank;
	float currentAmount, targetAmount;
	float moneySmoothDampTemp, smoothTimeSeconds = 1f;

	void ChangeTarget() {
		targetAmount = bank.GetGold();
	}

	void Start() {
		bank = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerBank>();
		bank.onGoldChangeEvent.AddListener(ChangeTarget);
		text = GetComponent<TextMeshProUGUI>();
	}

	void Update() {
		currentAmount = Mathf.SmoothDamp(currentAmount, targetAmount, ref moneySmoothDampTemp, smoothTimeSeconds);
		text.text = String.Format("{0}", Mathf.CeilToInt(currentAmount));
	}
}