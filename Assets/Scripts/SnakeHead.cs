using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes care of the snake's health and spawns/destroys tail sections.
/// </summary>
public class SnakeHead : MonoBehaviour {
	// snake's remaining health = how many tail sections left, 0 = game over
	private int health;

	// distance in unity units between tail pieces
	private float tailDistance = (float) 0.09;

	// the speed of the snake, taken from the player controller
	private float speed;

	// whether the snake is moving this frame or not
	private bool moving;

	// incoming damage and healing values for this frame
	private int incomingDamage = 0;
	private int incomingHeal = 0;

	// list containing all the tail pieces' scripts
	private List<SnakeTail> tailList;

	// prefab used for spawning new tail sections
	private GameObject snakeTailPrefab;

	// player controller used for controlling the snake
	private PlayerController playerController;

	// prev position of the snake head, used for interpolation during turns
	private Vector3 prevPosition;

	// middlemost tail section's position, used for enemy AI's targeting
	private Vector3 middleSectionPos;

	/// <summary>
	/// Adds a tail piece at the given location, following the given object, and appends the tail list
	/// Also returns the newly created tail piece in case the next tail piece after that needs it
	/// </summary>
	/// <returns>Newly created tail piece</returns>
	/// <param name="following">The GameObject this piece must follow</param>
	/// <param name="position">Transform position of the new piece</param>
	/// <param name="rotation">Quaternion rotation of the new piece</param>
	/// <param name="tailNumber">Sequential tail number</param>
	private GameObject AddTailPiece (GameObject following, Vector3 position, Quaternion rotation, int tailNumber)
	{
		// create the new tail piece's actual gameobject
		GameObject newTail = Instantiate (snakeTailPrefab, position, rotation);

		// name the tail piece for later when doing searches by name
		newTail.name = "SnakeTail" + tailNumber;

		// get the snaketail script of the new tail
		SnakeTail newScript = newTail.GetComponent<SnakeTail> ();

		// set the script's variables as necessary (which object it follows, the snake head's script, 
		// the distance between tail sections, which tail piece it is)
		newScript.FollowedObject = following;
		newScript.SnakeHeadScript = this;
		newScript.FollowDistance = tailDistance;
		newScript.TailNumber = tailNumber;

		// add the new tail script to the list of scripts
		tailList.Add (newScript);

		// exit the method returning the newly created gameobject
		return newTail;
	}

	/// <summary>
	/// Adds an amount to the incoming heal counter for the snake
	/// </summary>
	/// <param name="amount">Healing amount.</param>
	public void AddHeal (int amount)
	{
		incomingHeal += amount;
	}

	/// <summary>
	/// Adds tail pieces equal to the amount given
	/// </summary>
	/// <param name="amount">Healing amount</param>
	private void HealTail (int amount)
	{
		// get the correct gameobject to follow for the first piece created
		GameObject following = (health > 0) ? tailList [health - 1].gameObject : this.gameObject;

		// repeat for healing amount times
		for (int i = 0; i < amount; i++)
		{
			// create new tail piece and add to total health
			following = AddTailPiece (following, following.transform.position, following.transform.rotation, health);
			health++;
		}
	}

	/// <summary>
	/// Adds to the damage value based on the hurt tail section
	/// The closer the hurt tail section is to the snake head, the more damage is inflicted
	/// </summary>
	/// <param name="snakeTail">Snake tail.</param>
	public void AddDamage (GameObject snakeTail)
	{
		// get the tail number of the hurt tail section
		int damagedTail = snakeTail.GetComponent<SnakeTail> ().TailNumber;

		// add to the incoming damage if its higher than the highest value for this frame
		incomingDamage = Mathf.Max (health - damagedTail, incomingDamage);
	}

	/// <summary>
	/// Remove tail sections from the end equal to the amount given
	/// </summary>
	/// <param name="amount">Damage amount.</param>
	private void TakeDamage (int amount)
	{
		// repeat for damaged tail sections
		for (int i = health - 1; i >= health - amount; i--)
		{
			// remove the snaketail gameobject and any reference to it from the list
			SnakeTail tail = tailList [i];
			tailList.RemoveAt (i);
			Destroy (tail.gameObject);
		}
		// reduce total health remaining
		health -= amount;
	}

	/// <summary>
	/// Compare incoming damage and healing values to see which is higher and if the snake needs to gain or lose tail pieces
	/// </summary>
	private void CalculateIncomings ()
	{
		// check if more healing than damage and grow tail if necessary
		if (incomingHeal > incomingDamage)
		{
			HealTail (incomingHeal - incomingDamage);
		}
		// check if more damage than healing and remove tail sections if necessary
		else if (incomingDamage > incomingHeal)
		{
			TakeDamage (incomingDamage - incomingHeal);
		}
		// reset incoming values for the next frame
		incomingDamage = 0;
		incomingHeal = 0;
	}

	/// <summary>
	/// Returns the middle section's position used by enemy AI
	/// </summary>
	/// <value>Snaketail's middle's position.</value>
	public Vector3 MiddleSectionPos
	{
		get
		{
			return this.middleSectionPos;
		}
	}

	/// <summary>
	/// Updates the middle section's position if the snake has health
	/// </summary>
	public void UpdateMiddleSectionPos ()
	{
		// check if any health remaining
		if (health > 0)
		{
			// update the middle section's position
			this.middleSectionPos = tailList [Mathf.Min (health / 2, health - 1)].gameObject.transform.position;
		}
	}

	/// <summary>
	/// Gets or sets a value indicating whether the Snakehead is moving.
	/// </summary>
	/// <value><c>true</c> if moving; otherwise, <c>false</c>.</value>
	public bool Moving
	{
		get
		{
			return this.moving;
		}
		set
		{
			this.moving = value;
		}
	}

	/// <summary>
	/// Gets or sets the speed of the snake used by the tail sections
	/// </summary>
	/// <value>Snakehead's speed</value>
	public float Speed
	{
		get
		{
			return this.speed;
		}
		set
		{
			this.speed = value;
		}
	}

	/// <summary>
	/// Gets or sets the total health remaining.
	/// </summary>
	/// <value>Snake's health.</value>
	public int Health
	{
		get
		{
			return this.health;
		}
		set
		{
			this.health = value;
		}
	}

	/// <summary>
	/// Collision event for when colliding with enemy NPCs
	/// </summary>
	/// <param name="other">Other collider</param>
	void OnTriggerEnter (Collider other)
	{
		// check if the collider is an enemy
		if (other.gameObject.GetComponent<EnemyNPC> ())
		{
			// destroy the enemy NPC and heal for one.
			Destroy (other.gameObject);
			AddHeal (1);
		}
	}

	// Use this for initialization
	void Start () {
		// get the remaining health
		health = GameController.playerHealth;

		// get the player controller script and save it
		playerController = this.gameObject.GetComponent<PlayerController> ();

		// load the prefab from the resources folder
		snakeTailPrefab = Resources.Load ("Prefabs/SnakeTail") as GameObject;

		// save the previous position of the snake
		prevPosition = transform.position;

		// set the object the new tail section needs to follow
		GameObject following = this.gameObject;

		// create the tail section script list
		tailList = new List<SnakeTail> ();

		// save the position for the new tail piece
		Vector3 position = transform.position;

		// create tail pieces equal to total health
		for (int i = 0; i < health; i++)
		{
			// move the creation spot left equal to the distance between tail sections
			position.x -= tailDistance / 5;

			// create the new tail piece
			following = AddTailPiece (following, position, transform.rotation, i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// check if player's health went too low (= we deadededed)
		if (health <= 0)
		{
			// end the game
			GameController.GameOver ();
		}

		// save the snakehead's speed for this frame
		Speed = playerController.GetRealSpeed ();

		// save whether the snake is moving or not
		Moving = (prevPosition != transform.position) ? true : false;

		// save the position this frame for use in the next frame
		prevPosition = transform.position;

		// calculate incoming damage and healing values and act accordingly
		CalculateIncomings ();

		// update the tail's middle section's position if the tail exists
		UpdateMiddleSectionPos ();
	}
}