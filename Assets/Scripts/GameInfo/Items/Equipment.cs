using UnityEngine;
using System.Collections;

public class Equipment
{
	public string itemName;
	public float hitPointsDefense;
	public float mentalPointsDefense;
	public float strengthDamage;
	public float strengthAccuracy;
	public float aptitudeDamage;
	public float aptitudeAccuracy;
	public float charismaDamage;
	public float charismaAccuracy;
	public float hitPointsEvasion;
	public float mentalPointsEvasion;

	public Equipment(string name, float hpD, float mpD, float strD, float strAc, float aptD, float aptAc, float chaD, float chaAc, float hpEv, float mpEv)
	{
		itemName = name;
		hitPointsDefense = hpD;
		mentalPointsDefense = mpD;
		strengthDamage = strD;
		strengthAccuracy = strAc;
		aptitudeDamage = aptD;
		aptitudeAccuracy = aptAc;
		charismaDamage = chaD;
		charismaAccuracy = chaAc;
		hitPointsEvasion = hpEv;
		mentalPointsEvasion = mpEv;
	}
	
}
