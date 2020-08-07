
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDirector : MonoBehaviour {
	[SerializeField] float 	creditMultiplier = .75f, 
							rewardMultiplier = .2f,
							waveDuration = 1f,
							waveBetweenLength = 10f,
							spawnIntervalDuringWaveMin = .2f,
							spawnIntervalDuringWaveMax = 1f,
							spawnIntervalBetweenWavesMin = 4.5f,
							spawnIntervalBetweenWavesMax = 9f;

	[SerializeField] GameObject testPrefab;
	[SerializeField] string playerTag;
	
	Timers timers;
	
	public bool active;
	public float credit;

	float spawnInterval;
	bool newWave;

	public void SetActive(bool active) {
		this.active = active;
	}

	void Awake() {
		timers = new Timers();
		spawnInterval = Random.Range(spawnIntervalDuringWaveMin, spawnIntervalBetweenWavesMax);
		newWave = true;

		timers.RegisterTimer("WaveCD");
		timers.RegisterTimer("WaveDuration");
		timers.RegisterTimer("SpawnCD");
	}

	void Update() {
		if (active) {
			float cps = creditMultiplier * (1 + .4f * AssistantDirector.Instance.masterCoef);
			credit += cps * Time.deltaTime;

			if (timers.Expired("WaveCD")) {
				// new wave
				if (newWave && timers.Expired("WaveDuration"))
					timers.StartTimer("WaveDuration", waveDuration);
				newWave = false;

				if (timers.Expired("WaveDuration")) {
					spawnInterval = Random.Range(spawnIntervalDuringWaveMin, spawnIntervalDuringWaveMax);
					newWave = true;
					timers.StartTimer("WaveCD", waveBetweenLength);
				} else if (timers.Expired("SpawnCD")) {
					Spawn();
					timers.StartTimer("SpawnCD", spawnInterval);
				}
			} else {
				if (timers.Expired("SpawnCD")) {
					Spawn();			
					timers.StartTimer("SpawnCD", Random.Range(spawnIntervalBetweenWavesMin, spawnIntervalBetweenWavesMax));
				}
			}
		}
	}

	public void Spawn() {
		Vector2 pos = Vector2.zero;
		ObjectPool.Instantiate(testPrefab, pos, Quaternion.identity);
	}
} 