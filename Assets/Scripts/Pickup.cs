using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pickup : MonoBehaviour
{

	[SerializeField] float _lightGainAmount = 5.0f;
	[SerializeField] BoxCollider _boxCol;
	[SerializeField] ParticleSystem _ps;
	[SerializeField] Light _essenceLight;
	[SerializeField] AudioSource _as;

	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			Player p = other.gameObject.FindComponent<Player> ();
			if (p) p._playerLight.LightGain (_lightGainAmount);
			DeathEffect ();
		}
	}

	void DeathEffect ()
	{
		_boxCol.enabled = false;
		_ps.Play ();
		_as.Play ();
		transform.DOScale (Vector3.zero, _ps.main.duration + 0.2f).OnComplete (DeleteGO);
		_essenceLight.DOIntensity (0, 1.0f);
	}

	void DeleteGO ()
	{
		Destroy (this.gameObject);
	}

}