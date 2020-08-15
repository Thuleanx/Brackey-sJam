

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectBound : MonoBehaviour
{
	BoxCollider2D self;
	BoxCollider2D bounds;
	[SerializeField] string boundsTag = "Bounds";

	void Awake() {
		bounds = GameObject.FindGameObjectWithTag(boundsTag).GetComponent<BoxCollider2D>();
		self = GetComponent<BoxCollider2D>(); 
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		bounds = GameObject.FindGameObjectWithTag(boundsTag).GetComponent<BoxCollider2D>();
		self = GetComponent<BoxCollider2D>(); 
	}	

	Vector3 Bound(Vector3 pos) {
		Bounds box = bounds.bounds;
		Bounds selfbox = self.bounds;
		if (selfbox.min.x < box.min.x)
			pos.x += box.min.x - selfbox.min.x;
		if (selfbox.max.x > box.max.x)
			pos.x -= box.max.x - selfbox.max.x;
		if (selfbox.min.y < box.min.y)
			pos.y += box.min.y - selfbox.min.y;
		if (selfbox.max.y > box.max.y)
			pos.y -= box.max.y - selfbox.max.y;

		return pos;
	}

	void LateUpdate() {
		transform.position = Bound(transform.position);
	}
}
