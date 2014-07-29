using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumable
{
	public string itemName;
	public float hitPoints;
	public float mentalPoints;
	public bool hasEffects;
	public List<string> effects;

	public Consumable (string name, float hp, float mp)
	{
		itemName = name;
		hitPoints = hp;
		mentalPoints = mp;
		hasEffects = false;
	}

	public Consumable (string name, float hp, float mp, bool hasEff, string[] eff)
	{
		itemName = name;
		hitPoints = hp;
		mentalPoints = mp;
		hasEffects = hasEff;
//		foreach (string element in eff)
//		{
//			effects.Add(element);
//		}
	}

	// Getters
	public float getHPField()
	{
		return hitPoints;
	}


}
