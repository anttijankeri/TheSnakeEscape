using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour {
	private GameObject followedObject;
	private SnakeHead snakeHead;
	private float followDistance;

	public GameObject FollowedObject
	{
		get
		{
			return this.followedObject;
		}
		set
		{
			this.followedObject = value;
		}
	}

	public SnakeHead SnakeHeadScript
	{
		get
		{
			return this.snakeHead;
		}
		set
		{
			this.snakeHead = value;
		}
	}

	public float FollowDistance
	{
		get
		{
			return this.followDistance;
		}
		set
		{
			this.followDistance = value;
		}
	}

	private void FollowTarget ()
	{
		Vector3 posDifference = new Vector3 (0, 0, 0);
		posDifference = FollowedObject.transform.position - transform.position;
		float len = posDifference.magnitude;
		if (SnakeHeadScript.Moving || len > FollowDistance)
		{
			float radAngle = Mathf.Atan2 (posDifference.y, posDifference.x);

			transform.eulerAngles = new Vector3 (0, 0, radAngle * Mathf.Rad2Deg);

			float frameSpeed = Mathf.Min (len - FollowDistance, SnakeHeadScript.Speed * Time.deltaTime);

			transform.position += new Vector3 (Mathf.Cos (radAngle) * frameSpeed, Mathf.Sin (radAngle) * frameSpeed, 0);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		FollowTarget ();
	}
}
