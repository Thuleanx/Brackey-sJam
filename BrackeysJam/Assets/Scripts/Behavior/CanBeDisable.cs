
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeDisable : MonoBehaviour
{
	public void Disable() {
		gameObject.SetActive(false);
	}
}