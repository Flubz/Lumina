using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
	[SerializeField] EnemyAI _enemyAI;

	private void OnTriggerStay (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			_enemyAI.StartFollowingPlayer (other.transform);
		}
	}

	private void OnTriggerExit (Collider other)
	{
		_enemyAI.StopFollowingPlayer ();
	}
}