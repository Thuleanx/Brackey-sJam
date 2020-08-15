
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Condition))]
public class Dissolve : MonoBehaviour
{
	Material material;
	Condition condition;
	bool transed;

	float fade;
	[SerializeField] float dissolveRate = .5f;
	[SerializeField] float maxDissolve = 1.1f;

	void Awake() {
		condition = GetComponent<Condition>();
		material = GetComponent<SpriteRenderer>().material;
	}

	void OnEnable() {
		fade = 0;
	}

	void Update() {
		if (!condition.dead)
			fade += Time.deltaTime * dissolveRate;
		else fade -= Time.deltaTime * dissolveRate;
		fade = Mathf.Clamp(fade, 0, maxDissolve);

		material.SetFloat("_Fade", fade);

		if (fade == 0 && !CompareTag("Player")) {
			GetComponent<RewardPlayer>()?.GiveReward();
			GetComponent<RewardPlayer>()?.DisableMinion();
			gameObject.SetActive(false);
		} else if (fade == 0 && !transed) {
			transed = true;
			TransitionManager.Instance.LoadFirst();
		}
	}
}

