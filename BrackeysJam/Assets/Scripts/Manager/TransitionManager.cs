using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
	public static TransitionManager Instance;

	public float transitionTime = 5f;

	[System.Serializable]
	public class SceneInfo {
		public Scene scene;
	}

	[SerializeField]
	SceneInfo[] info;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}

	}

	public void Reload() {
		int index = SceneManager.GetActiveScene().buildIndex;
		StartCoroutine(LoadLevel(index));
	}

	public void TransitionToNext() {
		int index = SceneManager.GetActiveScene().buildIndex;
		if (index == 0) 
			StartCoroutine(LoadLevel(1));
		else
			StartCoroutine(LoadLevel(1 + (index - 1) % (info.Length - 2)));
	}

	public void TransitionToLast() {
		StartCoroutine(LoadLevel(info.Length - 1));
	}

	IEnumerator LoadLevel(int index) {
		yield return new WaitForSeconds(transitionTime);

		SceneManager.LoadScene(index);
	}
}