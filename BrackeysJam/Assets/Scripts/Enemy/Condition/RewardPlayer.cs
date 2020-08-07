using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyStatus))]
public class RewardPlayer : Condition
{
	EnemyStatus status;
	
	[SerializeField] string playerTag = "Player";
	PlayerBank bank;
	PlayerStatus pStatus;

	void Awake() {
		status = GetComponent<EnemyStatus>();

		GameObject player = GameObject.FindGameObjectWithTag(playerTag);

		bank = player.GetComponent<PlayerBank>();
		pStatus = player.GetComponent<PlayerStatus>();
	}

	public void GiveReward() {
		bank.AcquireGold(Mathf.CeilToInt(AssistantDirector.Instance.masterCoef * status.monsterValue));
		pStatus.AcquireXP(Mathf.CeilToInt(2 * AssistantDirector.Instance.masterCoef * status.monsterValue));
	}

	public void DisableMinion() {
		gameObject.SetActive(false);
	}
}
