
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
	[HideInInspector]
	public bool onGround, onWall, immune;

	[HideInInspector]
	public float wallDir;

	// [HideInInspector] public float faceDir = 1;
	float FaceDir = 1;
	public float faceDir { get { return FaceDir; } set {
		if (!LockFaceDir())
			FaceDir = value;
	} }

	[HideInInspector]
	public bool dead;

	public virtual bool LockFaceDir() {
		return false;
	}

	void OnEnable() {
		onGround = onWall = dead = false;
	}
}
