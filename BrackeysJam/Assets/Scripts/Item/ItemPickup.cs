using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ItemPickup : MonoBehaviour
{
	[SerializeField] string playerTag = "Player";

	[SerializeField] Item item;

	SpriteRenderer sprite;

	void Awake() {
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		sprite.sprite = ItemSpriteHandler.Instance.GetItemSprite(item);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag(playerTag)) {
			print("Picked up: " + item);
			collision.gameObject.GetComponent<PlayerItemHandler>().IncrementStack(item);
			gameObject.SetActive(false);
		}
	}
}
