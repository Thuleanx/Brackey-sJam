using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RaycastCollider2D))]
[RequireComponent(typeof(PlayerCondition))]
[RequireComponent(typeof(PlayerAnimationController))]
public class MovementController : Movement
{
	#region Components
	RaycastCollider2D raycastCollider;
	PlayerCondition condition;
	Status status;
	PlayerAnimationController anim;
	#endregion

	#region Rigid body
	#endregion

	#region Ability
	WallMovement wallMovement;
	#endregion

	#region Physics Constants

	[SerializeField] float platformHeight = .5f;

	[SerializeField] float maxJumpHeight;
	[SerializeField] float minJumpHeight;
	[SerializeField] float timeToJumpApexSeconds;

	[SerializeField] float terminalVelocity = 10;
	[SerializeField] float airSpeedModifier = .8f;
	[SerializeField] float airEasingTime = .5f;

	[HideInInspector]
	public float gravity;

	float jumpVelocityMax;
	float jumpVelocityMin;

	float airAccel = 0;

	#endregion

	#region Imprecisions

	[SerializeField] float coyoteTimeSeconds;
	float platformFallThroughSeconds;

	#endregion

	void Awake() {
		raycastCollider = GetComponent<RaycastCollider2D>();
		condition = GetComponent<PlayerCondition>();
		wallMovement = GetComponent<WallMovement>();
		status = GetComponent<Status>();
		anim = GetComponent<PlayerAnimationController>();
		CalculatePhysicsConstants();
	}

	void Start() {
		wallMovement?.CalculateConstants(gravity);
	}

	void CalculatePhysicsConstants() {
		gravity = 2 * maxJumpHeight / (timeToJumpApexSeconds * timeToJumpApexSeconds);	

		jumpVelocityMax = gravity * timeToJumpApexSeconds;
		jumpVelocityMin = Mathf.Sqrt(minJumpHeight * gravity);

		platformFallThroughSeconds =  Mathf.Sqrt(2 * platformHeight / gravity);
	}

	void ConditionUpdate() {
		condition.onGround = raycastCollider.collisionInfo.AnyBot || raycastCollider.platformCollisionInfo.AnyBot;
		condition.onWall = !condition.onGround && velocity.y <= 0 && (raycastCollider.collisionInfo.AllLeft ||  raycastCollider.collisionInfo.AllRight);
		if (condition.onWall) 
			condition.wallDir = raycastCollider.collisionInfo.AllLeft ? -1 : 1;
	}

	void TimerChecks() {
		if (raycastCollider.collisionInfo.AnyBot || raycastCollider.platformCollisionInfo.AnyBot)
			condition.timers.StartTimer("coyoteBuffer", coyoteTimeSeconds);
		// Jump
		// Potential bug with releasing the jump button possibly cancelling upward momentum. Fix not needed rn
		if (InputManager.Instance.axisInput.y < 0)
			condition.timers.StartTimer("platformFallThrough", platformFallThroughSeconds);

		if (condition.onWall)
			condition.timers.StartTimer("wallJumpBuffer", InputManager.Instance.inputBufferTimeSeconds);
	}

	void Update()
	{
		if (!condition.LockedVelocity) {
			velocity.y -= gravity * Time.deltaTime;

			if (raycastCollider.collisionInfo.AnyTop || (condition.onGround && velocity.y <= 0))
				velocity.y = 0;

			if (InputManager.Instance.axisInput.x != 0)
				condition.faceDir = Mathf.Sign(InputManager.Instance.axisInput.x);

			if (!condition.LockedMovement)
			{
				float speed = status == null ? 3f : status.speed;

				if (condition.onGround)
					velocity.x = InputManager.Instance.axisInput.x * speed;
				else
				{
					velocity.x = InputManager.Instance.axisInput.x * speed * airSpeedModifier;
					// float targetVelX = InputManager.Instance.axisInput.x * moveSpeed * airSpeedModifier;
					// velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref airAccel, airEasingTime);
				}

				// Jump
				if (InputManager.Instance.timers.ActiveAndNotExpired("jumpBuffer") &&
					condition.timers.ActiveAndNotExpired("coyoteBuffer"))
				{
					Jump();
					InputManager.Instance.timers.SetActive("jumpBuffer", false);
					condition.timers.SetActive("coyoteBuffer", false);
				}
				else if (InputManager.KeyUp(InputManager.Instance.jump))
					LowJump();
			}

			// terminal velocity
			velocity.y = Mathf.Clamp(velocity.y, -terminalVelocity, Mathf.Infinity);
		}

		raycastCollider.Move(
			velocity * Time.deltaTime,
			condition.timers.ActiveAndNotExpired("platformFallThrough") && !condition.LockedMovement,
		condition.faceDir);
	}

	void LateUpdate() {
		ConditionUpdate();
		TimerChecks();
		UpdateAnimation();
	}

	void UpdateAnimation() {
		if (!anim.LockedAnimation()) {
			if (condition.onGround && velocity.x == 0) 
				anim.State = PlayerState.Idle;
			else if (condition.onGround) 
				anim.State = PlayerState.Run;
			else if (velocity.y <= -1)
				anim.State = PlayerState.Fall;
			else anim.State = PlayerState.Jump;
		}
	}

	#region Abilities
	void Jump() {
		velocity.y = jumpVelocityMax;

		// if jump button released before touching the ground
		if (!InputManager.KeyPress(InputManager.Instance.jump))
			LowJump();
	}

	void LowJump() {
		velocity.y = Mathf.Min(velocity.y, jumpVelocityMin);
	}

	#endregion

	public void ResetHorizontalVelocity() {
		velocity.x = 0;
	}
}