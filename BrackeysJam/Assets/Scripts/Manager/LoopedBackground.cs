using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LoopedBackground : MonoBehaviour
{
	Camera mainCamera;

	[SerializeField] GameObject[] backgroundObjects;
	[SerializeField] float choke;
	[SerializeField] float[] offSet;

	Vector2	screenBounds;
	LinkedList<GameObject>[] backgroundLists;
	float[] halfWidths;

	[SerializeField] float paralaxYScale = .1f;


	Vector2 lastFramePos;

	void Start() {
		mainCamera = GetComponent<Camera>();

		screenBounds = GetCameraBounds(mainCamera);

		backgroundLists = new LinkedList<GameObject>[backgroundObjects.Length];
		halfWidths = new float[backgroundObjects.Length];


		for (int i = 0; i < backgroundObjects.Length; i++) {
			GameObject obj = backgroundObjects[i];
			halfWidths[i] = obj.GetComponent<SpriteRenderer>().bounds.extents.x;
			backgroundLists[i] = LoadChildObjects(obj, offSet[i]);
		}
		lastFramePos = transform.position;
	}		

	Vector2 GetCameraBounds(Camera camera) {
		Vector3 upRight = new Vector3(Screen.width, Screen.height, transform.position.z);
		return camera.ScreenToWorldPoint(upRight) - camera.ScreenToWorldPoint(transform.position.z * Vector3.forward);
	}

	LinkedList<GameObject> LoadChildObjects(GameObject obj, float offset) {
		LinkedList<GameObject> backgrounds = new LinkedList<GameObject>();

		float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke + offset;

		// multiply by 2
		int childCnt = (int) Mathf.Ceil(screenBounds.x * 2 / objectWidth) + 1; 
		GameObject clone = Instantiate(obj) as GameObject;

		for (int i = 0; i < childCnt; i++) {
			GameObject c = Instantiate(clone) as GameObject;

			c.transform.SetParent(obj.transform);
			c.transform.position = new Vector3((objectWidth + offset) * i, obj.transform.position.y, obj.transform.position.z);
			c.name = obj.name + i;

			backgrounds.AddLast(c);
		}

		Destroy(clone);
		Destroy(obj.GetComponent<SpriteRenderer>());

		return backgrounds;
	}


	void Reposition(ref LinkedList<GameObject> objs, float halfWidth) {
		halfWidth -= choke;
		GameObject first = objs.First.Value, last = objs.Last.Value;

		if (first != last) {
			if (transform.position.x + screenBounds.x / 2 > last.transform.position.x + halfWidth) {
				first.transform.position = new Vector3(
					last.transform.position.x + 2 * halfWidth, 
					first.transform.position.y,
					first.transform.position.z
				);
				objs.RemoveFirst();
				objs.AddLast(first);
			} else if (transform.position.x - screenBounds.x / 2 < first.transform.position.x - halfWidth) {
				last.transform.position = new Vector3(
					first.transform.position.x - 2 * halfWidth,
					last.transform.position.y,
					last.transform.position.z
				);
				objs.RemoveLast();
				objs.AddFirst(last);
			}
		}
	}

	void LateUpdate() {
		for (int i = 0; i < backgroundLists.Length; i++) {
			LinkedList<GameObject> refCatch = backgroundLists[i];
			Reposition(ref refCatch, halfWidths[i]);
			float parallaxSpeed = 1 - Mathf.Clamp01(Mathf.Abs(transform.position.z / backgroundObjects[i].transform.position.z)); 

			Vector2 displacement = (Vector2) transform.position - lastFramePos;
			displacement.y *= -paralaxYScale;
			backgroundObjects[i].transform.Translate(displacement * parallaxSpeed);
		}
		lastFramePos = transform.position;
	}
}
