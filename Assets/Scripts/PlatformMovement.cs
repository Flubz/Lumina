using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
	[SerializeField] float _moveRadius = 2.0f;
	[SerializeField] float _moveSpeed = 1.0f;
	// [Tooltip ("Global axis of platform movement.")]
	// [SerializeField] Direction _movementDirection;

	Vector3 _movementAxis;
	public float _damping = 5f;
	Vector3[] _targetLocations = new Vector3[2];
	bool _goingRight;
	Vector3 _targetPos = new Vector3 ();

	// void Start ()
	// {
	// 	// _targetPos = Vector3.zero;
	// 	// _targetLocations[0] = (transform.position.x + _moveRadius);
	// 	// _targetLocations[1] = (transform.position.x + _moveRadius * -1);
	// }

	// void LateUpdate ()
	// {

	// 	if (_goingRight && transform.position.x >= _targetLocations[0].x)
	// 	{
	// 		_targetPos = _targetLocations[0];
	// 	}
	// 	else if (transform.position.x <= _targetLocations[1].x)
	// 	{
	// 		_targetPos = _targetLocations[1];

	// 	}

	// 	transform.position = Vector3.Lerp (transform.position, _targetPos, _damping * Time.deltaTime);
	// }

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