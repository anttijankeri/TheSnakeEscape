using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGoal : MonoBehaviour {
	private bool colliding = false;
	private bool unlocked = false;

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

	public void ShowSelf ()
	{
		gameObject.GetComponent<Renderer> ().enabled = true;
	}

	public void HideSelf ()
	{
		gameObject.GetComponent<Renderer> ().enabled = false;
	}

	public bool Unlocked
	{
		set
		{
			unlocked = value;
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
			// change colliding value
			this.colliding = true;
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

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
