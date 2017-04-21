using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.GetComponent<SnakeHead> ())
		{
			Inventory.AddItem (gameObject.name);

			Destroy (gameObject);
		}
	}
}