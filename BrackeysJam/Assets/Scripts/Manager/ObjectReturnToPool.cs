using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturnToPool : MonoBehaviour
{
	public string sourceTag;

	void OnDisable() {
		ObjectPool.Instance.Return(sourceTag, gameObject);
	}
}