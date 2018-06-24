using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] Transform _textGO;

	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			Player p = other.gameObject.FindComponent<Player> ();
			if (p)
			{
				_textGO.DOScale (1.0f, 1.0f);
				StartCoroutine (GameOver ());
			}
		}
	}

	IEnumerator GameOver ()
	{
		yield return new WaitForSeconds (10.0f);
		ApplicationManager.instance.ReloadScene ();
	}
}