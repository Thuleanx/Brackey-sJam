
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWhenSpawn : MonoBehaviour
{
	[SerializeField] string soundName;

	void OnEnable() {
		AudioManager.Instance?.Play(soundName);	
	}
}
