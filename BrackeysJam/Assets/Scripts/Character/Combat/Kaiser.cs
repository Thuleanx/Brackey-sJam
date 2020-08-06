

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kaiser : MonoBehaviour {
	[SerializeField]
	bool shealthDefault = true;

	[HideInInspector]
	public bool shealth;

	void OnEnable() {
		shealth = shealthDefault;
	}
}
