
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance;

	[SerializeField] public float inputBufferTimeSeconds = .2f;

	[HideInInspector] public Vector2 axisInput;
	[HideInInspector] public bool jumpDown, jumpRelease, jump;

	[HideInInspector]
	public Timers timers;
	[HideInInspector]
	public IncrementalTimers itimers;
	

	void Awake() {
		InitTimers();
		Instance = this;
	}

	public void InitTimers() {
		timers = new Timers();
		itimers = new IncrementalTimers();

		timers.RegisterTimer("jumpBuffer");
	}

	public void RegisterInput() {
		axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		jumpDown = Input.GetButtonDown("Jump");
		jump = Input.GetButton("Jump");
		jumpRelease = Input.GetButtonUp("Jump");

		if (jumpDown) 
			timers.StartTimer("jumpBuffer", inputBufferTimeSeconds);
	}

	public void DecrementTimers() {
	}

	void LateUpdate() {
		RegisterInput();
	}
}
