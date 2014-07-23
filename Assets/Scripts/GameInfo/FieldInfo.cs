﻿using UnityEngine;
using System.Collections;

public class FieldInfo : MonoBehaviour {

	// lastPlayerPosition is given a default position for testing purposes
	public static Vector3 lastPlayerPosition = new Vector3(-1.0f, 0.0f, 0.0f);
	public static bool shouldDestroy = false;
	public static string lastEncounteredObjectTag = null;


	// Debugging
	public static void PrintInfo()
	{
		Debug.Log("Last player position: "+lastPlayerPosition);
		Debug.Log("shouldDestroy: "+shouldDestroy);
		Debug.Log("Last encountered object: "+lastEncounteredObjectTag);

	}
}
