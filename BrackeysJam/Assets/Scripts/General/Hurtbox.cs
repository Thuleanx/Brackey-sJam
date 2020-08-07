using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour
{
	// Attached to a status
	Status status;
	Condition condition;
	BoxCollider2D box;
	Timers timers;

	void Awake() {
		timers = new Timers();
		timers.RegisterTimer("iframe");
	}

	void Start()
	{
		box = GetComponent<BoxCollider2D>();
		condition = GetComponentInParent<Condition>();
		status = GetComponentInParent<Status>();
	}

	public float RegisterHit(float damage, Hitbox hitbox)
	{
		if (canBeHit()) {
			status.DealDamage(damage);
			if (status.Health == 0)
				condition.dead = true;
			status.OnGettingHit(hitbox);
			return damage;
		}
		return 0;
	}

	public void GiveIframe(float duration) {
		timers.StartTimer("iframe", duration);
	}

	public bool canBeHit()
	{
		return !timers.ActiveAndNotExpired("iframe");
	}
}