
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStaysInBound : MonoBehaviour
{
	float camHeight, camWidth;

	BoxCollider2D bounds;

	[SerializeField] string boundsTag;

	void Start() {
		camHeight = 2 * Camera.main.orthographicSize;
		camWidth = camHeight * Camera.main.aspect;
		bounds = GameObject.FindGameObjectWithTag(boundsTag).GetComponent<BoxCollider2D>();
	}

	Vector3 Bound(Vector3 pos) {
		Bounds box = bounds.bounds;
		pos.x = Mathf.Clamp(pos.x, box.min.x + camWidth / 2, box.max.x - camWidth / 2);
		pos.y = Mathf.Clamp(pos.y, box.min.y + camHeight / 2, box.max.y - camHeight / 2);
		return pos;
	}

	void LateUpdate() {
		transform.position = Bound(transform.position);
	}
}
