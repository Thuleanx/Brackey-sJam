using UnityEngine;

public class Calculate {
	public const float eps = .001f;
	
	public static float AsympEase(float current, float target, float rate) {
		return current + (target - current)	* rate * Time.timeScale;
	}

	public static Vector2 AsympEase(Vector2 current, Vector2 target, float rate) {
		return current + (target - current)	* rate * Time.timeScale;
	}

}