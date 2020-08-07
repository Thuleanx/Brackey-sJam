

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
	Image sprite;
	TMP_Text text;

	void Awake() {
		text = GetComponentInChildren<TMP_Text>();
		sprite = GetComponentInChildren<Image>();
	}

	public void SetText(string text) {
		this.text.text = text;
	}

	public void SetSprite(Sprite sprite) {
		this.sprite.sprite = sprite;
	}
}
