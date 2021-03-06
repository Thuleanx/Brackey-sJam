
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {

	[HideInInspector]
	public Vector2 velocity;	

	public abstract void ApplyKnockback(Vector2 displacement);
}