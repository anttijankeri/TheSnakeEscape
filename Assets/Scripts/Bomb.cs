using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bomb that's created which ticks down and then explodes and shows an explosion
/// </summary>
public class Bomb : MonoBehaviour {
	// total timer for the bomb to explode
	private float timeLeft = 4;

	// if the bomb has exploded already
	private bool exploded = false;

	// how long the explosion effect is shown
	private float explosionTimeLeft = 0.25f;

	// bomb explosion radius in unity units
	private float bombRadius = 1.2f;
	
	// Update is called once per frame
	void Update () {
		if (!exploded)
		{
			// reduce time left
			timeLeft -= Time.deltaTime;

			// check if time ran out
			if (timeLeft <= 0)
			{
				exploded = true;

				// make the explosion happen
				new Explosion (bombRadius, true, transform.position);

				// change sprite to explosion
				GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load ("Sprites/WorldSprites/Bomb_explode", typeof(Sprite));
			}
		}
		// bomb has exploded, count down until explosion should disappear
		else
		{
			explosionTimeLeft -= Time.deltaTime;

			if (explosionTimeLeft <= 0)
			{
				Destroy (gameObject);
			}
		}
	}
}