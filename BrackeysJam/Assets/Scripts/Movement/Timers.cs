using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers
{
	Dictionary<string, float> 	timeToExpire = new Dictionary<string, float>();
	Dictionary<string, bool> 	timerActive = new Dictionary<string, bool>();

	public bool RegisterTimer(string name) {
		if (!timeToExpire.ContainsKey(name)) {
			timeToExpire[name] = 0;
			timerActive[name] = false;
		}
		return !timeToExpire.ContainsKey(name);
	}

	public void StartTimer(string name, float durationOverride) {
		timeToExpire[name] = Time.time + durationOverride;
		SetActive(name, true);
	}

	public bool Active(string name) { return timerActive[name]; }

	public void SetActive(string name, bool active) {
		timerActive[name] = active;
	}

	public bool ActiveAndNotExpired(string name) {
		return Time.time < timeToExpire[name] && 
			timerActive[name];
	}

	public bool Expired(string name) {
		return Time.time >= timeToExpire[name];
	}
}