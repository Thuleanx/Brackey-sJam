
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TurnWhiteShader : MonoBehaviour
{
	SpriteRenderer sprite;
	Material defaultMat;
	[SerializeField] Material whiteMat;

	Timers timers;

	void Awake() {
		sprite = GetComponent<SpriteRenderer>();
		defaultMat = sprite.material;
		timers = new Timers();
		timers.RegisterTimer("Flash Duration");
	}

	public void TurnWhite(float duration) {
		sprite.material = whiteMat;
		timers.StartTimer("Flash Duration", duration);
	}

	void Update() {
		if (timers.Expired("Flash Duration"))
			sprite.material = defaultMat;
	}
}

