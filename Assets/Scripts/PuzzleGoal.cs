using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Goal object the player has to be colliding with to use the right puzzle item
/// </summary>
public class PuzzleGoal : MonoBehaviour {
	// if the player is currently colliding with this object
	private bool colliding = false;

	// is this puzzle solved/unlocked (=> player used the right item on top of the puzzlegoal)
	private bool unlocked = false;

	// if the puzzle should be shown when unlocked
	[SerializeField]
	private bool showOnUnlock = false;

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="PuzzleGoal"/> is colliding with the player.
	/// </summary>
	/// <value><c>true</c> if colliding; otherwise, <c>false</c>.</value>
	public bool Colliding
	{
		get
		{
			return this.colliding;
		}
		set
		{
			this.colliding = value;
		}
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="PuzzleGoal"/> is unlocked => solved
	/// </summary>
	/// <value><c>true</c> if unlocked; otherwise, <c>false</c>.</value>
	public bool Unlocked
	{
		set
		{
			unlocked = value;

			// show/hide if necessary
			if (unlocked)
			{
				GetComponent<Renderer> ().enabled = showOnUnlock;
			}
		}
		get
		{
			return unlocked;
		}
	}

	/// <summary>
	/// Collision event for when colliding with the player
	/// </summary>
	/// <param name="other">Other collider</param>
	void OnTriggerEnter (Collider other)
	{
		// check if the collider is the player
		if (other.gameObject.GetComponent<SnakeHead> ())
		{
			// check if regular puzzle goal (no items needed)
			if (gameObject.name == "PuzzleGoal")
			{
				unlocked = true;
			}
			// need items
			else
			{
				// change colliding value
				this.colliding = true;
			}
		}
	}

	/// <summary>
	/// Collision event for deactivating puzzle goals
	/// </summary>
	/// <param name="other">Other collider</param>
	void OnTriggerExit (Collider other)
	{
		// check if the collider is the player
		if (other.gameObject.GetComponent<SnakeHead> ())
		{
			// turn the puzzlegoal's collision off
			this.colliding = false;
		}
	}
}