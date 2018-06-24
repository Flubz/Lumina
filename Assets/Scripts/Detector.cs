using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
	[SerializeField] EnemyAI _enemyAI;

	private void OnTriggerEnter (Collider other)
	{
		StopAllCoroutines ();
		StartCoroutine (StartTimer (other));
	}

	IEnumerator StartTimer (Collider other)
	{
		yield return new WaitForSeconds (0.1f);
		if (other.gameObject.CompareTag ("Player"))
		{
			_enemyAI.StartFollowingPlayer (other.transform);
		}
	}

	private void OnTriggerExit (Collider other)
	{
		StopAllCoroutines ();
		StartCoroutine (StopTimer ());
	}

	IEnumerator StopTimer ()
	{
		yield return new WaitForSeconds (1.0f);
		_enemyAI.StopFollowingPlayer ();

	}
}