

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeleporterCountdown : MonoBehaviour
{
	[SerializeField]
	GameObject countdownObject;

	void Update() {
		countdownObject.SetActive(Teleporter.Instance.active && !Teleporter.Instance.completed);
	}
}
