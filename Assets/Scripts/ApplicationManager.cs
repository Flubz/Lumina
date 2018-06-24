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
	[SerializeField] CanvasGroup _cg;

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
		_gameStarted = false;
	}

	void StartSettings ()
	{

	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode)
	{
		StartSettings ();
	}

	private void Update ()
	{
		if (!_gameStarted)
		{
			if (Input.GetButtonDown ("Jump"))
			{
				
			}
		}
	}

}