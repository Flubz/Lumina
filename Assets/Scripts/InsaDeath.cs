using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsaDeath : MonoBehaviour
{

	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			Player p = other.gameObject.FindComponent<Player> ();
			if (p) p._playerLight.LightLoss (Mathf.Infinity);
		}
	}
}