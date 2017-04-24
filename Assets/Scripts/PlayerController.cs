using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the snake head with the mouse
/// </summary>
public class PlayerController : MonoBehaviour {
	// the speed of the snake, unity units per second (true speed = speed * speedMP * deltaTime)
	private float speed = 1;
	private float speedMP = 1;

	// the turn rate of the snake, degrees per second * multiplier
	private float turnRate = 300;
	private float turnRateMP = 1;

	// threshold distance for moving the snake
	private float movementThreshold = (float) 0.1;

	// goal position to move towards => last known mouse click position
	private Vector3 goalPosition;

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

	// Use this for initialization
	void Start () {
		// save object's current position as goal on startup
		goalPosition = new Vector3(transform.position.x, transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		// check if the game is not paused
		if (!GameController.gamePaused)
		{
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
				transform.position += new Vector3 (Mathf.Cos (radAngle) * frameSpeed, Mathf.Sin (radAngle) * frameSpeed, 0);
			}
		}
		// game is paused
		else
		{
			// reset goalposition to the current position so the snake doesnt move while the game is paused
			goalPosition = transform.position;
		}
	}
}