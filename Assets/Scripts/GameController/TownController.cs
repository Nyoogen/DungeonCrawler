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
	private SceneChanger sceneChanger;
	public GUISkin GUISkin;
	public GUIStyle DialogueImage;
	
	// public Rect shopBuyWindow = new Rect(300, 200, 400, 200);
	// private bool showWindow = false;
	
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
				// showDialogue = true;
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
			GUI.Box(new Rect(50, 50, 300, 300), horseHead, DialogueImage);
			GUI.Box(new Rect(350, 50, 300, 100), "Hello, friend. Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh Neigh.");
			if (GUI.Button(new Rect(400, 200, 200, 100), "Buy Potion"))
			{
				int index = Inventory.invList.IndexOf(ItemList.potion);
				if (index < 0)
				{
					Debug.Log("Potion doesn't exist, adding it to inventory");
					Inventory.invList.Add(ItemList.potion);
					Inventory.invCount.Add(1);
				}
				else
				{
					Debug.Log("Adding a potion to the inventory");
					Inventory.invCount[index]++;
					Debug.Log("Number of potions after adding: "+Inventory.invCount[index]);
				}
			}
			if (GUI.Button(new Rect(400, 320, 200, 100), "Sell Potion"))
			{
				int index = Inventory.invList.IndexOf(ItemList.potion);
				if (index < 0)
				{
					Debug.Log ("You have no potions to sell, bro.");
				}
				else if (Inventory.invCount[index] == 1)
				{
					Debug.Log ("You sold your last potion.");
                    Inventory.invList.RemoveAt(index);
                    Inventory.invCount.RemoveAt(index);
                }
                else if (Inventory.invCount[index] > 1)
                {
                    Inventory.invCount[index]--;
                    Debug.Log ("Number of potions after selling: "+Inventory.invCount[index]);
                }
            }
			
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
			
		}
		// if (showWindow == true)
		// {
		// 	shopBuyWindow = GUI.Window(0, shopBuyWindow, OpenShopBuyWindow, "Here's what we have available, bro!");
		// }
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
			// showWindow = false;
		}
	}
	
	// void OpenShopBuyWindow(int windowID) {
	// Doesn't do anything yet.
	// }
			
}
