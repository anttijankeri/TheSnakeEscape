using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
	private List<Item> itemList;

	public Inventory ()
	{
		itemList = new List<Item> ();

		AddItem (new Item ());
		AddItem (new Item ());
		AddItem (new Item ("Key", false));
		AddItem (new Item ());
		AddItem (new Item ("Cherry"));
		AddItem (new Item ("TetrisBlue"));
		AddItem (new Item ("TetrisYellow"));
		AddItem (new Item ("TetrisRed"));
		AddItem (new Item ("TetrisOrange"));
		AddItem (new Item ("TetrisPurple"));
		AddItem (new Item ());
	}

	public int ItemTotal
	{
		get
		{
			return itemList.Count;
		}
	}

	public void AddItem (Item item)
	{
		itemList.Add (item);
	}

	public void UseItem (int itemNumber)
	{
		if (!itemList [itemNumber].PuzzleItem)
		{
			itemList.RemoveAt (itemNumber);
		}
	}

	public Sprite GetSprite (int itemNumber)
	{
		return itemList [itemNumber].ItemSprite;
	}
}