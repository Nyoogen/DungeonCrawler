﻿using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour 
{
	private GameObject hpText;
	private GameObject mpText;
	private SceneChanger sceneChanger;

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
//		if (Input.GetKey(KeyCode.A))
//		{
//			PlayerInfo.hp -= 2;
//			PlayerInfo.mp -= 5;
//			hpText.guiText.text = PlayerInfo.hp.ToString();
//			mpText.guiText.text = PlayerInfo.mp.ToString();
//		}

		// Just for testing battle end
		if (Input.GetKeyDown(KeyCode.E))
		{
			sceneChanger.ChangeScene("Field");

		}
	}
}
