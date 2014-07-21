using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour 
{
	private GameObject hpText;
	private GameObject mpText;

	// Use this for initialization
	void Start () 
	{
		hpText = GameObject.Find("HPText");
		mpText = GameObject.Find("MPText");
		hpText.guiText.text = PlayerInfo.hp.ToString();
		mpText.guiText.text = PlayerInfo.mp.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.A))
		{
			PlayerInfo.hp -= 2;
			PlayerInfo.mp -= 5;
			hpText.guiText.text = PlayerInfo.hp.ToString();
			mpText.guiText.text = PlayerInfo.mp.ToString();
		}
	}
}
