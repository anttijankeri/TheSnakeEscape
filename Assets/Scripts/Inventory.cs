using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains and handles all the items the player has
/// </summary>
public class Inventory {
	// list of all the items the player has at the moment
	private List<Item> itemList;

	// static variable for the single object of this class
	private static Inventory inventory;

	// dictionary of all the items in the game and whether they're for puzzles (true) or not (false)
	private Dictionary<string, bool> itemTypes;

	/// <summary>
	/// Initializes a new instance of the <see cref="Inventory"/> class.
	/// </summary>
	public Inventory ()
	{
		// save the static variable to this object/script
		inventory = this;

		// create the item list
		itemList = new List<Item> ();

		// create and add all the items in the game to the itemtype dictionary
		itemTypes = new Dictionary<string, bool> ();
		itemTypes.Add ("Apple", false);
		itemTypes.Add ("Cherry", false);
		itemTypes.Add ("Bomb", false);
		itemTypes.Add ("Redbull", false);
		itemTypes.Add ("CalcBomb", true);
		itemTypes.Add ("Key", true);
		itemTypes.Add ("TetrisBlue", true);
		itemTypes.Add ("TetrisYellow", true);
		itemTypes.Add ("TetrisRed", true);
		itemTypes.Add ("TetrisPurple", true);
		itemTypes.Add ("TetrisOrange", true);
		itemTypes.Add ("PictureFrame", true);
	}

	/// <summary>
	/// Returns the total amount of items the player has
	/// </summary>
	/// <value>The item total.</value>
	public int ItemTotal
	{
		get
		{
			return itemList.Count;
		}
	}

	/// <summary>
	/// Adds an item of a certain name (has to match the dictionary input!)
	/// </summary>
	/// <param name="itemName">Item name.</param>
	public static void AddItem (string itemName)
	{
		// create new item with the given name
		Item item = new Item (itemName);

		// add it to the item list
		inventory.itemList.Add (item);

		// check if the item type is a puzzle item or not
		if (inventory.itemTypes [itemName])
		{
			// turn the item into a puzzle item (cannot be used outside puzzlegoals)
			item.ArmPuzzleItem ();
		}

		// update the inventory buttons
		GameObject.Find("InventoryCanvas").GetComponent<InventoryController> ().UpdateButtons ();
	}

	/// <summary>
	/// Use the item in the given slot
	/// </summary>
	/// <param name="itemNumber">Item number.</param>
	public void UseItem (int itemNumber)
	{
		// try to use the item (puzzle items cannot be used in the wrong place)
		if (itemList [itemNumber].Use ())
		{
			// remove the item from the list if it was used
			itemList.RemoveAt (itemNumber);
		}
	}

	/// <summary>
	/// Get the item sprite at the specific item slot
	/// </summary>
	/// <returns>The sprite.</returns>
	/// <param name="itemNumber">Item number.</param>
	public Sprite GetSprite (int itemNumber)
	{
		// return the sprite from the slot
		return itemList [itemNumber].ItemSprite;
	}
}