
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
	[HideInInspector]
	public bool onGround, onWall, immune;

	[HideInInspector]
	public float faceDir = 1, wallDir;

	[HideInInspector]
	public bool dead;

	void OnEnable() {
		onGround = onWall = dead = false;
	}
}
