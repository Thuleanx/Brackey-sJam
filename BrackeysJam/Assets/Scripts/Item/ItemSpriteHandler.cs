
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpriteHandler : MonoBehaviour
{
	public static ItemSpriteHandler Instance;

	[System.Serializable]
	public class MapKeyItemSprite : MapKey<Item, Sprite> {}

	public MapKeyItemSprite[] itemSpriteList;

	Dictionary<Item,Sprite> itemSpriteMap;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else Destroy(gameObject);
		itemSpriteMap = DictionaryHelper.GenDictionary(itemSpriteList);
	}

	public Sprite GetItemSprite(Item item) {
		return itemSpriteMap[item];
	}

	void OnDestroy() {
		if (Instance == this) Instance = null;
	}
}
