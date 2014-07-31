using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreInventory : MonoBehaviour {


	public ArrayList storeList = new ArrayList();
	public List<float> costList = new List<float>();
	
	void Awake()
	{
		storeList.Add(ItemList.potion);
		storeList.Add(ItemList.ether);
		storeList.Add(ItemList.woodenSword);
		storeList.Add(ItemList.excalibur);
		
		costList.Add(100f);
		costList.Add(200f);
		costList.Add(300f);
		costList.Add(500f);
	}
}
