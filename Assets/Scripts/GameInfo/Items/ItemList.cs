﻿using UnityEngine;
using System.Collections;

public class ItemList
{
	public static Consumable potion = new Consumable("Potion", 10f, 0);
	public static Consumable ether = new Consumable("Ether", 0, 10f);
	public static Consumable blindPotion = new Consumable("Blind Potion", 0, 0, true, new string[1]{"blind"});

	public static Equipment woodenSword = new Equipment("Wooden Sword", 0, 0, 0, 10, 10, 0, 0, 0, 0, 0, 0, 0);
	public static Equipment excalibur = new Equipment("Excalibur", 0, 0, 0, 100, 100, 0, 0, 0, 0, 0, 0, 0);
}
