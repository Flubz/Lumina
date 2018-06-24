using System.Collections;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Seeker))]
public class EnemyAI : MonoBehaviour
{
	[HideInInspector] public bool _pathIsEnded = false;

	[SerializeField] float _updateRate = 2f;
	[SerializeField] float _speed = 4f;
	[SerializeField] float _nextWaypointDist = 3f;
	[SerializeField] float _angleOffset = 45f;
	[SerializeField] float _rotSpeed = 45f;

	Path _path;
	Seeker _seeker;
	Rigidbody _rb;
	Transform _target;

	[SerializeField] Light _lightA;
	[SerializeField] Light _lightB;
	[SerializeField] Transform _detector;

	float _lightAIntensity;
	float _lightBIntensity;

	int _currentWaypoint = 0;

	void Awake ()
	{
		_seeker = GetComponent<Seeker> ();
		_rb = GetComponent<Rigidbody> ();
		_lightAIntensity = _lightA.intensity;
		_lightBIntensity = _lightB.intensity;
		StopFollowingPlayer ();
	}

	public void StartFollowingPlayer (Transform target_)
	{
		_target = target_;
		if (_target == null) return;

		_seeker.StartPath (transform.position, _target.position, OnPathComplete);

		_lightA.DOIntensity (_lightAIntensity, 1.0f);
		_lightB.DOIntensity (_lightBIntensity, 1.0f);

		StartCoroutine (UpdatePath ());
	}

	public void StopFollowingPlayer ()
	{
		_target = null;
		_rb.velocity = Vector3.zero;
		_lightA.DOIntensity (0, 1.0f);
		_lightB.DOIntensity (0, 1.0f);
	}

	// IEnumerator SearchForPlayer ()
	// {
	// 	GameObject sResult = GameObject.FindGameObjectWithTag ("Player");
	// 	if (sResult == null)
	// 	{
	// 		yield return new WaitForSeconds (0.5f);
	// 		StartCoroutine (SearchForPlayer ());
	// 	}
	// 	else
	// 	{
	// 		_target = sResult.transform;
	// 		_searchingForPlayer = false;
	// 		StartCoroutine (UpdatePath ());
	// 		yield return false;
	// 	}
	// }

	IEnumerator UpdatePath ()
	{
		if (_target != null)
		{
			_seeker.StartPath (transform.position, _target.position, OnPathComplete);
			yield return new WaitForSeconds (_updateRate);
			StartCoroutine (UpdatePath ());
		}
	}

	public void OnPathComplete (Path p)
	{
		if (p.error) gameObject.SetActive (false);

		if (!p.error)
		{
			_path = p;
			_currentWaypoint = 0;
		}
	}

	void FixedUpdate ()
	{
		if (_target == null) return;

		if (_path == null)
			return;

		if (_currentWaypoint >= _path.vectorPath.Count)
		{
			_pathIsEnded = true;
			return;
		}

		_pathIsEnded = false;

		Vector3 dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
		dir.y = 0;
		_rb.velocity = dir * _speed * Time.fixedDeltaTime;

		float dist = Vector3.Distance (transform.forward, _path.vectorPath[_currentWaypoint]);
		if (dist < _nextWaypointDist)
		{
			_currentWaypoint++;
			return;
		}
	}
}