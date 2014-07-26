using UnityEngine;
using System.Collections;

public class ItemList : MonoBehaviour 
{
	public Consumable potion = new Consumable(10f, 0);
	public Consumable ether = new Consumable(0, 10f);
	public Consumable blindPotion = new Consumable(0, 0, true, new string[1]{"blind"});

	public Equipment woodenSword = new Equipment();
}
