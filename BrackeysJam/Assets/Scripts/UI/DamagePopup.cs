
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
	TextMeshProUGUI text;

	[SerializeField] Color normalAttack, critColor;
	[SerializeField] float normalFontSize, critFontSize, maxLocalSize, minLocalSize, maxElevation, disappearThreshold = .75f;
	[SerializeField] float duration = 1f;

	static int sortingOrder = 0;


	float timeElapsed = 0f;
	Vector2 startLocation;

	void Awake() {
		text = GetComponentInChildren<TextMeshProUGUI>();
		HasDuration dur = gameObject.AddComponent<HasDuration>();
		dur.duration = duration * 2;
	}

	public void SetUp(int damage, bool crit) {
		text.text = damage.ToString();
		if (!crit) {
			text.color = normalAttack;
			text.fontSize = normalFontSize;
		} else {
			text.color = critColor;
			text.fontSize = critFontSize;
		}
		startLocation = transform.position;
		timeElapsed = 0f;
		text.canvas.sortingOrder = sortingOrder++;
	}

	void Ease(float t) {
		t = Mathf.Clamp01(t);

		float easeConstantParabola = 1 - (2*t-1) * (2*t-1);
		// parabolic
		transform.localScale = (new Vector3(1,1,0)) * (minLocalSize + (maxLocalSize - minLocalSize) * easeConstantParabola) + Vector3.forward;
		// linear
		transform.position = startLocation + Vector2.up * maxElevation * t;

		if (t > disappearThreshold) {
			Color tmp = text.color;
			tmp.a = Mathf.Lerp(1, 0, (t - disappearThreshold) / (1 - disappearThreshold));
			text.color = tmp;
		}
	}

	void Update() {
		Ease(timeElapsed);
		timeElapsed += Time.deltaTime / duration;
	}
}
