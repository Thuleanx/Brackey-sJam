
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDirector : MonoBehaviour {
	[SerializeField] float 	creditMultiplier = .75f, 
							rewardMultiplier = .2f,
							waveDuration = 1f,
							waveBetweenLength = 10f,
							spawnRange = 10f,
							spawnIntervalDuringWaveMin = .2f,
							spawnIntervalDuringWaveMax = 1f,
							spawnIntervalBetweenWavesMin = 4.5f,
							spawnIntervalBetweenWavesMax = 9f;
	[SerializeField] int maxEnemies = 40;
	[SerializeField] string playerTag;

	GameObject player;
	
	Timers timers;
	
	public bool active;

	[HideInInspector]
	float credit;

	float spawnInterval;
	bool newWave;

	public void SetActive(bool active) {
		this.active = active;
	}

	void Awake() {
		timers = new Timers();
		spawnInterval = Random.Range(spawnIntervalDuringWaveMin, spawnIntervalBetweenWavesMax);
		newWave = true;

		player = GameObject.FindGameObjectWithTag(playerTag);

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

	MonsterSpawnInfo ChooseMonster() {
		float weightedSum = 0;
		foreach (MonsterSpawnInfo monster in CatalogDirector.Instance.info) {
			if (credit > monster.cost)
				weightedSum += monster.weight;
		}
		float target = Random.Range(0, weightedSum);
		foreach (MonsterSpawnInfo monster in CatalogDirector.Instance.info) {
			if (credit > monster.cost) {
				target -= monster.weight;
				if (target <= 0)
					return monster;
			}
		}

		return null;
	}

	public void Spawn() {
		Vector2 spawnPoint = Vector2.zero;
		if (TilemapManager.Instance.GetPossibleSpawn(player.transform.position, spawnRange, ref spawnPoint) && CatalogDirector.Instance.numberOfEnemies < maxEnemies) {
			spawnPoint += new Vector2(.5f, .5f);

			MonsterSpawnInfo monster = ChooseMonster();

			if (monster != null) {
				GameObject obj = ObjectPool.Instance.Instantiate("Monster: " + monster.name, spawnPoint, Quaternion.identity);
				Collider2D collider = obj.GetComponent<Collider2D>();
				if (collider != null)
					obj.transform.position += Vector3.up * (collider.bounds.size.y);
				obj.GetComponent<EnemyStatus>().AssignValue(rewardMultiplier);
				credit -= monster.cost;

				CatalogDirector.Instance.numberOfEnemies++;
			}
		}
	}
} 