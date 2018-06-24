using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
	public static void RotateTowardsVector (this Transform trans_, Vector2 vel_, float rotSpeed_ = 4.0f, float angleOffset_ = 0.0f)
	{
		float angle = Mathf.Atan2 (vel_.y, vel_.x) * Mathf.Rad2Deg;
		trans_.rotation = Quaternion.Lerp (trans_.rotation, Quaternion.AngleAxis (angle + angleOffset_, Vector3.up * -1), Time.deltaTime * rotSpeed_);
	}

	public static T RandomFromList<T> (this List<T> list_)
	{
		return list_[UnityEngine.Random.Range (0, list_.Count)];
	}

	public static T FindComponent<T> (this GameObject go_)
	{
		if (go_.GetComponent<T> () != null) return go_.GetComponent<T> ();
		else
		{
			Debug.LogError ("Compoenent of Type " + typeof (T).GetType ().ToString () + " not found! ");
			return default (T);
		}
	}

}