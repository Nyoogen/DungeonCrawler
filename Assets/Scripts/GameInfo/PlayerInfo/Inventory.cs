using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public static ArrayList invList = new ArrayList();
	public static List<int> invCount = new List<int>();

	public static void AddItem(Equipment item)
	{
		int index = invList.IndexOf(item);
		if(index < 0)
		{
			// Item doesn't exist
			Debug.Log(item.itemName+" doesn't exist, adding it to inventory");
			invList.Add(item);
			invCount.Add(1);
		}
		else
			invCount[index]++;

	}

	public static void AddItem(Consumable item)
	{
		int index = invList.IndexOf(item);
		if(index < 0)
		{
			// Item doesn't exist
			Debug.Log(item.itemName+" doesn't exist, adding it to inventory");
			invList.Add(item);
			invCount.Add(1);
		}
		else
			invCount[index]++;
	}

	public static void RemoveItem(Equipment item)
	{
		int index = invList.IndexOf(item);
		if(index < 0)
			Debug.Log("There is no "+item.itemName+" to remove, bro.");
		else if(invCount[index] == 1)
		{
			Debug.Log("Removing last "+item.itemName);
			invList.RemoveAt(index);
			invCount.RemoveAt(index);
		}
		else
		{
			Debug.Log("Removing one "+item.itemName);
			invCount[index]--;
		}
	}

	public static void RemoveItem(Consumable item)
	{
		int index = invList.IndexOf(item);
		if(index < 0)
			Debug.Log("There is no "+item.itemName+" to remove, bro.");
		else if(invCount[index] == 1)
		{
			Debug.Log("Removing last "+item.itemName);
			invList.RemoveAt(index);
			invCount.RemoveAt(index);
		}
		else
		{
			Debug.Log("Removing one "+item.itemName);
			invCount[index]--;
		}
	}
}
