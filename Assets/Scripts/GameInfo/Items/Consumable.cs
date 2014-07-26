using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumable : MonoBehaviour 
{
	public string itemName;
	public float hitPoints;
	public float mentalPoints;
	public bool hasEffects;
	public List<string> effects;

	public Consumable (string name, float hp, float mp, bool hasEff, string[] eff)
	{
		itemName = name;
		hitPoints = hp;
		mentalPoints = mp;
		hasEffects = hasEff;
		foreach (string element in eff)
		{
			effects.Add(element);
		}
	}

	public Consumable (string name, float hp, float mp)
	{
		itemName = name;
		hitPoints = hp;
		mentalPoints = mp;
		hasEffects = false;
	}

	// Default constructor
	public Consumable ()
	{
		itemName = "";
		hitPoints = 0f;
		mentalPoints = 0f;
		hasEffects = false;
	}
}
