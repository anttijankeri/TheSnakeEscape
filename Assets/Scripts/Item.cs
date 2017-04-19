using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	private Sprite sprite;
	private string itemName;

	private bool puzzleItem = false;
	private string puzzleGoalName;

	public Item (string itemName)
	{
		this.itemName = itemName;

		sprite = (Sprite) Resources.Load ("Sprites/Items/" + this.itemName, typeof(Sprite));
	}

	public void ArmPuzzleItem (string puzzleGoal)
	{
		this.puzzleItem = true;
		this.puzzleGoalName = puzzleGoal;
	}

	public bool IsPuzzleItem
	{
		get
		{
			return this.puzzleItem;
		}
	}

	public Sprite ItemSprite
	{
		get
		{
			return sprite;
		}
	}

	public bool Use ()
	{
		if (this.puzzleItem)
		{
			PuzzleGoal puzzleGoalScript = GameObject.Find (puzzleGoalName).GetComponent<PuzzleGoal> ();

			if (puzzleGoalScript.Colliding == true)
			{
				switch (itemName) {
				case "Key":
					puzzleGoalScript.Unlocked = true;
					Debug.Log ("status: " + puzzleGoalScript.Unlocked);
					break;

				case "TetrisBlue":
				case "TetrisYellow":
				case "TetrisPurple":
				case "TetrisRed":
				case "TetrisOrange":
					puzzleGoalScript.ShowSelf ();
					break;
				}

				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			switch (itemName)
			{
			case "Apple":
				GameObject.Find ("SnakeHead").GetComponent<SnakeHead> ().AddHeal (4);
				break;

			case "Cherry":
				GameObject.Find ("SnakeHead").GetComponent<SnakeHead> ().AddHeal (2);
				break;
			}

			return true;
		}
	}
}