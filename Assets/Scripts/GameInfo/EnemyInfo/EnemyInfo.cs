using UnityEngine;
using System.Collections;

public class EnemyInfo
{
	public static string enemyName = "";
	public static float hp = 0f;
	public static float mp = 0f;
	public static float strDamage = 0f;
	public static float strAcc = 0f;
	public static float aptDamage = 0f;
	public static float aptAcc = 0f; 
	public static float chaDamage = 0f; 
	public static float chaAcc = 0f; 
	public static float hpDefense = 0f;
	public static float mpDefense = 0f;
	public static float hpEvasion = 0f;
	public static float mpEvasion = 0f;
	public static float speed = 0f;

	public static void SetEnemyInfo(Enemy enemy)
	{
		enemyName = enemy.enemyName;
		hp = enemy.hitPoints;
		mp = enemy.mentalPoints;
		strDamage = enemy.strengthDamage;
		strAcc = enemy.strengthAccuracy;
		aptDamage = enemy.aptitudeDamage;
		aptAcc = enemy.aptitudeAccuracy;
		chaDamage = enemy.charismaDamage;
		chaAcc = enemy.charismaAccuracy;
		hpDefense = enemy.hitPointsDefense;
		mpDefense = enemy.mentalPointsDefense;
		hpEvasion = enemy.hitPointsEvasion;
		mpEvasion = enemy.mentalPointsEvasion;
		speed = enemy.initiative;
	}
}
