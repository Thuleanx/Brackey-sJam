
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCondition))]
public class WallMovement : MonoBehaviour
{
	#region Component
	PlayerCondition condition;
	#endregion

	#region Wall Movement Constants
	[SerializeField] float wallGlideSpeed = 3;
	[SerializeField] float wallGlideHangSeconds = 2f;

	[SerializeField] Vector2 wallJumpApex;
	[SerializeField] Vector2 wallLeapApex;
	#endregion

	#region Movement Conditions
	[HideInInspector]
	public bool CanWallHang, CanWallGlide, CanWallJump;
	#endregion	

	Vector2 wallJumpVelocity, wallLeapVelocity;
	float wallJumpTime, wallLeapTime;

	public void CalculateConstants(float gravity) {
		wallJumpTime = Mathf.Sqrt(2 * wallJumpApex.y / gravity);
		wallJumpVelocity.y = gravity * wallJumpTime;
		wallJumpVelocity.x = wallJumpApex.x * gravity / wallJumpVelocity.y;

		wallLeapTime = Mathf.Sqrt(2 * wallLeapApex.y / gravity);
		wallLeapVelocity.y = gravity * wallLeapTime;
		wallLeapVelocity.x = wallLeapApex.x * gravity / wallLeapVelocity.y;
	}

	public void WallGrip(ref Vector2 velocity) {
		velocity.y = 0;	
	}

	public void WallGlide(ref Vector2 velocity) {
		velocity.y = Mathf.Clamp(velocity.y, -wallGlideSpeed, float.MaxValue);
	}

	public void WallJump(ref Vector2 velocity) {
		velocity = wallJumpVelocity;
		velocity.x *= -condition.faceDir;
	}

	public void WallLeap(ref Vector2 velocity) {
		velocity = wallLeapVelocity;
		velocity.x *= condition.faceDir;
	}

	void LateUpdate() {
		if (condition.onGround)
			condition.itimers.StartTimer("wallGlideHang", wallGlideHangSeconds);
		ConditionUpdate();
	}

	void ConditionUpdate() {
		CanWallHang = condition.onWall && !condition.itimers.Expired("wallGlideHang") && InputManager.Instance.axisInput.y >= 0;
		CanWallGlide = condition.onWall;
		CanWallJump = !condition.timers.Expired("wallJumpBuffer") && InputManager.Instance.timers.ActiveAndNotExpired("jumpBuffer") && 
			Mathf.Approximately(condition.wallDir, condition.faceDir);
	}
}
