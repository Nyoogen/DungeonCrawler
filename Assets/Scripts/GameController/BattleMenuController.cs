using UnityEngine;
using System.Collections;

public class BattleMenuController : MonoBehaviour 
{
	void OnMouseDown()
	{
		// The only unique identifier of these menu boxes is the guiText, so compare against them
		if (gameObject.transform.FindChild("MenuLabel").guiText.text == "Attack")
		{
			EnemyInfo.hp -= 20;
		}

	}
}
