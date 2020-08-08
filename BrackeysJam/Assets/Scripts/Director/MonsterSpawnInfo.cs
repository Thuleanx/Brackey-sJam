
[System.Serializable]
public class MonsterSpawnInfo {
	public MonsterCategory category;
	public string name;
	public float cost, weight;
}

[System.Serializable]
public enum MonsterCategory {
	Basic = 0,
	MiniBoss = 1
}