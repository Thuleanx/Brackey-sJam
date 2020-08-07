


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerItemHandler : MonoBehaviour
{
	Dictionary<Item, int> itemsPossess = new Dictionary<Item, int>();
	Dictionary<Item, float> earliestAcquireTime = new Dictionary<Item, float>();

	void Awake() {
		foreach (Item item in Enum.GetValues(typeof(Item)))
			itemsPossess[item] = 0;
	}

	public void IncrementStack(Item item) {
		itemsPossess[item]++;
		if (!earliestAcquireTime.ContainsKey(item))
			earliestAcquireTime[item] = Time.time;
		ItemCanvasLocator.Instance?.UpdateChange();
	}

	public int GetStacks(Item item) {
		return itemsPossess[item];
	}

	public float GetAcquiredTime(Item item) {
		if (earliestAcquireTime.ContainsKey(item))
			return earliestAcquireTime[item];
		return float.MaxValue;
	}
}
