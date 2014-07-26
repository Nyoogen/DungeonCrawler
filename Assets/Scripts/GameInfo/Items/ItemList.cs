using UnityEngine;
using System.Collections;

public class ItemList : MonoBehaviour 
{
	public Consumable potion = new Consumable("Potion", 10f, 0);
	public Consumable ether = new Consumable("Ether", 0, 10f);
	public Consumable blindPotion = new Consumable("Blind Potion", 0, 0, true, new string[1]{"blind"});

	public Equipment woodenSword = new Equipment();
}
