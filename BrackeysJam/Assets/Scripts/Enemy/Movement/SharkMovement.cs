
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovement : MonoBehaviour
{
	[SerializeField] Vector2 velocity;

	[SerializeField] string playerTag = "Player";

	[SerializeField] float gravity = 5;
	[SerializeField] float bouyancyConstant = 7f;
	[SerializeField] float terminalVelocity = 12f;

	void Update() {
		velocity.y -= gravity * Time.deltaTime;

		if (TilemapManager.Instance.HasTile(transform.position)) {
			velocity.y = bouyancyConstant * Time.deltaTime;
		}

		velocity.y = Mathf.Clamp(velocity.y, -terminalVelocity, float.MaxValue);

		transform.Translate(velocity);
	}
}
