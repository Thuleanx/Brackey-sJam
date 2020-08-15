
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepManager
{
	public static void Sleep(float seconds, MonoBehaviour behaviour) {
		behaviour.StartCoroutine(SleepWait(seconds));
	}

	static IEnumerator SleepWait(float seconds) {
		Time.timeScale = 0.1f;
		yield return new WaitForSeconds(.1f * seconds);
		Time.timeScale = 1f;
	}
}