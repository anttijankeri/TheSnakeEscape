using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Teleports the player to the given scene if all the puzzlegoals in the room are solved
/// </summary>
public class RoomTeleporter : MonoBehaviour {
	// list of all the puzzlegoal scripts in the room
	private List<PuzzleGoal> puzzleList;

	// whether the teleporter is open for teleporting or not (all puzzles must ve solved in the room for it to be open)
	private bool open = false;

	// how often the puzzle goals are checked for completion, and the current cooldown for the check
	private float checkCooldownRate = 1;
	private float checkCooldown = 0;

	/// <summary>
	/// Collision event for player
	/// </summary>
	/// <param name="other">Collider other.</param>
	void OnTriggerEnter (Collider other)
	{
		// check if the collider is the snakehead and the teleporter is currently open
		if (other.gameObject.GetComponent<SnakeHead> () && open)
		{
			// advance to the next scene
			GameController.AdvanceScene ();
		}
	}

	// Use this for initialization
	void Start () {
		// create the puzzle list
		puzzleList = new List<PuzzleGoal> ();

		// make a temporary list of all the puzzlegoal objects (Puzzlegoals must be tagged with the PuzzleGoal tag)
		GameObject[] tempPuzzleList = GameObject.FindGameObjectsWithTag ("PuzzleGoal");

		// go through the list
		foreach (GameObject puzzle in tempPuzzleList)
		{
			// add all the puzzlegoal scripts to the puzzle list
			puzzleList.Add (puzzle.GetComponent<PuzzleGoal> ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		// check if the teleporter isnt open yet and the check cooldown has run out
		if (!open && checkCooldown <= 0)
		{
			// save a temp variable for checking the puzzles
			// if this is still true when the puzzles are checked then all the puzzles are unlocked
			bool allPuzzlesUnlocked = true;

			// go through all the puzzles in the list
			foreach (PuzzleGoal puzzle in puzzleList)
			{
				// check if the puzzle is not solved yet
				if (!puzzle.Unlocked)
				{
					// a puzzle was not solved yet, time to bug out
					allPuzzlesUnlocked = false;
					break;
				}
			}

			// save the open status to the correct value
			open = allPuzzlesUnlocked;

			// start the check cooldown again
			checkCooldown = checkCooldownRate;
		}
		// cooldown not finished
		else
		{
			// reduce cooldown by the frame time
			checkCooldown -= Time.deltaTime;
		}
	}
}