using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour 
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

	// Default constructor
	public Equipment()
	{
		itemName = "";
		hitPoints = 0f;
		mentalPoints = 0f;
		strength = 0f;
		aptitude = 0f;
		charisma = 0f;
		agility = 0f;
		cunning = 0f;
	}
}
