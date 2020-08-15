using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	protected 
	Transform followPosition; // player

	protected 
	float camHeight, camWidth;

	public virtual void Awake() {
		SeekFollowPosition();
		UpdateSize();
	}

	protected void SeekFollowPosition() {
		followPosition = GameObject.FindGameObjectWithTag("Player").transform;
	}

	protected void UpdateSize() {
		camHeight = 2 * Camera.main.orthographicSize;
		camWidth = camHeight * Camera.main.aspect;
	}
}
