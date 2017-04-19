using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Moves the camera relative to the player.
/// </summary>
public class CameraController : MonoBehaviour {
	// the snake head's gameobject
    private GameObject player;

	// Use this for initialization
	void Start ()
    {
		// get the snake head game object
		player = GameObject.Find ("SnakeHead");

		// save the aspect ratio for the game window, leaving the inventory portion out of the calculation
		float aspectRatio = (float) Screen.width / ((float) Screen.height * (1 - InventoryController.inventoryPortion));

		// set the camera aspect ratio to the calculated one
		Camera.main.aspect = aspectRatio;

		// set the viewport in the window to ignore the bottom of the screen for the inventory
		Camera.main.pixelRect = new Rect (0, InventoryController.inventoryPortion * Screen.height, (float) 1 * Screen.width, (float) 1 * Screen.height);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
		// move the camera to the player's position
		transform.position = player.transform.position + new Vector3 (0, 0, -10);
	}
}
