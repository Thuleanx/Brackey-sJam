
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
	[HideInInspector]
	public bool onGround, onWall;

	[HideInInspector]
	public float faceDir = 1, wallDir;

	[HideInInspector]
	public bool dead;

	public void CommitDie() {
		gameObject.SetActive(false);
	}
}
