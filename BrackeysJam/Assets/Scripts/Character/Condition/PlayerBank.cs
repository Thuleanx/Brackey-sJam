


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerBank : MonoBehaviour
{
	int money;
	[HideInInspector]
	public UnityEvent onGoldChangeEvent = new UnityEvent();

	void Awake() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GetComponent<PlayerStatus>().AcquireXP(money);
		money = 0;
	}

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
