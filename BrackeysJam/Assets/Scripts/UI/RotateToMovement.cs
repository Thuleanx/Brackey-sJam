
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Movement))]
public class RotateToMovement : MonoBehaviour {

	[SerializeField] float offset;
	[SerializeField] bool flip;
	Movement movement;
	SpriteRenderer sprite;

	void Awake() {
		movement = GetComponent<Movement>();
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(movement.velocity.y, movement.velocity.x) + offset);

		sprite.flipY = (movement.velocity.x < 0) ^ flip;
	}
}