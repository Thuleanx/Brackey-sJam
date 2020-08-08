

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DestroyOnFirstScene : MonoBehaviour
{
	void Awake() {
		SceneManager.activeSceneChanged += DestroyOnMenuScreen;
	}

	void DestroyOnMenuScreen(Scene oldScene, Scene newScene)
     {
         if (newScene.buildIndex == 0) //could compare Scene.name instead
         {
             Destroy(gameObject); //change as appropriate
         }
     }
}

