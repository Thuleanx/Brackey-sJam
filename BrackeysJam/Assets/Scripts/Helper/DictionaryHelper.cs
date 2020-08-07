
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryHelper {
	public static Dictionary<Key, Value> GenDictionary<Key, Value>(List<MapKey<Key,Value>> ls) {
		Dictionary<Key, Value> dict = new Dictionary<Key, Value>();
		foreach (var map in ls) 
			dict[map.key] = map.value;
		return dict;
	}

	public static Dictionary<Key, Value> GenDictionary<Key, Value>(MapKey<Key,Value>[] ls) {
		Dictionary<Key, Value> dict = new Dictionary<Key, Value>();
		foreach (var map in ls) 
			dict[map.key] = map.value;
		return dict;
	}
}