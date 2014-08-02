using UnityEngine;
using System.Collections;

public class Enemy
{
	public string enemyName;
	public float hitPoints;
	public float mentalPoints;
	public float strengthDamage;
	public float strengthAccuracy;
	public float aptitudeDamage;
	public float aptitudeAccuracy;
	public float charismaDamage; 
	public float charismaAccuracy; 
	public float hitPointsDefense;
	public float mentalPointsDefense;
	public float hitPointsEvasion;
	public float mentalPointsEvasion;

	// Default constructor
	public Enemy()
	{
		enemyName = "";
		hitPoints = 0f;
		mentalPoints = 0f;
		strengthDamage = 0f;
		strengthAccuracy = 0f;
		aptitudeDamage = 0f;
		aptitudeAccuracy = 0f;
		charismaDamage = 0f;
		charismaAccuracy = 0f;
		hitPointsDefense = 0f;
		mentalPointsDefense = 0f;
		hitPointsEvasion = 0f;
		mentalPointsEvasion = 0f;
	}

	public Enemy(string name, float hp, float mp, float strDmg, float strAcc, float aptDmg, float aptAcc, float chaDmg, float chaAcc, float hpDef, float mpDef, float hpEva, float mpEva)
	{
		enemyName = name;
		hitPoints = hp;
		mentalPoints = mp;
		strengthDamage = strDmg;
		strengthAccuracy = strAcc;
		aptitudeDamage = aptDmg;
		aptitudeAccuracy = aptAcc;
		charismaDamage = chaDmg;
		charismaAccuracy = chaAcc;
		hitPointsDefense = hpDef;
		mentalPointsDefense = mpDef;
		hitPointsEvasion = hpEva;
		mentalPointsEvasion = mpEva;
	}

}
