using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Game controller for controlling the game's rooms etc
/// Spawned in at the start of the first level and not destroyed on scene changes
/// Manually destroyed when going to the game over screen
/// </summary>
public class GameController : MonoBehaviour {
	// time limit in seconds for how long the player has until gameover
	private float timeLimit = 125;

	// bool for checking if the countdown has started yet (timer starts inside the queen's house once the battery app has been talked to)
	private bool timerStarted = false;

	// static variable for the specific gamecontroller object (used in static methods)
	public static GameController gameController;

	// bool for if the game is currently paused or not
	public static bool gamePaused = false;

	// timer and pause screen objects that are manipulated later
	private Text timer;
	private GameObject timerCanvas;
	private Image pauseImage;

	// list of scenes by name/string and the currently active scene
	// 0 = main menu
	// 1 = game over
	// 2 - 9 the actual gameworld scenes
	private List<string> sceneList;
	private int activeSceneNumber;

	/// <summary>
	/// Starts the timelimit countdown
	/// </summary>
	public static void StartTimer ()
	{
		// save the timer to started position
		gameController.timerStarted = true;

		// turn on the timer canvas so it shows
		gameController.timerCanvas.SetActive (true);
	}

	/// <summary>
	/// Advances the gameworld scene to the next
	/// </summary>
	public static void AdvanceScene ()
	{
		// loads the next scene from the list
		SceneManager.LoadSceneAsync (gameController.sceneList [gameController.activeSceneNumber + 1]);
	}

	/// <summary>
	/// End the game and go to the gameover scene
	/// </summary>
	public static void GameOver ()
	{
		// load the gameover scene from the list
		SceneManager.LoadSceneAsync (gameController.sceneList [1]);

		// destroy the inventory canvas and all its children (= the whole inventory system)
		Destroy (GameObject.Find ("InventoryCanvas"));
	}

	/// <summary>
	/// Pauses or unpauses the game and shows the pause menu
	/// </summary>
	public void PauseUnPauseGame ()
	{
		// reverse the gamepaused variable
		GameController.gamePaused = !GameController.gamePaused;

		// show/hide the pause menu image
		pauseImage.enabled = (GameController.gamePaused);

		// show/hide the timer
		timerCanvas.SetActive (!GameController.gamePaused && timerStarted);

		// (de)activate the inventory
		InventoryController.inventoryCanvas.SetActive (!GameController.gamePaused);
	}

	/// <summary>
	/// Turn off self-destruct on scene changes
	/// </summary>
	void Awake ()
	{
		// make the gameobject persistent
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		// set the active scene to the first gameworld scene
		activeSceneNumber = 2;

		// save the gamecontroller static variable to this script
		gameController = this;

		// save all the UI objects of different kinds from the child-tree
		timerCanvas = gameObject.transform.GetChild (0).gameObject;
		timer = timerCanvas.transform.GetChild (1).GetComponent<Text> ();
		pauseImage = gameObject.transform.GetChild (1).GetChild (0).GetComponent<Image> ();

		// give the pausebutton its listener for pausing/unpausing
		Button pauseButton = gameObject.transform.GetChild (2).GetChild (0).GetComponent<Button> ();
		pauseButton.onClick.AddListener (() => PauseUnPauseGame ());

		// create the scenelist
		sceneList = new List<string> ();

		// add ALL the scenes in the game to the list
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
		// check if in the mainmenu or gameover scenes for self-destruction
		if (SceneManager.GetActiveScene ().name == sceneList[0] || SceneManager.GetActiveScene ().name == sceneList[1])
		{
			// destroy self
			Destroy (gameObject);
		}

		// check if the countdown has started and the game is not paused
		if (timerStarted && !GameController.gamePaused)
		{
			// reduce the time left by the frame time
			timeLimit -= Time.deltaTime;

			// update the timer's text (format xx:yy where xx = minutes and yy = seconds)
			timer.text = ((int) timeLimit / 60).ToString ("00") + ":" + ((int) timeLimit % 60).ToString ("00");
		}

		// check if out of time
		if (timeLimit <= 0)
		{
			// force the game to end
			GameController.GameOver ();
		}
	}
}