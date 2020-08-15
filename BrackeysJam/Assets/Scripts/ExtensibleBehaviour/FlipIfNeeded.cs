

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlipIfNeeded : MonoBehaviour
{
	SpriteRenderer sprite;	
	[SerializeField] float offsetAngle = 0f;

	void Awake() {
		sprite = GetComponent<SpriteRenderer>();
	}

	void Update() {
		float rot = transform.rotation.eulerAngles.z;
		if (rot < 0) rot += 360;

		sprite.flipY = rot > 90 && rot < 270;
	}
}
