using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Controls the snake head with the mouse
/// </summary>
public class PlayerController : MonoBehaviour {
	// the speed of the snake, unity units per second (true speed = speed * speedMP * deltaTime)
	[SerializeField]
	private float speed = 0.6f;
	private float speedMP = 1;

	// the turn rate of the snake, degrees per second * multiplier
	[SerializeField]
	private float turnRate = 300;
	private float turnRateMP = 1;

	// how much is left of this speed boost
	private float boostTimeLeft = 0;

	// threshold distance between current position and goal position for moving the snake
	private float movementThreshold = (float) 0.1;

	// goal position to move towards => last known mouse click position
	private Vector3 goalPosition;

	// the character controller component of the snakehead
	private CharacterController charController;

	/// <summary>
	/// Gets or sets the speed multiplier for the snake
	/// Default = 1
	/// </summary>
	/// <value>SpeedMP</value>
	public float Speed
	{
		get
		{
			return this.speedMP;
		}
		set
		{
			this.speedMP = (float) value;
		}
	}

	/// <summary>
	/// Returns the actual speed of the snake for this frame
	/// </summary>
	/// <returns>True speed.</returns>
	public float GetRealSpeed ()
	{
		return this.speedMP * speed;
	}

	/// <summary>
	/// Gets or sets the turn rate multiplier for the snake
	/// Default = 1
	/// </summary>
	/// <value>TurnRateMP</value>
	public float TurnRate
	{
		get
		{
			return this.turnRateMP;
		}
		set
		{
			this.turnRateMP = (float) value;
		}
	}

	/// <summary>
	/// Activates a temporary boost for x seconds
	/// Can boost or lower speed and/or turn rate
	/// </summary>
	/// <param name="speedMP">float speed multiplier.</param>
	/// <param name="turnMP">float turn rate multiplier</param>
	/// <param name="boostTime">float boost time in seconds</param>
	public void ActivateBoost (float speedMP, float turnMP, float boostTime)
	{
		this.speedMP = speedMP;
		this.turnRateMP = turnMP;
		this.boostTimeLeft = boostTime;
	}

	/// <summary>
	/// Updates the boost mode counter
	/// And resets the multipliers to 1 if boost is out
	/// </summary>
	private void UpdateBoost ()
	{
		// check if the time is out
		if (this.boostTimeLeft <= 0)
		{
			// reset multipliers
			this.speedMP = 1;
			this.turnRateMP = 1;
		}
		else
		{
			// reduce time remaining
			this.boostTimeLeft -= Time.deltaTime;
		}
	}

	// Use this for initialization
	void Start () {
		// save the character controller component
		charController = gameObject.GetComponent<CharacterController> ();

		// save object's current position as goal on startup
		goalPosition = new Vector3(transform.position.x, transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		// check if the game is not paused and the dialogue is not active
		if (!GameController.gamePaused && !dialogueManager.dialogueActive)
		{
			// update speed/turn boost
			UpdateBoost ();

			// check if mouse key held down
			if (Input.GetMouseButton (0))
			{
				// save current mouse position IN THE 2D WORLD as the current goal
				if (Input.mousePosition.y > Screen.height * InventoryController.inventoryPortion)
				{
					Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					mousePositionInWorld.z = 0;
					goalPosition = mousePositionInWorld;
				}
			}

			// get the difference between the current and goal positions
			Vector3 positionDifference = goalPosition - transform.position;

			// get the absolute distance in unity units
			float len = positionDifference.magnitude;

			// check if not close enough to the goal position yet ==> need to move/turn
			if (len > movementThreshold)
			{
				// get the angle between the goal and the current positions
				float angle = Mathf.Atan2 (positionDifference.y, positionDifference.x) * Mathf.Rad2Deg;

				// get the current angle of the snake
				float currentAngle = transform.rotation.eulerAngles.z;

				// check if the snake needs to be turned
				if (angle != currentAngle)
				{
					// calculate the turn for this frame, making sure its not too big, and turn the snake
					float turn = Mathf.Clamp (Mathf.DeltaAngle (currentAngle, angle), -turnRate * turnRateMP * Time.deltaTime, turnRate * turnRateMP * Time.deltaTime);
					transform.Rotate (0, 0, turn);
				}

				// save the snake's new angle
				currentAngle = transform.rotation.eulerAngles.z;

				// save the angle in radians
				float radAngle = Mathf.Deg2Rad * currentAngle;

				// calculate the snake's speed for this frame
				float frameSpeed = Mathf.Min (len, speedMP * speed * Time.deltaTime);

				// move the snake forwards at set speed
				charController.Move(new Vector3 (Mathf.Cos (radAngle) * frameSpeed, Mathf.Sin (radAngle) * frameSpeed, 0));
			}
		}
		// game is paused or dialogue is active
		else
		{
			// reset goalposition to the current position so the snake doesnt move while the game is paused
			goalPosition = transform.position;
		}
	}
}