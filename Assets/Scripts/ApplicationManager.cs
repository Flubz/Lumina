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
	[SerializeField] Canvas _canvas;

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
		AudioManager.instance.Play ("Melody");
		OnGameStart ();
	}

	void OnGameStart ()
	{
		_gameStarted = false;
		_canvas.gameObject.SetActive (true);
		Time.timeScale = 0.0f;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		OnGameStart ();
	}

	public void StartGame ()
	{
		_gameStarted = true;
		_canvas.gameObject.SetActive (false);
		Time.timeScale = 1.0f;
	}

	public void ReloadScene ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

}