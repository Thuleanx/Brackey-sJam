
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDuration : MonoBehaviour
{
	public float duration;
	float timeStart;

	void OnEnable() {
		timeStart = Time.time;
	}

	void Update() {
		if (timeStart + duration <= Time.time) 
			gameObject.SetActive(false);
	}
}