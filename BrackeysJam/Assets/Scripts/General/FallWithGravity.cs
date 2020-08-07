
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastCollider2D))]
public class FallWithGravity : MonoBehaviour
{
	RaycastCollider2D controller;

	[SerializeField] float gravity = 70;

	[HideInInspector]
	public Vector2 velocity;

	void Awake() {
		controller = GetComponent<RaycastCollider2D>();
	}

	void Update() {
		velocity.y -= gravity;
	}

	void LateUpdate() {
		controller.Move(velocity);
	}
}
