

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBarToHealth : BarFollow
{
	[SerializeField] string entityTag;
	Status status;

	public override void Awake() {
		base.Awake();
		status = GameObject.FindGameObjectWithTag(entityTag).GetComponent<Status>();
	}

	void Update() {
		SetFill((float) status.Health / status.maxHealth);
	}	
}
