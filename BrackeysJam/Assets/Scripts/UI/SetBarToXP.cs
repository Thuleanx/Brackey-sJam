

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBarToXP : BarFollow
{
	[SerializeField] string entityTag;
	PlayerStatus status;

	public override void Awake() {
		base.Awake();
		status = GameObject.FindGameObjectWithTag(entityTag).GetComponent<PlayerStatus>();
	}

	void Update() {
		// print((status.XP - status.XPTotFormula(status.Level)) + status.XPFormula(status.Level + 1));
		SetFill((float) (status.XP - status.XPTotFormula(status.Level)) / status.XPFormula(status.Level + 1));
	}	
}
