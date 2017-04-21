using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
	private List<Item> itemList;
	private static Inventory inventory;
	private Dictionary<string, bool> itemTypes;

	public Inventory ()
	{
		inventory = this;

		itemList = new List<Item> ();

		itemTypes = new Dictionary<string, bool> ();
		itemTypes.Add ("Apple", false);
		itemTypes.Add ("Cherry", false);
		itemTypes.Add ("Key", true);
		itemTypes.Add ("TetrisBlue", true);
		itemTypes.Add ("TetrisYellow", true);
		itemTypes.Add ("TetrisRed", true);
		itemTypes.Add ("TetrisPurple", true);
		itemTypes.Add ("TetrisOrange", true);
	}

	public int ItemTotal
	{
		get
		{
			return itemList.Count;
		}
	}

	public static void AddItem (string itemName)
	{
		Item item = new Item (itemName);
		inventory.itemList.Add (item);
		if (inventory.itemTypes [itemName])
		{
			item.ArmPuzzleItem ();
		}
	}

	public void UseItem (int itemNumber)
	{
		if (itemList [itemNumber].Use ())
		{
			itemList.RemoveAt (itemNumber);
		}
	}

	public Sprite GetSprite (int itemNumber)
	{
		return itemList [itemNumber].ItemSprite;
	}
}