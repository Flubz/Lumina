using System;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
	float _light;
	bool _hasNoLight;

	public float _GetLight { get { return _light; } }

	[SerializeField]
	[Range (1, 1000)]
	[Tooltip ("The Initial Light of the GameObject.")]
	private float _initialLight;

	[SerializeField]
	[Range (1, 1000)]
	[Tooltip ("The Maximum Light of the GameObject.")]
	private float _maxLight;
	public float _GetMaxLight { get { return _maxLight; } }

	[SerializeField]
	[Tooltip ("Show Light Debug Logs in console.")]
	private bool _debug;

	[SerializeField]
	[Tooltip ("Prevents LightLoss to GameObject.")]
	private bool _invincible;
	public bool _Invincible { get { return _invincible; } set { _invincible = value; } }

	/// <summary>
	/// When the Light reaches 0 any methods that are subscribed will be called.
	/// </summary>
	public event Action LightGone;

	/// <summary>
	/// Called when the GameObject is LightLossd.
	/// </summary>
	public event Action LightLostEvent;

	public float _GetLightPercent
	{
		get
		{
			if (_maxLight != 0) return (float) _light / (float) _maxLight;
			else Debug.LogError ("Max Light is 0!");
			return 0;
		}
	}

	void Start ()
	{
		Reset ();
	}

	/// <summary>
	/// Adds to the Light.
	/// </summary>
	public void LightGain (float _LightGainValue)
	{
		_light += Mathf.Abs (_LightGainValue);
		if (_light > _maxLight) _light = _maxLight;
		if (_debug) Debug.Log (gameObject.name + " LightGained: " + _LightGainValue + " | Light: " + _light);
	}

	/// <summary>
	/// Takes away from the Light.
	/// </summary>
	public void LightLoss (float _LightLossValue)
	{
		if (_invincible) return;
		_light -= Mathf.Abs (_LightLossValue);
		if (_light < 0) _light = 0;
		if (_debug) Debug.Log (gameObject.name + " LightLost: " + _LightLossValue + " | Light: " + _light);
		if (LightLostEvent != null) LightLostEvent ();
		if (hasNoLight () && LightGone != null) LightGone ();
	}

	/// <summary>
	/// Returns true if the Light is less than or equal to 0.
	/// </summary>
	public bool hasNoLight ()
	{
		if (_light <= 0) _hasNoLight = true;
		return _hasNoLight;
	}

	/// <summary>
	/// Resets the Light component to its orignal values.
	/// </summary>
	public void Reset ()
	{
		_hasNoLight = false;
		_light = _initialLight;
	}

	private void OnValidate ()
	{
		if (_initialLight <= 0)
			_initialLight = 1;

		if (_maxLight < _initialLight)
			_maxLight = _initialLight;
	}

}