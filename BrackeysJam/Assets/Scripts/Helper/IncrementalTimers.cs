using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class IncrementalTimers
{
	Dictionary<string, float> 	timeToExpire = new Dictionary<string, float>();

	public bool RegisterTimer(string name) {
		if (!timeToExpire.ContainsKey(name)) {
			timeToExpire[name] = 0;
		}
		return !timeToExpire.ContainsKey(name);
	}

	public void StartTimer(string name, float duration) {
		timeToExpire[name] = duration;
	}

	public bool Expired(string name) {
		return timeToExpire[name] <= 0;
	}

	public void Increment(string name, float amount) {
		timeToExpire[name] -= Time.deltaTime;
	}

	public void Exhaust(string name) {
		timeToExpire[name] = 0;
	}

	public void ExhaustAll() {
		for (int i = 0; i < timeToExpire.Count; i++) {
			var item = timeToExpire.ElementAt(i);
			Exhaust(item.Key);
		}
	}

	public float TimeLeft(string name) {
		return timeToExpire[name];
	}
}