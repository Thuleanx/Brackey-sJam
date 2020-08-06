
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastCollider2D), typeof(MobCondition), typeof(GroundEnemyAnimator))]
[RequireComponent(typeof(Status))]
public class GroundMovement : MonoBehaviour
{
	#region Component

	RaycastCollider2D controller;
	GameObject player;
	MobCondition condition;
	GroundEnemyAnimator anim;
	Status status;

	#endregion

	#region Rigid Body

	[SerializeField] float jumpHeight = 1.5f, timeToJumpApexSeconds = .2f;
	[SerializeField] float turnAdjustmentDistance = 1f, jumpCoolDownSeconds = 2f;
	float jumpVelocity, gravity, turnCooldownSeconds;

	Vector2 velocity;

	#endregion


	void Awake() {
		controller = GetComponent<RaycastCollider2D>();
		condition = GetComponent<MobCondition>();
		anim = GetComponent<GroundEnemyAnimator>();
		player = GameObject.FindGameObjectWithTag("Player");
		status = GetComponent<Status>();

		CalculatePhysics();
	}

	void Start() {
		turnCooldownSeconds = turnAdjustmentDistance / status.speed;
	}

	void OnEnable() {
		velocity = Vector2.zero;
	}

	void CalculatePhysics() {
		gravity = 2 * jumpHeight / (timeToJumpApexSeconds * timeToJumpApexSeconds);	
		jumpVelocity = gravity * timeToJumpApexSeconds;
	}

	void Update() {

		if (controller.CombinedInfo.AnyBot || controller.CombinedInfo.AnyTop)
			velocity.y = 0;
		
		velocity.y -= gravity * Time.deltaTime;

		if (!condition.LockedMovement) {
			velocity.x = condition.faceDir * status.speed;

			// see player and not the same direction
			if (condition.playerSighted && !Mathf.Approximately(Mathf.Sign(player.transform.position.x- transform.position.x), condition.faceDir)) {
				if (!condition.timers.ActiveAndNotExpired("turnCD"))
					Turn();
			}

			Vector2 dist = velocity * Time.deltaTime;
			// if continue walking results in fall, turn

			Vector2 groundPosCheck = controller.rayCastOrigins.corners[condition.faceDir < 0 ? 0 : 1, 0] + dist;
			if (condition.onGround && TilemapManager.Instance.DistanceToGround(groundPosCheck) > jumpHeight)
				Turn();

			// if stumble onto wall, turn anyway (unless see player, then you jump up)
			if (condition.onWall) {
				if (!condition.playerSighted) Turn();
				else if (!condition.timers.ActiveAndNotExpired("jumpCD"))
					Jump();
			}
		}

		if (condition.dead)
			velocity.x = 0;
			
		controller.Move(velocity * Time.deltaTime, false, condition.faceDir);
	}

	void LateUpdate() {
		ConditionUpdate();
		UpdateAnimation();
	}

	public void UpdateAnimation() {
		if (!condition.LockedMovement && !condition.onGround && velocity.y <= 0) 
			anim.State = GroundEnemyState.Fall;
	}

	public void ConditionUpdate() {
		condition.onGround = controller.CombinedInfo.AnyBot;	
		condition.onWall = controller.CombinedInfo.AnyLeft || controller.CombinedInfo.AnyRight;

		// see player (if player is above it) (-1 for tolerance)
		condition.playerSighted = player.transform.position.y >= controller.body.bounds.min.y - jumpHeight && 
			Mathf.Abs(player.transform.position.x - transform.position.x) > turnAdjustmentDistance / 2;
	}

	void Turn() {
		condition.faceDir *= -1;
		velocity.x = condition.faceDir * status.speed;	
		condition.timers.StartTimer("turnCD", turnCooldownSeconds);
	}

	void Jump() {
		velocity.y = jumpVelocity;
		anim.State = GroundEnemyState.Jump;
		condition.timers.StartTimer("jumpCD", jumpCoolDownSeconds);
	}

	public void ResetHorizontalVelocity() {
		velocity.x = 0;
	}
}
