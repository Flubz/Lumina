using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public PlayerLight _playerLight;
	[SerializeField] float _lightDiminishRate = 1.0f;
	[SerializeField] Light _spotLight;

	private void Start ()
	{
		StartCoroutine (LightDiminish ());
		_playerLight.LightGone += DeathEvent;
	}

	IEnumerator LightDiminish ()
	{
		while (true)
		{
			_playerLight.LightLoss (_lightDiminishRate);
			_spotLight.spotAngle = _playerLight._GetLight;
			yield return new WaitForSeconds (1.0f);
		}
	}

	void DeathEvent ()
	{
		ApplicationManager.instance.ReloadScene();
	}
	
	

}