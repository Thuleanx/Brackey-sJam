
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BarFollow : MonoBehaviour
{
	[SerializeField] string barTag;	
	Slider slider;	

	public virtual void Awake() {
		slider = GetComponent<Slider>();
		if (barTag != null && barTag.Length > 0) 
			slider.fillRect = GameObject.FindGameObjectWithTag(barTag).GetComponent<RectTransform>();	
	}

	public void SetFill(float fraction) {
		slider.value = Mathf.Clamp01(fraction);
	}
}
