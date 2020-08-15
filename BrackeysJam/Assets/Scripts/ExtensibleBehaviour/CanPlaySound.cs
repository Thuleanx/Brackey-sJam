using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanPlaySound : MonoBehaviour
{
	public void Play(string soundName) {
		AudioManager.Instance.Play(soundName);
	}
}