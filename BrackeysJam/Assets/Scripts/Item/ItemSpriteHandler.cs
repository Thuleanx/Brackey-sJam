
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
		// List<MapKey<Item,Sprite>> ls = new List<MapKey<Item, Sprite>>();
		Instance = this;
		itemSpriteMap = DictionaryHelper.GenDictionary(itemSpriteList);
	}

	public Sprite GetItemSprite(Item item) {
		return itemSpriteMap[item];
	}
}
