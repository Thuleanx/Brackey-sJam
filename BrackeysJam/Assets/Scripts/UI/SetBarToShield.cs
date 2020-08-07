


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBarToShield : BarFollow
{
	[SerializeField] string entityTag;
	Status status;

	public override void Awake() {
		base.Awake();
		status = GameObject.FindGameObjectWithTag(entityTag).GetComponent<Status>();
	}

	void Update() {
		if (status.maxShield > 0) {
			SetFill((float) status.shield / status.maxShield);
		} else SetFill(0);
	}	
}
