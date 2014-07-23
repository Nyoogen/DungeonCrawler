using UnityEngine;
using System.Collections;

public class GeneralController : MonoBehaviour 
{
	private GameObject player;
	private GameObject HPText;
	private GameObject MPText;

	void Awake ()
	{
		if (FieldInfo.shouldDestroy)
		{
			Debug.Log("trying to destroy");
			// Make sure that lastEncounteredObject exists
			if (FieldInfo.lastEncounteredObjectTag != null)
				Destroy(GameObject.FindGameObjectWithTag(FieldInfo.lastEncounteredObjectTag));
		}

		player = GameObject.FindGameObjectWithTag("Player");
		HPText = GameObject.Find("/FieldStatus/HPText");
		MPText = GameObject.Find("/FieldStatus/MPText");
		player.transform.position = FieldInfo.lastPlayerPosition;
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			Debug.Log("HP is: "+PlayerInfo.hp);
			Debug.Log("MP is: "+PlayerInfo.mp);
		}

		if (Input.GetKeyDown (KeyCode.H))
		{
			Debug.Log ("Halving HP and MP");
			PlayerInfo.hp = 0.5f*PlayerInfo.hp;
			PlayerInfo.mp = 0.5f*PlayerInfo.mp;
		}
		
		HPText.guiText.text = PlayerInfo.hp.ToString();
		MPText.guiText.text = PlayerInfo.mp.ToString();
	}
}
