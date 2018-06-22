using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField] Rigidbody _rb;
	[SerializeField] float _moveSpeed;
	[SerializeField] float _rotSpeed;
	[SerializeField] float _rotOffset;

	private void FixedUpdate ()
	{
		if (GetRotationVector ().magnitude > 0.4f)
		{
			transform.RotateTowardsVector (GetRotationVector (), _rotSpeed, _rotOffset);
			_rb.velocity = (transform.forward * Time.deltaTime * _moveSpeed);
		}
	}

	Vector3 GetRotationVector ()
	{
		return new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"), 0);
	}

}