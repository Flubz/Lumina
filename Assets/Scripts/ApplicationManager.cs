using UnityEngine;
using UnityEngine.EventSystems;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Singleton that manages the application. Primarily for scene management.

public class ApplicationManager : MonoBehaviour
{
	public static ApplicationManager instance = null;
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
		// SceneManager.sceneLoaded += OnSceneLoaded;
		AudioManager.instance.Play ("Ambience_1");
	}

}