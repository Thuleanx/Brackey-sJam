using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Hitbox : MonoBehaviour
{
	public Status status;
	BoxCollider2D box;	

	[SerializeField] LayerMask hurtboxMask;
	[SerializeField] float damageFrequency;
	[SerializeField] float damageMultiplier = 1f;

	Dictionary<Hurtbox, float> hitlast = new Dictionary<Hurtbox, float>();	

	static int maxHitboxResults = 10;

	void Start() {
		box = GetComponent<BoxCollider2D>();
		if (status == null)
			status = GetComponentInParent<Status>();
	}

	public void AttachStatus(Status status) {
		this.status = status;
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
				float dmg = status.damage * damageMultiplier;

				bool crit = Thuleanx.Random.Range(0, 1) < status.critRate;

				if (crit)
					dmg *= status.critMultiplier;

				float dmgDealt = hurtbox.RegisterHit(dmg, crit, this);
				if (dmgDealt != 0) {
					status.OnHit(hurtbox, dmgDealt, crit);
					hitlast[hurtbox] = Time.time;
				}
			}
		}
	}
}