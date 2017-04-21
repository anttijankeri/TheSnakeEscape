using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private float timeLimit = 125;
	private bool timerStarted = false;

	public static GameController gameController;
	public static bool gamePaused = false;

	private Text timer;
	private GameObject timerCanvas;
	private Image pauseImage;

	private List<string> sceneList;
	private int activeSceneNumber;

	public static void StartTimer ()
	{
		gameController.timerStarted = true;
		gameController.timerCanvas.SetActive (true);
	}

	public static void AdvanceScene ()
	{
		SceneManager.LoadSceneAsync (gameController.sceneList [gameController.activeSceneNumber + 1]);
	}

	public static void GameOver ()
	{
		SceneManager.LoadSceneAsync (gameController.sceneList [1]);
		Destroy (GameObject.Find ("InventoryCanvas"));
	}

	public void PauseUnPauseGame ()
	{
		GameController.gamePaused = !GameController.gamePaused;

		pauseImage.enabled = (GameController.gamePaused);
		timerCanvas.SetActive (!GameController.gamePaused && timerStarted);
		InventoryController.inventoryCanvas.SetActive (!GameController.gamePaused);
	}

	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		activeSceneNumber = 2;

		gameController = this;

		timerCanvas = gameObject.transform.GetChild (0).gameObject;
		timer = timerCanvas.transform.GetChild (1).GetComponent<Text> ();
		pauseImage = gameObject.transform.GetChild (1).GetChild (0).GetComponent<Image> ();

		Button pauseButton = gameObject.transform.GetChild (2).GetChild (0).GetComponent<Button> ();
		pauseButton.onClick.AddListener (() => PauseUnPauseGame ());

		sceneList = new List<string> ();

		sceneList.Add ("MainMenu");
		sceneList.Add ("GameOver");
		sceneList.Add ("Scene1");
		/*
		sceneList.Add ("Scene2");
		sceneList.Add ("Scene3");
		sceneList.Add ("Scene4");
		sceneList.Add ("Scene5");
		sceneList.Add ("Scene6");
		sceneList.Add ("Scene7");
		sceneList.Add ("Scene8");
		*/
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "GameOver")
		{
			Destroy (GameController.gameController.gameObject);
		}

		if (timerStarted && !GameController.gamePaused)
		{
			timeLimit -= Time.deltaTime;
			timer.text = ((int) timeLimit / 60).ToString ("00") + ":" + ((int) timeLimit % 60).ToString ("00");
		}

		if (timeLimit <= 0)
		{
			GameController.GameOver ();
		}
	}
}