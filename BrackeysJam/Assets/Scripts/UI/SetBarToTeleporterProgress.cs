

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBarToTeleporterProgress : BarFollow
{
	void Update() {
		SetFill(1 - Teleporter.Instance.GetTimeLeft() / Teleporter.Instance.teleporterDurationSeconds);
	}	
}
