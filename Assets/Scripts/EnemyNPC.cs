using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Controls the enemy NPC behaviour
/// </summary>
public class EnemyNPC : MonoBehaviour {
	// how long in seconds the enemy should wait after hitting a tail section
	[SerializeField]
	private float waitOnHit = 1;

	// if the enemy should self destruct after hitting the snake tail
	[SerializeField]
	private bool selfDestruct = false;

	// if the game should end if we get hit
	[SerializeField]
	private bool suddenDeath = false;

	// if the enemy tries to chase the player
	[SerializeField]
	public bool blind = false;

	// if the enemy can be killed
	[SerializeField]
	public bool unkillable = false;

	// if the enemy can see thru walls
	[SerializeField]
	private bool wallhack = false;

	// object destroyer to trigger upon death
	[SerializeField]
	private string objectDestroyerName = "";

	// dialogue trigger name upon death
	[SerializeField]
	private string dialogueTriggerName = "";

	// how long the NPC has left to wait
	private float timeToWait = 0;

	// the movement speed of the NPC per second
	[SerializeField]
	private float speed = (float) 0.6;

	// the rigidbody component
	private Rigidbody rb;

	// the snake head script used for following
	private SnakeHead snakeHead;

	/// <summary>
	/// Triggers the object destroyer if necessary
	/// </summary>
	void OnDestroy ()
	{
		// if theres a destroyer attached
		if (objectDestroyerName != "")
		{
			// find and save the trigger, then check if it actually exists
			ObjectDestroyer objDes = GameObject.Find (objectDestroyerName).GetComponent<ObjectDestroyer> ();
			if (objDes != null)
			{
				objDes.TriggerDestroyer ();
			}
		}

		// trigger some dialogue if necessary
		if (dialogueTriggerName != "")
		{
			DialogueTrigger.TriggerDialogue (dialogueTriggerName);
		}
	}

	// Use this for initialization
	void Start () {
		// get the snake head script
		snakeHead = GameObject.Find ("SnakeHead").GetComponent<SnakeHead> ();

		// save the rigidbody component
		rb = GetComponent<Rigidbody> ();
	}

	/// <summary>
	/// Collision event when colliding with any snake pieces
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider other)
	{
		// check if the object exists
		if (GameObject.Find(other.gameObject.name))
		{
			// check if the collided object is a snake tail
			if (other.gameObject.name.Contains("SnakeTail"))
			{
				// start the timer for waiting
				timeToWait = waitOnHit;

				// tell the snakehead to take some damage based on the tail section that was hit
				snakeHead.AddDamage (other.gameObject);

				// if sudden death mode is on => game over
				if (suddenDeath)
				{
					GameController.GameOver ();
				}

				// if need to self destruct
				if (selfDestruct)
				{
					GameObject.Destroy (gameObject);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		// not paused, blind, or in dialogue mode
		if (!GameController.gamePaused && !blind && !dialogueManager.dialogueActive)
		{
			// check if the timer to wait is gone and the snake actually has some health
			if (timeToWait <= 0 && snakeHead.Health > 0)
			{
				// update the position we need to be chasing
				Vector3 snakeMidPos = (snakeHead.MiddleSectionPos);

				// calculate the position difference between the NPC and the chased position
				Vector3 posDifference = snakeMidPos - transform.position;

				// get line of sight to the player
				if (wallhack || !Physics.Raycast (transform.position, posDifference, posDifference.magnitude, 1 << 8))
				{
					// stop the patrolling/movement if there is some
					if (GetComponent<NPCPathing> ())
					{
						GetComponent<NPCPathing> ().moving = false;
					}

					// move the NPC at the correct frame speed
					rb.MovePosition(posDifference.normalized * speed * Time.deltaTime + transform.position);
				}
			}
			// the timer is still running
			else
			{
				// reduce the timer by the correct amount
				timeToWait -= Time.deltaTime;
			}
		}
	}
}