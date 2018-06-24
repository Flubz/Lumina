using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField] Rigidbody _rb;
	[SerializeField] float _moveSpeed;
	[SerializeField] float _maxVelocity;
	[SerializeField] float _rotSpeed;
	[SerializeField] float _rotOffset;
	[Space (1.0f)]

	[Header ("Dash Settings")]
	[SerializeField] Transform _orb;
	[SerializeField] float _maxDashDistance;
	[SerializeField] float _orbMoveSpeed;
	[SerializeField] Ease _dashEase;
	[SerializeField] float _dashRate = 2.0f;
	float _timeUntilNextDash;

	bool _shiftPressed;
	Vector3 tempVec;
	[SerializeField] AudioSource _audioSource;
	[SerializeField] float _audioFadeTimeStart = 1.0f;
	[SerializeField] float _audioFadeTimeEnd = 1.0f;
	[SerializeField] float _audioEffectMaxVolume = 0.2f;

	void Start ()
	{
		_orb.gameObject.SetActive (false);
		_audioSource.Play ();
	}

	private void FixedUpdate ()
	{
		// if (_shiftPressed) return;
		if (GetRotationVector ().magnitude > 0.4f)
		{
			if (_audioSource.volume == 0) _audioSource.DOFade (_audioEffectMaxVolume, _audioFadeTimeStart);
			transform.RotateTowardsVector (GetRotationVector (), _rotSpeed, _rotOffset);
			tempVec = (transform.forward * _moveSpeed * Time.fixedDeltaTime);
			_rb.velocity = new Vector3 (tempVec.x, _rb.velocity.y, tempVec.z);
			if (_rb.velocity.magnitude >= _maxVelocity) _rb.velocity = _rb.velocity * _maxVelocity;
		}
		else
		if (_audioSource.volume != 0) _audioSource.DOFade (0.0f, _audioFadeTimeEnd);
	}

	Vector3 GetRotationVector ()
	{
		return new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0);
	}

	// void Update ()
	// {
	// 	CheckForDash ();
	// }

	// void CheckForDash ()
	// {
	// 	if (Time.time > _timeUntilNextDash && _dashRate != 0)
	// 	{
	// 		if (Input.GetButtonDown ("Jump") && !_orb.gameObject.activeSelf)
	// 			LaunchOrb ();
	// 		else if (Input.GetButtonDown ("Jump") && _orb.gameObject.activeSelf)
	// 			TeleportToOrb ();
	// 		_timeUntilNextDash = Time.time + 1 / _dashRate;
	// 	}

	// }

	// void LaunchOrb ()
	// {
	// 	_orb.gameObject.SetActive (true);
	// 	_shiftPressed = true;
	// 	_orb.position = transform.position;

	// 	Vector3 telePos = transform.position + transform.forward * _maxDashDistance;

	// 	Tweener t = _orb.DOMove (telePos, _orbMoveSpeed).SetEase (_dashEase);
	// 	t.OnComplete (DisableGO);
	// }

	// void DisableGO ()
	// {
	// 	_orb.gameObject.SetActive (false);
	// }

	// void TeleportToOrb ()
	// {
	// 	transform.position = _orb.position;

	// 	_shiftPressed = false;
	// 	_orb.gameObject.SetActive (false);
	// }

}