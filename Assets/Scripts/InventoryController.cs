using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Creates and controls the inventory
/// </summary>
public class InventoryController : MonoBehaviour {
	// how much the inventory takes vertically from the bottom of the screen on a scale of 0-1
	public static float inventoryPortion = 0.15f;

	// the amount of ITEM BUTTONS the inventory has in total
	public static int inventoryButtonAmount = 4;

	// the currently shown item slot on the leftmost button
	private int currentLeftmostItemSlot = 0;

	// the inventory itself
	private Inventory inventory;

	// a list of ALL the buttons the inventory has (2 arrows + item buttons)
	private List<Button> buttonList;

	// a static variable for the inventory canvas
	public static GameObject inventoryCanvas;

	/// <summary>
	/// Browses left with the shown items by one if possible
	/// </summary>
	private void BrowseLeft ()
	{
		// check if can go further left
		if (currentLeftmostItemSlot > 0)
		{
			// change the currently shown leftmost item
			currentLeftmostItemSlot--;

			// update the item button sprites
			UpdateButtons ();
		}
	}

	/// <summary>
	/// Browses the item list to the right
	/// </summary>
	private void BrowseRight ()
	{
		// check if there's any items to show to the right
		if (inventory.ItemTotal > currentLeftmostItemSlot + inventoryButtonAmount)
		{
			// increase the leftmost item
			currentLeftmostItemSlot++;

			// update the item button sprites
			UpdateButtons ();
		}
	}

	/// <summary>
	/// Item button click
	/// Tries to use the item from the button if there is one and moves the other items around
	/// </summary>
	/// <param name="button">Button number</param>
	private void ClickItemButton (int button)
	{
		// check if the pressed button is supposed to have an item
		if (inventory.ItemTotal > currentLeftmostItemSlot + button)
		{
			// use the item if possible
			inventory.UseItem (currentLeftmostItemSlot + button);

			// check if there's not enough items being shown atm (the listing needs to move LEFT)
			if (currentLeftmostItemSlot > inventory.ItemTotal - inventoryButtonAmount)
			{
				// move the listing left by one if possible
				currentLeftmostItemSlot = Mathf.Max (0, currentLeftmostItemSlot - 1);
			}

			// update the item button sprites
			UpdateButtons ();
		}
	}

	/// <summary>
	/// Updates the item button sprites or hides them if necessary
	/// </summary>
	public void UpdateButtons ()
	{
		// go through all the item buttons in the button list
		for (int i = 2; i < 2 + inventoryButtonAmount; i++)
		{
			// check if the button is supposed to have an item shown
			if (inventory.ItemTotal > currentLeftmostItemSlot + i - 2)
			{
				// activate and change the sprite to the correct one
				buttonList [i].GetComponent<Image> ().enabled = true;
				buttonList [i].GetComponent<Image> ().sprite = inventory.GetSprite (currentLeftmostItemSlot + i - 2);
			}
			// no item for this button
			else
			{
				// deactivate the item image
				buttonList [i].GetComponent<Image> ().enabled = false;
			}
		}
	}

	/// <summary>
	/// Turn off self-destruction on scene changes
	/// </summary>
	void Awake ()
	{
		// dont self-destruct when changing rooms
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		// save the inventory's canvas to the static variable (used by the gamecontroller to hide the inventory when game is paused)
		inventoryCanvas = gameObject;

		// create the actual inventory
		inventory = new Inventory ();

		// create the list for buttons
		buttonList = new List<Button> ();

		// go through ALL the buttons the inventory contains
		// 0 = the inventory background (not needed)
		// 1 = the left inventory arrow button
		// 2 = the right inventory arrow button
		// 3-6 = all the item buttons starting from the left going right
		for (int i = 1; i <= 2 + inventoryButtonAmount; i++)
		{
			// add all the button components to the list
			buttonList.Add (gameObject.transform.GetChild (i).GetComponent<Button> ());
		}

		// give the arrow buttons their functionality
		buttonList [0].onClick.AddListener (() => BrowseLeft ());
		buttonList [1].onClick.AddListener (() => BrowseRight ());

		// go through the item buttons in the list
		for (int i = 2; i < 2 + inventoryButtonAmount; i++)
		{
			// save the button number to a temp value (very necessary, dont remove)
			int temp = i - 2;

			// give the item button its functionality
			buttonList [i].onClick.AddListener (() => ClickItemButton (temp));
		}

		// update all the button sprites and hide if necessary
		UpdateButtons ();
	}
}