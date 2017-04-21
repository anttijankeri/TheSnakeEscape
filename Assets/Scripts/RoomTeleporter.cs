using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTeleporter : MonoBehaviour {
	private List<PuzzleGoal> puzzleList;
	private bool open = false;

	private float checkCooldownRate = 1;
	private float checkCooldown = 0;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.GetComponent<SnakeHead> () && open)
		{
			GameController.AdvanceScene ();
		}
	}

	// Use this for initialization
	void Start () {
		puzzleList = new List<PuzzleGoal> ();

		GameObject[] tempPuzzleList = GameObject.FindGameObjectsWithTag ("PuzzleGoal");

		foreach (GameObject puzzle in tempPuzzleList)
		{
			puzzleList.Add (puzzle.GetComponent<PuzzleGoal> ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!open && checkCooldown <= 0)
		{
			bool allPuzzlesUnlocked = true;

			foreach (PuzzleGoal puzzle in puzzleList)
			{
				if (!puzzle.Unlocked)
				{
					allPuzzlesUnlocked = false;
					break;
				}
			}

			open = allPuzzlesUnlocked;
			checkCooldown = checkCooldownRate;
		}
		else
		{
			checkCooldown -= Time.deltaTime;
		}
	}
}