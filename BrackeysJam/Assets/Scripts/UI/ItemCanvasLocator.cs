

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemCanvasLocator : MonoBehaviour
{
	public static ItemCanvasLocator Instance;

	[SerializeField] Vector2Int itemPixelSize;
	[SerializeField] GameObject itemDisplayPrefab;
	[SerializeField] string playerTag = "Player";

	List<GameObject> itemDisplay = new List<GameObject>();
	PlayerItemHandler handler;

	void Awake() {
		Instance = this;
		for (int i = 0; i < transform.childCount; i++) {
			GameObject child = transform.GetChild(i).gameObject;

			RectTransform rect = child.GetComponent<RectTransform>();

			for (int x = 0; x <= rect.rect.width - itemPixelSize.x; x += itemPixelSize.x) {
				for (int y = 0; y <= rect.rect.height - itemPixelSize.x; y += itemPixelSize.y) {
					GameObject obj = Instantiate(itemDisplayPrefab, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));					

					RectTransform objRect = obj.GetComponent<RectTransform>();

					objRect.sizeDelta = itemPixelSize;

					objRect.anchoredPosition = (Vector2) rect.position - rect.anchorMin * rect.sizeDelta;

					objRect.localPosition += new Vector3(x, y, 0);

					itemDisplay.Add(obj);
				}
			}
		}
		foreach (var obj in itemDisplay)
			obj.transform.parent = transform;

		itemDisplay.Sort((o1, o2) => {
			if (o1.transform.position.y < o2.transform.position.y)
				return -1;
			else if (o1.transform.position.y > o2.transform.position.y)
				return 1;
			return o1.transform.position.x < o2.transform.position.x ? -1 : 1;
		});
	}	

	void Start() {
		handler = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerItemHandler>();
		foreach (var obj in itemDisplay)
			obj.SetActive(false);
	}

	public void UpdateChange() {
		List<Item> items = new List<Item>();
		foreach (Item item in Enum.GetValues(typeof(Item))) {
			if (handler.GetAcquiredTime(item) != float.MaxValue)
				items.Add(item);
		}
		items.Sort((a, b) => { return handler.GetAcquiredTime(a) < handler.GetAcquiredTime(b) ? -1 : 1; });

		for (int i = 0; i < itemDisplay.Count; i++) {
			if (i >= items.Count) itemDisplay[i].SetActive(false);
			else {
				itemDisplay[i].SetActive(true);
				ItemDisplay display = itemDisplay[i].GetComponent<ItemDisplay>();

				int numStacks = handler.GetStacks(items[i]);
				if (numStacks > 1)
					display.SetText("x" + numStacks);
				else display.SetText("");

				display.SetSprite(ItemSpriteHandler.Instance.GetItemSprite(items[i]));
			}
		}

		print("UPDATED " + itemDisplay.Count);
	}
}
