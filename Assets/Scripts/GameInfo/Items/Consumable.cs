using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Consumable : MonoBehaviour 
{
	public float hitPoints;
	public float mentalPoints;
	public bool hasEffects;
	public List<string> effects;

	public Consumable (float hp, float mp, bool hasEff, string[] eff)
	{
		hitPoints = hp;
		mentalPoints = mp;
		hasEffects = hasEff;
		foreach (string element in eff)
		{
			effects.Add(element);
		}
	}

	public Consumable (float hp, float mp)
	{
		hitPoints = hp;
		mentalPoints = mp;
		hasEffects = false;
	}

	// Default constructor
	public Consumable ()
	{
		hitPoints = 0f;
		mentalPoints = 0f;
		hasEffects = false;
	}
}
