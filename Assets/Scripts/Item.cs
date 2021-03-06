﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for a single item that's in the inventory
/// </summary>
public class Item {
	// the sprite the item has
	private Sprite sprite;

	// the name of the item AND THE SPRITE (HAVE TO MATCH!!!)
	private string itemName;

	// whether this item is a puzzle item or not (puzzle items can only used at the puzzlegoal object)
	private bool puzzleItem = false;

	// the name of the puzzlegoal object that's the target object
	private string puzzleGoalName;

	/// <summary>
	/// Creates a new item with the given name and get its sprite
	/// </summary>
	/// <param name="itemName">Item name.</param>
	public Item (string itemName)
	{
		// save the item name
		this.itemName = itemName;

		// load the sprite from the resources/items/ folder (sprite name MUST MATCH item name)
		sprite = (Sprite) Resources.Load ("Sprites/Items/" + this.itemName, typeof(Sprite));
	}

	/// <summary>
	/// Turns the item to a puzzle item
	/// </summary>
	public void ArmPuzzleItem ()
	{
		// this item is now a puzzle item
		this.puzzleItem = true;

		// saves the puzzlegoal name to "PuzzleGoalItemname"
		// Thus puzzlegoal objects must be named that way
		this.puzzleGoalName = "PuzzleGoal" + itemName;
	}

	/// <summary>
	/// Returns the actual sprite of the item
	/// </summary>
	/// <value>The item sprite.</value>
	public Sprite ItemSprite
	{
		get
		{
			return sprite;
		}
	}

	/// <summary>
	/// Try to use the item
	/// Returns true if item was used, false otherwise
	/// </summary>
	/// <returns>True/False if the item was used</returns>
	public bool Use ()
	{
		// check if the item is a puzzle item
		if (this.puzzleItem)
		{
			// check if there's a correct type of puzzle in the scene
			if (GameObject.Find(puzzleGoalName))
			{
				// find the correct puzzlegoal's script object
				PuzzleGoal puzzleGoalScript = GameObject.Find (puzzleGoalName).GetComponent<PuzzleGoal> ();

				// check if the puzzlegoal is currently colliding with the player/snake head
				if (puzzleGoalScript.Colliding == true)
				{
					// unlock the puzzle
					puzzleGoalScript.Unlocked = true;

					// item was used
					return true;
				}
			}
			// not colliding with the correct puzzlegoal
			// item was not used
			return false;
		}
		// not a puzzle item => item can be used freely
		else
		{
			// check which item is in question
			switch (itemName)
			{
			// apple, restore some health
			case "Apple":
				// find the snakehead object and give it some healing
				GameObject.Find ("SnakeHead").GetComponent<SnakeHead> ().AddHeal (2);
				break;

			// cherry, restore lots of health
			case "Cherry":
				// find the snakehead object and give it lots of healing
				GameObject.Find ("SnakeHead").GetComponent<SnakeHead> ().AddHeal (4);
				break;

			// redbull, increase speed and turnrate
			case "RedBull":
				// find the player controlloer and boost it
				GameObject.Find ("SnakeHead").GetComponent<PlayerController> ().ActivateBoost (1.5f, 1.5f, 15);
				break;

			// bomb, create a bomb
			case "Bomb":
				// create the bomb at the snakehead position
				GameObject.Instantiate(Resources.Load ("Prefabs/Bomb") as GameObject, GameObject.Find ("SnakeHead").transform.position, Quaternion.identity);
				break;
			}

			// item was used successfully
			return true;
		}
	}
}