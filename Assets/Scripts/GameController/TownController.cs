using UnityEngine;
using System.Collections;

public class TownController : MonoBehaviour 
{
	private bool showButtons = true;

	void OnGUI()
	{
		if (showButtons)
		{
			float xShift = 200.0f;
			if(GUI.Button(new Rect(280, 550, 160, 60), "Haberdasher"))
			{
				// Go to equip menu
			}
			
			if (GUI.Button(new Rect(280+xShift, 550, 160, 60), "Caravansary"))
			{
				// Go to abilities menu
			}
			
			if (GUI.Button (new Rect(280+(2*xShift), 550, 160, 60), "Abyss"))
			{
				// Go to items menu
			}
			
		}
	}
}
