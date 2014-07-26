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
	public Texture swatch_black_dff;
	private SceneChanger sceneChanger;
	
	void Awake()
	{
		sceneChanger = GetComponent<SceneChanger>();
	}
        
	void OnGUI()
	{
		GUI.skin.box.wordWrap = true;
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
				showButtons = false;
				sceneChanger.ChangeScene("Field");
			}
			
		}
		if (showDialogueStore)
		{
			GUI.Box(new Rect(50, 50, 300, 300), horseHead);
			GUI.Box(new Rect(350, 50, 300, 300), "Hello, friend. Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh.");
			
		}
		if (showDialogueInn)
		{
			GUI.Box(new Rect(50, 50, 300, 300), dJdicpic);
			if (PlayerInfo.SchmooSlain == false)
			{
				GUI.Box(new Rect(350, 50, 300, 100), "Slay the schmoo, bro!");
			}
			if (PlayerInfo.SchmooSlain == true)
			{
				GUI.Box(new Rect(350, 50, 300, 100), "You've slain the schmoo, bro!");
			}
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
