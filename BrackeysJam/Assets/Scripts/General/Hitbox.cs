using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hitbox : MonoBehaviour
{
	Status status;
	BoxCollider2D box;	

	[SerializeField] LayerMask hurtboxMask;
	[SerializeField] float damageFrequency;

	Dictionary<Hurtbox, float> hitlast = new Dictionary<Hurtbox, float>();	

	static int maxHitboxResults = 10;

	void Start() {
		box = GetComponent<BoxCollider2D>();
		status = GetComponentInParent<Status>();
	}

	public List<Hurtbox> GetOverlappingHurtbox() {
		List<Hurtbox> results = new List<Hurtbox>();		
		Collider2D[] receiver = new Collider2D[maxHitboxResults];

		ContactFilter2D filter = new ContactFilter2D();
		filter.layerMask = hurtboxMask;
		filter.useLayerMask = true;

		int count = box.OverlapCollider(filter, receiver);

		for (int i = 0; i < count; i++) {
			// guaranteed to have hurtbox
			results.Add(receiver[i].GetComponent<Hurtbox>());
		}

	
		return results;
	}

	void OnDisable() {
		hitlast = new Dictionary<Hurtbox, float>();
	}

	void Update() {
		List<Hurtbox> hurtboxes = GetOverlappingHurtbox();

		foreach (Hurtbox hurtbox in hurtboxes) {
			if (!hitlast.ContainsKey(hurtbox) || (damageFrequency > 0 && Time.time - hitlast[hurtbox] >= 1 / damageFrequency)) {
				if (hurtbox.RegisterHit(status.damage))
					hitlast[hurtbox] = Time.time;
			}
		}
	}
}