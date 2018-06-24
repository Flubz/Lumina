using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Singleton that manages the application. Primarily for scene management.

public class ApplicationManager : MonoBehaviour
{
	public static ApplicationManager instance = null;
	[HideInInspector] public bool _gameStarted;
	[SerializeField] CanvasGroup _canvas;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	private void Start ()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		AudioManager.instance.Play ("Ambience_1");
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		_gameStarted = false;
		FindObjectOfType<PlayerLight> ()._Invincible = true;
		_canvas.
	}

	public void OnGameStarted (PlayerMovement pm_)
	{
		_gameStarted = true;
		pm_._pl._Invincible = false;
	}

}