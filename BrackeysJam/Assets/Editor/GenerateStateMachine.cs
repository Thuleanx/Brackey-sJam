using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenerateStateMachine : EditorWindow
{
	string resultText = "";
	string machineName = "";

	[MenuItem("Thuleanx/GenerateAnimator")]
	static void Init() {
		GenerateStateMachine window = ScriptableObject.CreateInstance<GenerateStateMachine>();

		window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
		window.ShowPopup();
	}

	void OnGUI() {
		machineName = EditorGUILayout.TextField("name of animator ", machineName);
		resultText = EditorGUILayout.TextField("number of states: ", resultText);
		int num = 0;
		if (GUI.Button(new Rect(0, 50, position.width, 30), "Agree!") && int.TryParse(resultText, out num) && machineName.Length > 0)
		{
			CreateAnimator(machineName, num);
			this.Close();
		}
	}

	void OnInspectorUpdate() {
		Repaint();
	}

	void CreateAnimator(string name, int numberOfStates, string path = "Assets/Thuleanx/StateMachine/") {
		if (numberOfStates > 0) {
			var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(path + name + ".controller");


			controller.AddParameter("state", AnimatorControllerParameterType.Int);

			var rootStateMachine = controller.layers[0].stateMachine;

			UnityEditor.Animations.AnimatorState[] states = new UnityEditor.Animations.AnimatorState[numberOfStates];

			for (int i = 0; i < numberOfStates; i++)
			{
				states[i] = rootStateMachine.AddState(i.ToString());
				var transition = rootStateMachine.AddAnyStateTransition(states[i]);
				transition.AddCondition(UnityEditor.Animations.AnimatorConditionMode.Equals, i, "state");
				transition.duration = 0;
				transition.canTransitionToSelf = false;
			}
			rootStateMachine.AddEntryTransition(states[0]);
		} else
			Debug.Log(numberOfStates + " needs to be a possitive integer");
	}
}

