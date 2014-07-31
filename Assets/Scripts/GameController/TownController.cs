using UnityEngine;
using System.Collections;

public class TownController : MonoBehaviour 
{
	private bool showButtons = true;
	private bool showDialogueStore = false;
	private bool showDialogueInn = false;
	public Texture horseHead;
	public Texture dJdicpic;
	private SceneChanger sceneChanger;
	public GUISkin GUISkin;
	public GUIStyle DialogueImage;
	
	private bool showStatus = false;
	
	void Awake()
	{
		sceneChanger = GetComponent<SceneChanger>();
	}
        
	void OnGUI()
	{
		GUI.skin = GUISkin;
		if (showButtons)
		{
			float xShift = 200.0f;
			if(GUI.Button(new Rect(280, 550, 160, 60), "Haberdasher"))
			{
				showButtons = false;
				showDialogueStore = true;
			}
			
			if (GUI.Button(new Rect(280+xShift, 550, 160, 60), "Caravansary"))
			{
				showButtons = false;
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
			GUI.Box(new Rect(50, 50, 300, 300), horseHead, DialogueImage);
			GUI.Box(new Rect(350, 50, 300, 100), "Hello, friend. Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh.");

			if (GUI.Button(new Rect(400, 200, 200, 100), "Buy Potion"))
				Inventory.AddItem(ItemList.potion);

			if (GUI.Button(new Rect(400, 320, 200, 100), "Sell Potion"))
				Inventory.RemoveItem(ItemList.potion);
            
			if (GUI.Button(new Rect(650, 200, 200, 100), "Buy Ether"))
				Inventory.AddItem(ItemList.ether);

			if (GUI.Button(new Rect(650, 320, 200, 100), "Sell Ether"))
				Inventory.RemoveItem(ItemList.ether);

			if (GUI.Button(new Rect(400, 440, 200, 100), "Exit"))
			{
				showButtons = true;
				showDialogueStore = false;
            }
            
        }
        if (showDialogueInn)
        {
            GUI.Box(new Rect(50, 50, 300, 300), dJdicpic, DialogueImage);
			if (GameState.SchmooSlain == false)
			{
				GUI.Box(new Rect(350, 50, 300, 100), "Slay the schmoo, bro!");
			}
			if (GameState.SchmooSlain == true)
			{
				GUI.Box(new Rect(350, 50, 300, 100), "You've slain the schmoo, bro!");
			}
			if (GUI.Button(new Rect(400, 200, 200, 100), "View Status"))
			{
				showStatus = true;
			}
			if (GUI.Button(new Rect(400, 320, 200, 100), "Exit"))
			{
				showButtons = true;
				showDialogueInn = false;
			}
		}
		if (showStatus == true)
		{
			GUI.Box(new Rect(675, 50, 200, 400), "Current Parameters\n\nHP: "+PlayerInfo.hp+"\nMP: "+PlayerInfo.mp+"\n\nStrength: "+PlayerInfo.strength+"\nAptitude: "+PlayerInfo.aptitude+"\nCharisma: "+PlayerInfo.charisma+"\nAgility: "+PlayerInfo.agility+"\nCunning: "+PlayerInfo.cunning+"\n\nDefense: "+PlayerInfo.HPDefense+"\nMental Defense: "+PlayerInfo.MPDefense+"\nPhysical Power: "+PlayerInfo.strDamage+"\nPhysical Finesse: "+PlayerInfo.strAcc+"\nMagical Power: "+PlayerInfo.aptDamage+"\nMagical Finesse: "+PlayerInfo.aptAcc+"\nSocial Power: "+PlayerInfo.chaDamage+"\nSocial Finesse: "+PlayerInfo.chaAcc+"\nPhysical Evasion: "+PlayerInfo.HPEvasion+"\nMental Alertness: "+PlayerInfo.MPEvasion+"\n\nAchievements\n\nSlain the Schmoo: "+GameState.SchmooSlain);
		}
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0)) // 0 always means left mouse button. This is hard-coded.
		{
		showStatus = false;
		}
	}
			
}
