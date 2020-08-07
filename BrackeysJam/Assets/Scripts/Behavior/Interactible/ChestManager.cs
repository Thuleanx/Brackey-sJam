
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Interactable))]
public class ChestManager : MonoBehaviour
{
	Animator anim;
	PlayerBank bank;
	Interactable interactable;

	Canvas canvas;

	public float baseCost;
	float coefOnEnable;

	[SerializeField] string playerTag = "Player";


	[HideInInspector]
	public bool isOpen;

	void Awake() {
		anim = GetComponent<Animator>();
		interactable = GetComponent<Interactable>();
		canvas = GetComponentInChildren<Canvas>();
	}

	void Start() {
		bank = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerBank>();
	}

	public int GetCost() {
		return Mathf.CeilToInt(baseCost * Mathf.Pow(coefOnEnable, 1.25f));
	}

	void OnEnable() {
		isOpen = false;
		coefOnEnable = AssistantDirector.Instance == null ? 1 : AssistantDirector.Instance.masterCoef;
		anim.SetInteger("state", (int) ChestStates.Close);
		canvas.gameObject.SetActive(true);
	}

	void Update() {
		if (!isOpen)
			interactable.interactable = bank.GetGold() >= GetCost();
	}

	public void Open() {
		if (!isOpen) {
			if (bank.GetGold() >= GetCost()) {
				bank.SpendGold(GetCost());
				isOpen = true;
				anim.SetInteger("state", (int)ChestStates.Open);
				canvas.gameObject.SetActive(false);
			}
		}
	}

	public void SpawnItem() {
		GameObject itemObj = ObjectPool.Instance.Instantiate("Interatible: Item", (Vector2)transform.position + Vector2.up, Quaternion.identity);
		Item item = (Item)Mathf.FloorToInt(UnityEngine.Random.Range(0, Enum.GetValues(typeof(Item)).Length));
		itemObj.GetComponent<ItemPickup>().item = item;
	}
}
