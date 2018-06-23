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

	bool _movementLocked;

	void Start ()
	{
		_orb.gameObject.SetActive (false);
	}

	private void FixedUpdate ()
	{
		// if (_movementLocked) return;
		if (GetRotationVector ().magnitude > 0.4f)
		{
			transform.RotateTowardsVector (GetRotationVector (), _rotSpeed, _rotOffset);
			_rb.velocity = (transform.forward * _moveSpeed * Time.fixedDeltaTime) + (_rb.velocity / 100);
			if (_rb.velocity.magnitude >= _maxVelocity) _rb.velocity = _rb.velocity * _maxVelocity;
		}
	}

	Vector3 GetRotationVector ()
	{
		return new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0);
	}

	void Update ()
	{
		CheckForDash ();
	}

	void CheckForDash ()
	{
		if (Time.time > _timeUntilNextDash && _dashRate != 0)
		{
			if (Input.GetButtonDown ("Jump") && !_orb.gameObject.activeSelf)
				LaunchOrb ();
			else if (Input.GetButtonDown ("Jump") && _orb.gameObject.activeSelf)
				TeleportToOrb ();
			_timeUntilNextDash = Time.time + 1 / _dashRate;
		}

	}

	void LaunchOrb ()
	{
		Debug.Log("ASD");
		_orb.gameObject.SetActive (true);
		_movementLocked = true;
		_orb.position = transform.position;

		Vector3 telePos = transform.position + transform.forward * _maxDashDistance;

		Tweener t = _orb.DOMove (telePos, _orbMoveSpeed).SetEase (_dashEase);
		t.OnComplete (DisableGO);
	}

	void DisableGO ()
	{
		_orb.gameObject.SetActive (false);
	}

	void TeleportToOrb ()
	{
		transform.position = _orb.position;

		_movementLocked = false;
		_orb.gameObject.SetActive (false);
	}

}