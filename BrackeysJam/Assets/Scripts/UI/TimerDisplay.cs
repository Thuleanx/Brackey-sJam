
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TMP_Text))]
public class TimerDisplay : MonoBehaviour
{
	TMP_Text text;

	void Awake() {
		text = GetComponent<TextMeshProUGUI>();
	}

	void LateUpdate() {
		int numSeconds = Mathf.FloorToInt(AssistantDirector.Instance.timeElapsedSeconds);
		int hours = numSeconds / 60 / 60;
		int minutes = numSeconds / 60 % 60;
		int seconds = numSeconds % 60;

		if (hours > 0) {
			text.text = String.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
		} else {
			text.text = String.Format("{0:00} : {1:00}", minutes, seconds);
		}
	}
}
