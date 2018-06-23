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
	[Header ("Dash Settings")]
	[SerializeField] Transform _orb;
	[SerializeField] float _maxDashDistance;
	[SerializeField] float _orbMoveSpeed;
	[SerializeField] Ease _dashEase;

	bool _movementLocked;

	void Start ()
	{
		_orb.gameObject.SetActive (false);
	}

	private void FixedUpdate ()
	{
		if (_movementLocked) return;
		if (GetRotationVector ().magnitude > 0.4f)
		{
			transform.RotateTowardsVector (GetRotationVector (), _rotSpeed, _rotOffset);
			_rb.AddForce (transform.forward * _moveSpeed, ForceMode.Force);
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
		if (Input.GetButtonDown ("Jump"))
			LaunchOrb ();

		if (Input.GetButtonUp ("Jump"))
			TeleportToOrb ();

	}

	void LaunchOrb ()
	{
		_orb.gameObject.SetActive (true);
		_movementLocked = true;
		_orb.position = transform.position;

		Vector3 telePos = transform.position + transform.forward * _maxDashDistance;

		_orb.DOMoveX (telePos.x, _orbMoveSpeed).SetEase (_dashEase);
		_orb.DOMoveZ (telePos.z, _orbMoveSpeed).SetEase (_dashEase);
	}

	void TeleportToOrb ()
	{
		transform.position = _orb.position;

		_movementLocked = false;
		_orb.gameObject.SetActive (false);
	}

}