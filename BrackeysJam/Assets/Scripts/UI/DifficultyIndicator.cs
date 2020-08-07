
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BarFollow))]
public class DifficultyIndicator : MonoBehaviour
{
	Image image;
	BarFollow bar;

	[SerializeField]
	Difficulty[] difficulties;

	[System.Serializable]
	public class Difficulty {
		public Color color = Color.red;
		public string name;
	}

	[SerializeField] Image indicatorImage;
	[SerializeField] TMP_Text displayText;

	void Awake() {
		image = GetComponent<Image>();
		bar = GetComponent<BarFollow>();
	}

	void LateUpdate() {
		float progress = (EnemyStatus.LevelProgress(Director.Instance.masterCoef) - 1) / 3;
		int lastColor = Mathf.Clamp(Mathf.FloorToInt(progress), 0, difficulties.Length - 1);
		int nextColor = Mathf.Clamp(Mathf.CeilToInt(progress), 0, difficulties.Length - 1);

		if (lastColor == nextColor) {
			image.color = difficulties[lastColor].color;
			bar.SetFill(0);
		} else {
			image.color = difficulties[lastColor].color;
			indicatorImage.color = difficulties[nextColor].color;
			bar.SetFill(progress - lastColor);
		}
		displayText.text = difficulties[lastColor].name;
	}
}
