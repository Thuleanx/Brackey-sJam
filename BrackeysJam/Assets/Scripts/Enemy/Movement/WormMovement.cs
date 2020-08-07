

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour
{
	[SerializeField] Vector2 velocity;
	[SerializeField] float speed = 6;
	[SerializeField] float maxAcceleration = 6;

	GameObject player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		Vector2 desiredVelocity = speed * ((Vector2) (player.transform.position - transform.position)).normalized;
		Vector2 steer = Vector2.ClampMagnitude(desiredVelocity - velocity, maxAcceleration * Time.deltaTime);
	}
}

