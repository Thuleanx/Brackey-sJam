
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(PlayerCondition))]
[RequireComponent(typeof(Kaiser))]
public class KaiserDash : Ability {

	Kaiser kaiser;

	[SerializeField] float dashDuration = .2f;
	[SerializeField] Vector2 SDis, UDis;

	float ShorVelocity, SvertVelocity, UhorVelocity, UvertVelocity;

	public override void Awake() {
		base.Awake();
		kaiser = GetComponent<Kaiser>();
	}

	public override void Start() {
		base.Start();
		ShorVelocity = SDis.x / dashDuration;
		SvertVelocity = Mathf.Sqrt(SDis.y * movement.gravity);

		UhorVelocity = UDis.x / dashDuration;
		UvertVelocity = Mathf.Sqrt(UDis.y * movement.gravity);
	}

	public override void Execute() {

		if (kaiser.shealth)
			movement.velocity = new Vector2(
				ShorVelocity, SvertVelocity
			);
		else
			movement.velocity = new Vector2(
				UhorVelocity, UvertVelocity
			);

		movement.velocity.x *= condition.faceDir;
		condition.onGround = false;
		condition.timers.StartTimer("controlTime", dashDuration);


		Vector2 knock = knockback;
		knock.x *= condition.faceDir;
		movement.ApplyKnockback(knock);
	}
}
