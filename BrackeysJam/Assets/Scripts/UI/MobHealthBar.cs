


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobHealthBar: BarFollow
{
	[SerializeField] Status status;

	public void RegisterStatus(Status status) {
		this.status = status;
	}

	void Update() {
		SetFill((float) status.Health / status.maxHealth);
	}	
}
