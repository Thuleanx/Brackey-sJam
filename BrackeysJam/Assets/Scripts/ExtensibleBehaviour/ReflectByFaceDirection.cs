

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectByFaceDirection : MonoBehaviour
{
	[SerializeField] bool flipX;

	Condition condition;
	Vector2 offset;
	

	void Start() {
		condition = GetComponentInParent<Condition>();
	}

	void Update() {
		if (condition.faceDir * (flipX ? -1 : 1) < 0) {
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else {
			transform.localScale = new Vector3(1,1,1);
		}
	}
}
