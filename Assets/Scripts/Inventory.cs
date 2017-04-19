using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
	private List<Item> itemList;

	public Inventory ()
	{
		itemList = new List<Item> ();

		AddItem (new Item ("Apple"));
		AddItem (new Item ("Apple"));
		AddItem (new Item ("Key"));
		itemList [2].ArmPuzzleItem ("PuzzleGoal");
		AddItem (new Item ("Apple"));
		AddItem (new Item ("Cherry"));
		AddItem (new Item ("TetrisBlue"));
		itemList [5].ArmPuzzleItem ("PuzzleGoal");
		AddItem (new Item ("TetrisYellow"));
		AddItem (new Item ("TetrisRed"));
		AddItem (new Item ("TetrisOrange"));
		AddItem (new Item ("TetrisPurple"));
		AddItem (new Item ("Cherry"));
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