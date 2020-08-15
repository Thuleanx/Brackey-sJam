using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class ItemEffect {
	static string filePath = "/JSON/ItemData.json";

	static Dictionary<string, ItemData> effects;

	public static void LoadData() {
		if (effects == null) {
			string jsonText = File.ReadAllText(Application.dataPath + filePath);
			effects = JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(jsonText);	
		}
	}

	public static float GetItemEffect(Item item, int stacks) {
		LoadData();
		string itemName = Enum.GetName(typeof(Item), item);
		if (effects.ContainsKey(itemName)) {
			ItemData data = effects[itemName];

			if (data.StackingType == "Linear")
				return stacks == 0 ? 0 : data.BaseEffect + data.StackingEffect * (stacks - 1);
			if (data.StackingType == "Hyperbolic")
				return 1 - 1 / (1 + (data.StackingEffect * stacks));
			if (data.StackingType == "Exponential")
				return Mathf.Pow(data.ExponentialFactor, stacks);
		} else {
			Debug.Log("Item not found : " + item);
			return -1111111;
		}

		return 0;
	}
}
