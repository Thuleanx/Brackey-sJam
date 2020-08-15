using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCondition))]
public class DirtParticleManager : MonoBehaviour
{
	[SerializeField] ParticleSystem landingDust;
	[SerializeField] ParticleSystem[] jumpingDust;
	[SerializeField] float cameraShakeConstant = .05f;
	bool lastFrameOnGround = false;

	RaycastCollider2D controller;
	PlayerCondition condition;

	void Awake() {
		controller = GetComponent<RaycastCollider2D>();
		condition = GetComponent<PlayerCondition>();
	}

	void LateUpdate() {
		if (lastFrameOnGround != controller.CombinedInfo.AnyBot && !lastFrameOnGround) {
			landingDust.Play();
			CameraShake.Instance?.IncreaseTrauma(cameraShakeConstant);
		}
		lastFrameOnGround = controller.CombinedInfo.AnyBot;
	}

	public void PlayJump() {
		foreach (var system in jumpingDust)
			system.Play();
	}
}
