using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for picking up items from the gameworld and adding them to the inventory
/// Make sure the gameobject is named the same as the item used in the Item class!
/// </summary>
public class ItemPickup : MonoBehaviour {

	/// <summary>
	/// Collision event start (with player)
	/// </summary>
	/// <param name="other">Collider other.</param>
	void OnTriggerEnter (Collider other)
	{
		// check if colliding with the player/snake head
		if (other.gameObject.GetComponent<SnakeHead> ())
		{
			// add this item to the inventory
			Inventory.AddItem (gameObject.name);

			// destroy the item pickup object
			Destroy (gameObject);
		}
	}
}