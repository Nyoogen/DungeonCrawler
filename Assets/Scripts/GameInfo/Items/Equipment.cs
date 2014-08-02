using UnityEngine;
using System.Collections;

public class Equipment
{
	public string itemName;
	public int slot;					// We'll use integers to denote slot. For now: 1) Main hand, 2) Off Hand, 3) Head, 4) Body, 5) Hands, 6) Legs, 7) Feet, 8) Neck, 9) Ring
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
	public float initiative;


	// Default constructor
	public Equipment()
	{
		itemName = "";
		slot = -1;
		hitPointsDefense = 0f;
		mentalPointsDefense = 0f;
		strengthDamage = 0f;
		strengthAccuracy = 0f;
		aptitudeDamage = 0f;
		aptitudeAccuracy = 0f;
		charismaDamage = 0f;
		charismaAccuracy = 0f;
		hitPointsEvasion = 0f;
		mentalPointsEvasion = 0f;
		initiative = 0f;
	}

	public Equipment(string name, int slotNum, float hpD, float mpD, float strD, float strAc, float aptD, float aptAc, float chaD, float chaAc, float hpEv, float mpEv, float speed)
	{
		itemName = name;
		slot = slotNum;
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
		initiative = speed;
	}
}
