using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {
	private int health = 20;
	private float tailDistance = (float) 0.125;
	private float speed;
	private bool moving;
	private int incomingDamage = 0;
	private int incomingHeal = 0;

	private List<SnakeTail> tailList;
	private GameObject snakeTailPrefab;
	private PlayerController playerController;

	private Vector3 prevPosition;
	private Vector3 middleSectionPos;

	private GameObject AddTailPiece (GameObject following, Vector3 position, Quaternion rotation, int tailNumber)
	{
		GameObject newTail = Instantiate (snakeTailPrefab, position, rotation);
		newTail.name = "SnakeTail" + tailNumber;
		SnakeTail newScript = newTail.GetComponent<SnakeTail> ();
		newScript.FollowedObject = following;
		newScript.SnakeHeadScript = this;
		newScript.FollowDistance = tailDistance;
		newScript.TailNumber = tailNumber;
		tailList.Add (newScript);
		return newTail;
	}

	public void AddHeal (int amount)
	{
		incomingHeal += amount;
	}

	private void HealTail (int amount)
	{
		GameObject following = (health > 0) ? tailList [health - 1].gameObject : this.gameObject;
		for (int i = 0; i < amount; i++)
		{
			following = AddTailPiece (following, following.transform.position, following.transform.rotation, health);
			health++;
		}
		Debug.Log ("healed for " + amount + ", current health " + health);
	}

	public void AddDamage (GameObject snakeTail)
	{
		int damagedTail = snakeTail.GetComponent<SnakeTail> ().TailNumber;
		incomingDamage = Mathf.Max (health - damagedTail, incomingDamage);
	}

	private void TakeDamage (int amount)
	{
		for (int i = health - 1; i >= health - amount; i--)
		{
			SnakeTail tail = tailList [i];
			tailList.RemoveAt (i);
			Destroy (tail.gameObject);
		}
		health -= amount;
		Debug.Log ("Took " + amount + " damage, current health " + health);
	}

	private void CalculateIncomings ()
	{
		if (incomingHeal > incomingDamage)
		{
			HealTail (incomingHeal - incomingDamage);
		}
		else if (incomingDamage > incomingHeal)
		{
			TakeDamage (incomingDamage - incomingHeal);
		}
		incomingDamage = 0;
		incomingHeal = 0;
	}

	public Vector3 MiddleSectionPos
	{
		get
		{
			return this.middleSectionPos;
		}
	}

	public void UpdateMiddleSectionPos ()
	{
		if (health > 0)
		{
			this.middleSectionPos = tailList [Mathf.Min (health / 2, health - 1)].gameObject.transform.position;
		}
	}

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

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.GetComponent<EnemyNPC> ())
		{
			Destroy (other.gameObject);
			AddHeal (3);
		}
	}

	// Use this for initialization
	void Start () {
		playerController = this.gameObject.GetComponent<PlayerController> ();
		snakeTailPrefab = Resources.Load ("SnakeTail") as GameObject;
		prevPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		GameObject following = this.gameObject;
		tailList = new List<SnakeTail> ();
		Vector3 position = transform.position;

		for (int i = 0; i < health; i++)
		{
			position.x -= tailDistance;
			following = AddTailPiece (following, position, transform.rotation, i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Speed = playerController.GetRealSpeed ();
		Moving = (prevPosition != transform.position) ? true : false;
		prevPosition = transform.position;
		CalculateIncomings ();
		UpdateMiddleSectionPos ();
	}
}