
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance;

	[SerializeField] public float inputBufferTimeSeconds = .2f;
	[SerializeField] public float attackInputBufferTimeSeconds = .1f;

	[HideInInspector] public Vector2 axisInput;
	[HideInInspector] public int jump, interact;

	[HideInInspector] 
	public int  S1, S2, S3, S4;

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
		timers.RegisterTimer("S1Buffer");
		timers.RegisterTimer("S2Buffer");
		timers.RegisterTimer("S3Buffer");
		timers.RegisterTimer("S4Buffer");
	}

	public void RegisterInput() {
		axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		jump = GetKey("Jump");
		interact = GetKey("Interact");
		S1 = GetKey("S1");
		S2 = GetKey("S2");
		S3 = GetKey("S3");
		S4 = GetKey("S4");

		if (KeyDown(jump)) 
			timers.StartTimer("jumpBuffer", inputBufferTimeSeconds);
		if (KeyPress(S1))
			timers.StartTimer("S1Buffer", attackInputBufferTimeSeconds);
		if (KeyPress(S2))
			timers.StartTimer("S2Buffer", attackInputBufferTimeSeconds);
		if (KeyPress(S3))
			timers.StartTimer("S3Buffer", attackInputBufferTimeSeconds);
		if (KeyPress(S4))
			timers.StartTimer("S4Buffer", attackInputBufferTimeSeconds);
	}

	static int GetKey(string name) {
		return 	(Input.GetButton(name) ? 1 : 0) +
				(Input.GetButtonDown(name) ? 1 : 0) * 2 +
				(Input.GetButtonUp(name) ? 1 : 0) * 4;
	}

	public static bool KeyPress(int key) {
		return (key & 1) == 1;
	}

	public static bool KeyDown(int key) {
		return ((key >> 1) & 1) == 1;
	}

	public static bool KeyUp(int key) {
		return ((key >> 2) & 1) == 1;
	}

	public void DecrementTimers() {
	}

	void LateUpdate() {
		RegisterInput();
	}
}
