
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
	Transform followPosition; // player

	void Awake() {
		if (followPosition == null)
			SeekFollowPosition();
	}

	void SeekFollowPosition() {
		followPosition = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		SeekFollowPosition();
		transform.position = new Vector3(
			followPosition.position.x,
			transform.position.y,
			transform.position.z);
	}
}
