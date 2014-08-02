using UnityEngine;
using System.Collections;

public class PlayerInfo
{
	public static Equipment[] equipment = new Equipment[9];	// We can put a different number in the brackets once we know how many pieces of equipment we want
															// For now: Main hand, offhand, Head, Body, Hands, Legs, Feet, Neck, Ring	

	public static float hp = 100.0f;
	public static float mp = 100.0f;

	public static float strength = 10f; // Determines damage and accuracy of Attack / Strength abilities, which damage HP.
	public static float aptitude = 10f; // Determines damage and accuracy of Aptitude abilites, which damage HP or MP.
	public static float charisma = 10f; // Determines damage and accuracy of Mental Attack / Charisma abilities, which damage MP.
	public static float agility = 10f; // Determines evasion of attacks which damage HP (Strength/Aptitude), and turn order.
	public static float cunning = 10f; // Determines evasion of attacks which damage MP (Charisma/Aptitude), and field effects stuff.
	
	// What follows are the secondary stats and the secondary advantages conferred by equipment. On the primary stat level, damage
	// and accuracy are the same, but are modified by the equipment you equip, the buffs/debuffs in effect, and the specific ability
	// you use. Secondary stats are used in combat; primary stats will probably be the prereqs for new skills in advancement.
	
	public static float strDamageEquip = 0f; // The HP damage benefit of a weapon (or accessory, potentially) for Str abilities.
	public static float strAccEquip = 0f; // A weapon's accuracy for Str abilities.
	public static float aptDamageEquip = 0f; // The HP/MP damage benefit of a magic tool or spellbook for Apt abilities.
	public static float aptAccEquip = 0f; // The accuracy benefit of a magic tool for Apt abilities.
	public static float chaDamageEquip = 0f; // The MP damage benefit of an outfit or accessory for Cha abilities.
	public static float chaAccEquip = 0f; // The accuracy beneift of an outfit or accessory for Cha abilities.
	public static float hpDefenseEquip = 0f; // The reduction of HP damage conferred by armor.
	public static float mpDefenseEquip = 0f; // The reduction of MP damage conferred by accessories.
	public static float hpEvasionEquip = 0f; // Whatever bonus to Agility is conferred by equipment.
	public static float mpEvasionEquip = 0f; // Whatever bonus to Cunning is conferred by equipment.
	
	public static float strDamageMod = 0f; // The effect of temporary buffs/debuffs on Strength damage.
	public static float strAccMod = 0f; // The effect of temporary buffs/debuffs on Strength accuracy.
	public static float aptDamageMod = 0f; // The effect of temporary buffs/debuffs on Aptitude damage.
	public static float aptAccMod = 0f; // The effect of temporary buffs/debuffs on Aptitude accuracy.
	public static float chaDamageMod = 0f; // The effect of temporary buffs/debuffs on Charisma damage.
	public static float chaAccMod = 0f; // The effect of temporary buffs/debuffs on Charisma accuracy.
	public static float hpDefenseMod = 0f; // The effect of temporary buffs/debuffs on HP defense.
	public static float mpDefenseMod = 0f; // The effect of temporary buffs/debuffs on MP defense.
	public static float hpEvasionMod = 0f; // The effect of temporary buffs/debuffs on HP evasion.
	public static float mpEvasionMod = 0f; // The effect of temporary buffs/debuffs on MP evasion.
	
	// Here are the secondary stats. Use these for combat.
	
	public static float strDamage;
	public static float strAcc;
	public static float aptDamage;
	public static float aptAcc; 
	public static float chaDamage; 
	public static float chaAcc; 
	public static float hpDefense;
	public static float mpDefense;
	public static float hpEvasion;
	public static float mpEvasion;
	
	public static float currency = 1000f; // I'll need an update script somewhere, in town at least.
    
	public static void UseConsumable(Consumable item)
	{
		if((hp+item.hitPoints) > 100.0f)
			hp = 100.0f;
		else
			hp += item.hitPoints;

		if((mp+item.mentalPoints) > 100.0f)
			mp = 100.0f;
		else
			mp += item.mentalPoints;
	}

	public static void UseConsumable(Consumable item, string[] effects)
	{
		UseConsumable(item);
		// Theoretically heal status effects here
	}

	public static void EquipItem(Equipment item)
	{
		strDamageEquip += item.strengthDamage;
		strAccEquip    += item.strengthAccuracy;
		aptDamageEquip += item.aptitudeDamage;
		aptAccEquip    += item.aptitudeAccuracy;
		chaDamageEquip += item.charismaDamage;
		chaAccEquip    += item.charismaAccuracy;
		hpDefenseEquip += item.hitPointsDefense;
		mpDefenseEquip += item.mentalPointsDefense;
		hpEvasionEquip += item.hitPointsEvasion;
		mpEvasionEquip += item.mentalPointsEvasion;
		equipment[item.slot] = item;
		UpdateStats();
	}

	public static void UnequipItem(int slot)
	{
		strDamageEquip -= equipment[slot].strengthDamage;
		strAccEquip    -= equipment[slot].strengthAccuracy;
		aptDamageEquip -= equipment[slot].aptitudeDamage;
		aptAccEquip    -= equipment[slot].aptitudeAccuracy;
		chaDamageEquip -= equipment[slot].charismaDamage;
		chaAccEquip    -= equipment[slot].charismaAccuracy;
		hpDefenseEquip -= equipment[slot].hitPointsDefense;
		mpDefenseEquip -= equipment[slot].mentalPointsDefense;
		hpEvasionEquip -= equipment[slot].hitPointsEvasion;
		mpEvasionEquip -= equipment[slot].mentalPointsEvasion;
		equipment[slot] = new Equipment();
	}
	
	public static void UpdateStats()
	{
		strDamage = strength + strDamageEquip + strDamageMod;
		strAcc = strength + strAccEquip + strAccMod;
		aptDamage = aptitude + aptDamageEquip + aptDamageMod;
		aptAcc = aptitude + aptAccEquip + aptAccMod;
		chaDamage = charisma + chaDamageEquip + chaDamageMod;
		chaAcc = charisma + chaAccEquip + chaAccMod;
		hpDefense = hpDefenseEquip + hpDefenseMod; // There's no HP defense stat. That would be redundant with HP.
		mpDefense = mpDefenseEquip + mpDefenseMod; // Likewise, there's no MP defense stat; there's just MP itself.
		hpEvasion = agility + hpEvasionEquip + hpEvasionMod;
		mpEvasion = cunning + mpEvasionEquip + mpEvasionMod;
	}
	
	// I'm putting my rudimentary currency value in here for now.
	
	public static bool Purchase(float value)
	{
		Debug.Log("Current currency: "+currency);
		Debug.Log("Received value: "+value);
		if (currency - value < 0f)
		{
			Debug.Log("After denial: "+currency);
			return false;
		}
		else 
		{
			currency -= value;
			Debug.Log("After purchasing: "+currency);
			return true;
		}
		
	}
}
