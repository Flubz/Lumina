using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraLight : MonoBehaviour
{
	[SerializeField] Light _camLight;

	private bool _mouseState;
	private Vector3 target;
	public Vector3 screenSpace;
	public Vector3 offset;
	[SerializeField] LayerMask _layers;
	Vector3 _camLightVec;
	[SerializeField] Player _player;
	[SerializeField] float _lightLossAmount = 1.0f;
	[SerializeField] float _lightLossConstant = 1.0f;


	// Update is called once per frame
	void Update ()
	{
		// Debug.Log(_mouseState);
		if (Input.GetMouseButtonDown (0))
		{

			RaycastHit hitInfo;
			target = GetClickedObject (out hitInfo);
			if (target != null)
			{
				_mouseState = true;
				screenSpace = Camera.main.WorldToScreenPoint (target);
				offset = target - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
				_camLightVec = new Vector3 (target.x, target.y, transform.position.z);
				_camLight.DOIntensity (1.4f, 1.0f);
				_player._playerLight.LightLoss (_lightLossAmount);
				StartCoroutine (LightDiminish ());
			}
		}

		if (Input.GetMouseButtonUp (0))
		{
			_mouseState = false;
			_camLight.DOIntensity (0, 1.0f);
		}

		if (_mouseState)
		{
			//keep track of the mouse position
			var curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

			//convert the screen mouse position to world point and adjust with offset
			var curPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;
			curPosition.y = transform.position.y;
			_camLight.transform.position = curPosition;
		}
	}

	IEnumerator LightDiminish ()
	{
		while (_mouseState)
		{
			_player._playerLight.LightLoss (_lightLossConstant);
			yield return new WaitForSeconds (1.0f);
		}
	}

	Vector3 GetClickedObject (out RaycastHit hit)
	{
		Vector3 tempVec = Vector3.zero;
		GameObject target = null;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction * 10, out hit, _layers))
		{
			target = hit.collider.gameObject;
			tempVec = hit.point;
		}

		return tempVec;
	}

}