
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// persistent though scenes
using UnityEngine.SceneManagement;

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
	[SerializeField] bool teleporter;

	public static CombatDirector teleporterDirector;

	GameObject player;
	
	Timers timers;
	
	[HideInInspector]
	public bool active;

	[HideInInspector]
	float credit;

	float spawnInterval;
	bool newWave;
	bool bossSpawned;

	void Awake() {
		timers = new Timers();
		spawnInterval = Random.Range(spawnIntervalDuringWaveMin, spawnIntervalBetweenWavesMax);
		newWave = true;

		player = GameObject.FindGameObjectWithTag(playerTag);
		if (teleporter && teleporterDirector == null) teleporterDirector = this;

		timers.RegisterTimer("WaveCD");
		timers.RegisterTimer("WaveDuration");
		timers.RegisterTimer("SpawnCD");

		SceneManager.sceneLoaded += OnSceneLoad;
	}

	void OnSceneLoad(Scene scene, LoadSceneMode mode) {
		spawnInterval = Random.Range(spawnIntervalDuringWaveMin, spawnIntervalBetweenWavesMax);
		newWave = true;
		credit = 0;
		active = false;
		bossSpawned = false;
	}

	void OnDestroy() {
		SceneManager.sceneLoaded -= OnSceneLoad;
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
					if (teleporter && !bossSpawned)
						bossSpawned |= ForceSpawnBoss();
					else Spawn();
					timers.StartTimer("SpawnCD", spawnInterval);
				}
			} else {
				if (timers.Expired("SpawnCD")) {
					if (teleporter && !bossSpawned)
						bossSpawned |= ForceSpawnBoss();
					else Spawn();
					timers.StartTimer("SpawnCD", Random.Range(spawnIntervalBetweenWavesMin, spawnIntervalBetweenWavesMax));
				}
			}
		} else if (teleporterDirector != null && !teleporter) {
			teleporterDirector.credit += credit;
			credit = 0;
		}
	}

	void LateUpdate() {
		if (teleporter) active = Teleporter.Instance.active && !Teleporter.Instance.completed;
		else active = !Teleporter.Instance.active;
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

	MonsterSpawnInfo ChooseMonster(MonsterCategory category) {
		float weightedSum = 0;
		foreach (MonsterSpawnInfo monster in CatalogDirector.Instance.info) {
			if (credit > monster.cost && monster.category == category)
				weightedSum += monster.weight;
		}
		float target = Random.Range(0, weightedSum);
		foreach (MonsterSpawnInfo monster in CatalogDirector.Instance.info) {
			if (credit > monster.cost && monster.category == category) {
				target -= monster.weight;
				if (target <= 0)
					return monster;
			}
		}

		return null;
	}

	public bool Spawn() {
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
				return true;
			}
		}
		return false;
	}

	public bool ForceSpawnBoss() {
		Vector2 spawnPoint = Vector2.zero;
		if (TilemapManager.Instance.GetPossibleSpawn(player.transform.position, spawnRange, ref spawnPoint) && CatalogDirector.Instance.numberOfEnemies < maxEnemies) {
			spawnPoint += new Vector2(.5f, .5f);

			MonsterSpawnInfo monster = null;

			while (monster == null) {
				credit += 100f; // inflate until boss spawnable
				monster = ChooseMonster(MonsterCategory.MiniBoss);
			}

			GameObject obj = ObjectPool.Instance.Instantiate("Monster: " + monster.name, spawnPoint, Quaternion.identity);
			Collider2D collider = obj.GetComponent<Collider2D>();
			if (collider != null)
				obj.transform.position += Vector3.up * (collider.bounds.size.y);
			obj.GetComponent<EnemyStatus>().AssignValue(rewardMultiplier);
			credit -= monster.cost;

			CatalogDirector.Instance.numberOfEnemies++;
			return true;
		}
		return false;
	}
} 