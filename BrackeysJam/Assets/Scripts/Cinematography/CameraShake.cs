using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public static CameraShake Instance;

	[SerializeField] float traumaDecayRate = .2f;
	[SerializeField] float translationalOffsetMax = 2f;
	[SerializeField] float maxShakeAngle = 30f;
	[SerializeField] float perlinNoiseSampleSpeedRate = 10f;
	float trauma;

	float seedAngle, seedTransX, seedTransY;

	void Awake() {
		Instance = this;
	}

	public void IncreaseTrauma(float amt) {
		trauma = Mathf.Clamp01(trauma + amt);
	}

	float PerlinNegOneToOne(float seed) {
		return 1 - 2 * Mathf.PerlinNoise(seed, Time.time * perlinNoiseSampleSpeedRate);
	}

	void Update() {
		if (!Mathf.Approximately(trauma, 0f)) {
			float shake = trauma * trauma;

			float angle = maxShakeAngle * shake * PerlinNegOneToOne(seedAngle);
			float offsetX = translationalOffsetMax * shake * PerlinNegOneToOne(seedTransX); 
			float offsetY = translationalOffsetMax * shake * PerlinNegOneToOne(seedTransY);

			transform.localPosition = new Vector3(
				offsetX,
				offsetY,
				transform.localPosition.z
			);

			transform.rotation = Quaternion.Euler(0, 0, angle);
		} else {
			transform.localPosition = new Vector3(0f, 0f, transform.localPosition.z);
			transform.rotation = Quaternion.identity;
		}	
		trauma = Mathf.Clamp01(trauma - traumaDecayRate * Time.deltaTime);
	}
}
