using UnityEngine;
using System.Collections;

public class TownController : MonoBehaviour 
{
	private bool showButtons = true;
	private bool showDialogue = false;
	private bool showDialogueStore = false;
	private bool showDialogueInn = false;
	public Texture horseHead;
	public Texture dJdicpic;

	void OnGUI()
	{
		if (showButtons)
		{
			float xShift = 200.0f;
			if(GUI.Button(new Rect(280, 550, 160, 60), "Haberdasher"))
			{
				showButtons = false;
				showDialogue = true;
				showDialogueStore = true;
			}
			
			if (GUI.Button(new Rect(280+xShift, 550, 160, 60), "Caravansary"))
			{
				showButtons = false;
				showDialogue = true;
				showDialogueInn = true;
			}
			
			if (GUI.Button (new Rect(280+(2*xShift), 550, 160, 60), "Abyss"))
			{
				Application.LoadLevel("Field");
			}
			
		}
		if (showDialogueStore)
		{
			GUI.Box(new Rect(50, 50, 300, 300), horseHead);
			GUI.Box(new Rect(350, 50, 100, 100), "Hello, friend.");
		}
		if (showDialogueInn)
		{
			GUI.Box(new Rect(50, 50, 300, 300), dJdicpic);
			GUI.Box(new Rect(350, 50, 300, 100), "Broooooooooooooooooo!");
		}
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0)) // 0 always means left mouse button. This is hard-coded.
		{
			if(showDialogue)
			{
				showButtons = true;
				showDialogue = false;
				showDialogueStore = false;
				showDialogueInn = false;
			}
		}
	}		
}
