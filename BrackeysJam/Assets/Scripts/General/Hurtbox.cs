using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour
{
	// Attached to a status
	Status status;
	BoxCollider2D box;
	Timers timers;

	void Awake() {
		timers = new Timers();
		timers.RegisterTimer("iframe");
	}

	void Start()
	{
		box = GetComponent<BoxCollider2D>();
		status = GetComponentInParent<Status>();
	}

	public bool RegisterHit(float damage)
	{
		if (canBeHit()) {
			status.Health -= damage;
			print("YOU BAD");
			return true;
		}
		return false;
	}

	public void GiveIframe(float duration) {
		timers.StartTimer("iframe", duration);
	}

	public bool canBeHit()
	{
		return !timers.ActiveAndNotExpired("iframe");
	}
}