﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
	[SerializeField] string tilemapReadTag = "Map";

	public static TilemapManager Instance;

	BoundsInt bounds;
	Vector2Int offset;
	int[,] map;

	void Awake() {
		Instance = this;
	}

	void Start() {
		LoadMap();
	}

	void LoadMap() {
		int cnt = 0;
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tilemapReadTag)) {
			Tilemap tilemap = obj.GetComponent<Tilemap>();
			if (cnt++ == 0) {
				bounds = tilemap.cellBounds;
			} else {
				bounds.SetMinMax(
					Vector3Int.Min(bounds.min, tilemap.cellBounds.min), 
					Vector3Int.Max(bounds.max, tilemap.cellBounds.max)
				);
			}
		}
		offset = (Vector2Int) bounds.min;

		map = new int[bounds.max.x - bounds.min.x + 1, bounds.max.y - bounds.min.y + 1];
		for (int i = 0; i <= bounds.max.x - bounds.min.x; i++)
		for (int j = 0; j <= bounds.max.y - bounds.min.y; j++)
			map[i,j] = -1;

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Map"))
		{
			Tilemap tilemap = obj.GetComponent<Tilemap>();

			for (int y = bounds.min.y; y <= bounds.max.y; y++)
			{
				for (int x = bounds.min.x; x <= bounds.max.x; x++)
				{
					if (tilemap.HasTile(new Vector3Int(x, y, 0)))
						map[x - offset.x, y - offset.y] = 0;
				}
			}
		}

		for (int i = 0; i <= bounds.max.x - bounds.min.x; i++)
		for (int j = 0; j <= bounds.max.y - bounds.min.y; j++)
		{
			if (map[i, j] == -1) map[i, j] = bounds.max.y - bounds.min.y + 1;
			if (j > 0) map[i, j] = Mathf.Min(map[i, j], map[i, j - 1] + 1);
		}
	}
	
	public bool HasTile(Vector2 pos) {
		Vector2Int posAdjusted = Vector2Int.FloorToInt(pos);
		return bounds.Contains((Vector3Int) posAdjusted) ? map[posAdjusted.x - offset.x, posAdjusted.y - offset.y] == 0 : false;
	}

	public int DistanceToGround(Vector2 pos) {
		Vector2Int posAdjusted = Vector2Int.FloorToInt(pos);
		return (bounds.Contains((Vector3Int) posAdjusted) ? map[posAdjusted.x - offset.x, posAdjusted.y - offset.y] - 1 : bounds.max.y - bounds.min.y + 1);
	}
}