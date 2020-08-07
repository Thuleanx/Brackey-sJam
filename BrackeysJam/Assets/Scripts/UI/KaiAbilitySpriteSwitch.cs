


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KaiAbilitySpriteSwitch : MonoBehaviour
{
	[SerializeField] string playerTag = "Player";

	[SerializeField]
	Sprite shealth, unshealth;

	Image img;
	Kaiser kaiser;

	void Awake() {
		img = GetComponent<Image>();
		kaiser = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Kaiser>();
	}

	void LateUpdate() {
		img.sprite = kaiser.shealth ? shealth : unshealth;
	}
}
