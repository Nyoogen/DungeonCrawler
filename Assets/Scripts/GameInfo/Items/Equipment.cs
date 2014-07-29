using UnityEngine;
using System.Collections;

public class Equipment
{
	public string itemName;
	public float hitPoints;
	public float mentalPoints;
	public float strength;
	public float aptitude;
	public float charisma;
	public float agility;
	public float cunning;

	public Equipment(string name, float hp, float mp, float str, float apt, float cha, float agi, float cun)
	{
		itemName = name;
		hitPoints = hp;
		mentalPoints = mp;
		strength = str;
		aptitude = apt;
		charisma = cha;
		agility = agi;
		cunning = cun;
	}
	
}
