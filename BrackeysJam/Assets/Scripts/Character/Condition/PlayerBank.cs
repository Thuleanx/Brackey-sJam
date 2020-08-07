


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBank : MonoBehaviour
{
	int money;
	[HideInInspector]
	public UnityEvent onGoldChangeEvent = new UnityEvent();

	public int GetGold() {
		return money;
	}

	public void AcquireGold(int amt) {
		money += amt;
		onGoldChangeEvent.Invoke();
	}

	public bool SpendGold(int amt) {
		if (money >= amt) {
			money -= amt;
			onGoldChangeEvent.Invoke();
			return true;
		}
		return false;
	}
}
