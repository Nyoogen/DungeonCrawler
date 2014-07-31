using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	// My attempt to implement a shop code using Eugene's inventory code
	private bool showInventory = false;
	private bool haveInitialized = false;
	public GUIStyle defaultStyle;
	public GUIStyle clickedStyle;
	public Vector2 invSlotSize = new Vector2(400,25);	// Size of each inventory slot
	public Vector2 invInitPos = new Vector2(300,100);	// Coordinates for the top left corner of the inventory
	private Rect invRect;	// Inventory box
	private string[] slotStrings = new string[10];
	private GUIStyle[] slotStyles = new GUIStyle[10];
	private int currentPage = 1;
	private int itemIndex;
	private int clickedIndex;
	private bool showConfirm = false;
	private StoreInventory storeInventory;
	
	void Awake()
	{
		sceneChanger = GetComponent<SceneChanger>();
		
		// Shop code
		invRect = new Rect(invInitPos.x, invInitPos.y, invSlotSize.x, invSlotSize.y*10.0f);
		
		storeInventory = GetComponent<StoreInventory>();
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

			if (GUI.Button(new Rect(400, 200, 200, 100), "Buy"))
				showInventory = true;

			// if (GUI.Button(new Rect(400, 200, 200, 100), "Buy Potion"))
			//	Inventory.AddItem(ItemList.potion);

			// if (GUI.Button(new Rect(400, 320, 200, 100), "Sell Potion"))
			//	Inventory.RemoveItem(ItemList.potion);
            
			// if (GUI.Button(new Rect(650, 200, 200, 100), "Buy Ether"))
			//	Inventory.AddItem(ItemList.ether);

			// if (GUI.Button(new Rect(650, 320, 200, 100), "Sell Ether"))
			//	Inventory.RemoveItem(ItemList.ether);

			if (GUI.Button(new Rect(400, 440, 200, 100), "Exit"))
			{
				showButtons = true;
				showDialogueStore = false;
            }
            
        }
        
        // Epic inventory code go!
        
		if(!haveInitialized)
			InitStyles();
		
		if(showInventory)
		{
			float vertShift = invSlotSize.y;	// This isn't necessary of course, but I think it looks cleaner
			int slotCount = 0;
			int index = 0;
			Consumable con;
			Equipment equip;
			List<Rect> rectList = new List<Rect>();
			
			
			// This only generates the inventory bounding box
			GUI.Box (new Rect(invRect), "");
			
			// This weird-ass math is a result of wanting the current page to start at 1, and not 0
			// So for example, if we were on page 3, we're checking to see if there are less than 30 items
			if(storeInventory.storeList.Count == 0)
			{
				// Don't show anything but the inventory box
			}
			else if(storeInventory.storeList.Count < 10+((currentPage-1)*10))
			{
				slotCount = storeInventory.storeList.Count-((currentPage-1)*10); // This should return the singles digit
				// i.e. if there were 28 items, slotCount should be 8
			}
			else
			{
				slotCount = 10;
			}
			
			// This generates the visible list of items
			for(int i=0; i<slotCount; i++)
			{
				index = i+((currentPage-1)*10);
				
				
				if(storeInventory.storeList[index] is Consumable)
				{
					con = (Consumable)storeInventory.storeList[index];
					slotStrings[i] = con.itemName + " (" + storeInventory.costList[index].ToString() + ")";
				}
				else if(storeInventory.storeList[index] is Equipment)
				{
					equip = (Equipment)storeInventory.storeList[index];
					slotStrings[i] = equip.itemName + " (" + storeInventory.costList[index].ToString() + ")";
				}
				
				// Because this is in the for loop, this will generate the GUI.Box for every necessary inventory item
				GUI.Box(new Rect(invInitPos.x, invInitPos.y+(vertShift*i), invSlotSize.x, invSlotSize.y), slotStrings[i], slotStyles[i]);
				
				if (rectList.Count < slotCount)
				{
					rectList.Add(new Rect(invInitPos.x, invInitPos.y+(vertShift*i), invSlotSize.x, invSlotSize.y));
				}
			}
			
			if(Input.GetMouseButtonDown(0))
			{
				Event e = Event.current;
				for(int i=0; i<rectList.Count; i++)
				{
					if(rectList[i].Contains(e.mousePosition))
					{
						// Reset all the styles (to the "unclicked" state)
						for(int j=0; j<slotCount; j++)
						{
							slotStyles[j] = defaultStyle;
						}
						
						// Then change the one clicked item to the "clicked" state
						slotStyles[i] = clickedStyle;
						itemIndex = i+((currentPage-1)*10);
						clickedIndex = i;
						showConfirm = true;
						break;
					}
				}
				
				if(!invRect.Contains(e.mousePosition))
				{
					showInventory = false;
					showConfirm = false;
				}
				
			}
			
			
		}
		
		// I need to adapt showConfirm so that it simply adds to inventory.
		
		if (showConfirm)
		{
			if (GUI.Button(new Rect(0.5f*Screen.width-50.0f,0.5f*Screen.height, 80, 30), "YES!"))
			{
				Debug.Log("The cost is: "+storeInventory.costList[clickedIndex]);
				bool currencyCheck = PlayerInfo.Purchase(storeInventory.costList[clickedIndex]);
				if(currencyCheck == true)
				{
					if(storeInventory.storeList[clickedIndex] is Consumable)
					{
						Consumable con = (Consumable)storeInventory.storeList[clickedIndex];
						Inventory.AddItem(con);
					}
					else if(storeInventory.storeList[clickedIndex] is Equipment)
					{
						Equipment equip = (Equipment)storeInventory.storeList[clickedIndex];
						Inventory.AddItem(equip);
					}		
				}
				else
				{
					Debug.Log("You can't buy this!");
				}
				
				// "Unclick" the item
				slotStyles[clickedIndex] = defaultStyle;
				
				// Close confirmation dialog box
				showConfirm = false;
			}
			
			if (GUI.Button(new Rect(0.5f*Screen.width+50.0f,0.5f*Screen.height, 80, 30), "NO!"))
			{
				// Do nothing
				
				// "Unclick" the item
				slotStyles[clickedIndex] = defaultStyle;
				
				// Close confirmation dialog box
				showConfirm = false;
			}
		}
		
		// Epic inventory code end!
        
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
		
		// Display currency
		GUI.Box (new Rect(675, 500, 100, 30), "Money: "+PlayerInfo.currency+"G");
		
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown(0)) // 0 always means left mouse button. This is hard-coded.
		{
		showStatus = false;
		}
	}
	
	// TownController InitStyles and MakeTex for storeInventory
	
	private void InitStyles()
	{
		defaultStyle = new GUIStyle(GUI.skin.box);
		clickedStyle = new GUIStyle(GUI.skin.box);
		clickedStyle.normal.background = MakeTex(2,2,Color.gray);
		
		for(int i=0; i<slotStrings.Length; i++)
		{
			slotStrings[i] = "";
		}
		
		for(int i=0; i<slotStyles.Length; i++)
		{
            slotStyles[i] = defaultStyle;
        }
        
        haveInitialized = true;
	}
	
	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[i] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
        result.Apply();
        return result;
    }
    
}
