using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

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
	public static float HPDefenseEquip = 0f; // The reduction of HP damage conferred by armor.
	public static float MPDefenseEquip = 0f; // The reduction of MP damage conferred by accessories.
	public static float HPEvasionEquip = 0f; // Whatever bonus to Agility is conferred by equipment.
	public static float MPEvasionEquip = 0f; // Whatever bonus to Cunning is conferred by equipment.
	
	public static float strDamageMod = 0f; // The effect of temporary buffs/debuffs on Strength damage.
	public static float strAccMod = 0f; // The effect of temporary buffs/debuffs on Strength accuracy.
	public static float aptDamageMod = 0f; // The effect of temporary buffs/debuffs on Aptitude damage.
	public static float aptAccMod = 0f; // The effect of temporary buffs/debuffs on Aptitude accuracy.
	public static float chaDamageMod = 0f; // The effect of temporary buffs/debuffs on Charisma damage.
	public static float chaAccMod = 0f; // The effect of temporary buffs/debuffs on Charisma accuracy.
	public static float HPDefenseMod = 0f; // The effect of temporary buffs/debuffs on HP defense.
	public static float MPDefenseMod = 0f; // The effect of temporary buffs/debuffs on MP defense.
	public static float HPEvasionMod = 0f; // The effect of temporary buffs/debuffs on HP evasion.
	public static float MPEvasionMod = 0f; // The effect of temporary buffs/debuffs on MP evasion.
	
	// Here are the secondary stats. Use these for combat.
	
	public static float strDamage = strength + strDamageEquip + strDamageMod;
	public static float strAcc = strength + strAccEquip + strAccMod;
	public static float aptDamage = aptitude + aptDamageEquip + aptDamageMod;
	public static float aptAcc = aptitude + aptAccEquip + aptAccMod;
	public static float chaDamage = charisma + chaDamageEquip + chaDamageMod;
	public static float chaAcc = charisma + chaAccEquip + chaAccMod;
	public static float HPDefense = HPDefenseEquip + HPDefenseMod; // There's no HP defense stat. That would be redundant with HP.
	public static float MPDefense = MPDefenseEquip + MPDefenseMod; // Likewise, there's no MP defense stat; there's just MP itself.

	public static void Heal(float healValue)
	{
		if((hp+healValue) > 100.0f)
			hp = 100.0f;
		else
			hp += healValue;
	}
}
