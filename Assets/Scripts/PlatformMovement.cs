using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
	[SerializeField] float _moveRadius = 2.0f;
	[SerializeField] float _moveDuration = 3.0f;
	Ease _movementEase = Ease.InOutCubic;
	Vector3 _initialPos;

	void Start ()
	{
		_initialPos = transform.localPosition;
		MoveLeft ();
	}

	void MoveLeft ()
	{
		Tweener t = transform.DOMoveZ (_initialPos.z + _moveRadius, _moveDuration);
		t.SetEase (_movementEase);
		t.OnComplete (MoveRight);
	}

	void MoveRight ()
	{
		Tweener t = transform.DOMoveZ (_initialPos.z + _moveRadius * -1, _moveDuration);
		t.SetEase (_movementEase);
		t.OnComplete (MoveLeft);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			transform.DOTogglePause ();
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			transform.DOTogglePause ();
		}
	}

	// void OnValidate ()
	// {
	// 	_movementAxis = DirectionToAxis (_movementDirection);
	// }

	// Vector3 DirectionToAxis (Direction axis_)
	// {
	// 	switch (axis_)
	// 	{
	// 		case Direction.up:
	// 			return Vector3.up;
	// 		case Direction.down:
	// 			return Vector3.down;
	// 		case Direction.left:
	// 			return Vector3.left;
	// 		case Direction.right:
	// 			return Vector3.right;
	// 		case Direction.back:
	// 			return Vector3.back;
	// 		case Direction.forward:
	// 			return Vector3.forward;
	// 	}
	// 	return Vector3.zero;
	// }

	// enum Direction
	// {
	// 	undefined,
	// 	up,
	// 	down,
	// 	left,
	// 	right,
	// 	forward,
	// 	back
	// }

}