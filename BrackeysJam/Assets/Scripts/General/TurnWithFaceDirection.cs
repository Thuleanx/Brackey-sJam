

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TurnWithFaceDirection : MonoBehaviour
{
	[SerializeField] bool flipX;

	Collider2D box;
	Condition condition;

	Vector2 offset;

	void Start() {
		box = GetComponent<BoxCollider2D>();
		condition = GetComponentInParent<Condition>();
		offset = box.offset;
	}

	void Update() {
		if (condition.faceDir * (flipX ? -1 : 1) < 0)
			box.offset = new Vector2(
				-offset.x,
				offset.y
			);
		else
			box.offset = offset;
	}
}
