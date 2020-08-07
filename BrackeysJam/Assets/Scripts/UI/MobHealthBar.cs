


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobHealthBar: BarFollow
{
	Status status;
	Vector2 position;

	public override void Awake() {
		base.Awake();
		status = GetComponentInParent<Status>();
		position = transform.localPosition;
	}

	public void RegisterStatus(Status status) {
		this.status = status;
	}

	void Update() {
		SetFill((float) status.Health / status.maxHealth);
		transform.position = (Vector2) transform.parent.position + position;
		transform.rotation = Quaternion.identity;
	}	
}
