using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Moves the camera relative to the player.
/// </summary>
public class CameraController : MonoBehaviour {
    private GameObject player;

	// Use this for initialization
	void Start ()
    {
		player = GameObject.Find ("SnakeHead");

		float aspectRatio = (float) Screen.width / ((float) Screen.height * (1 - InventoryController.inventoryPortion));
		Camera.main.aspect = aspectRatio;

		Camera.main.pixelRect = new Rect (0, InventoryController.inventoryPortion * Screen.height, (float) 1 * Screen.width, (float) 1 * Screen.height);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
		transform.position = player.transform.position + new Vector3 (0, 0, -10);
	}
}
