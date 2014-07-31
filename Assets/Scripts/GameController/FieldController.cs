using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldController : MonoBehaviour 
{
	public Texture seanTexture;
	private GameObject player;
	private GameObject HPText;
	private GameObject MPText;
	private MenuController menuController;
	private bool showMenu = false;
	private bool showInventory = false;
	private bool haveInitialized = false;

	// Inventory vars
	public GUIStyle defaultStyle;
	public GUIStyle clickedStyle;
	public Vector2 invSlotSize = new Vector2(400,25);	// Size of each inventory slot
	public Vector2 invInitPos = new Vector2(300,100);	// Coordinates for the top left corner of the inventory
	private Rect invRect;	// Inventory box
	private string[] slotStrings = new string[10];
	private GUIStyle[] slotStyles = new GUIStyle[10];
	private List<Rect> rectList = new List<Rect>();
	private int currentPage = 1;
	private int itemIndex;
	private int clickedIndex;
	private bool showConfirm = false;
	//	private bool hasChangedPage = false; 
	
	// Status vars
	public Rect statusRect = new Rect(675, 50, 200, 400);	// Status box
	private bool showStatus = false;

	void Awake ()
	{
		invRect = new Rect(invInitPos.x, invInitPos.y, invSlotSize.x, invSlotSize.y*10.0f);

		for(int i=0; i<PlayerInfo.equipment.Length; i++)
		{
			PlayerInfo.equipment[i] = new Equipment();
		}

		Debug.Log("Equipment in slot 0 is :"+PlayerInfo.equipment[0].itemName);
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
		menuController = GameObject.Find("FieldStatus").GetComponent<MenuController>();

		player.transform.position = FieldInfo.lastPlayerPosition;
		
		// Initializing current stats
		
		PlayerInfo.UpdateStats();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			Debug.Log("Adding "+ItemList.excalibur.itemName);
			Inventory.AddItem(ItemList.excalibur);
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			Debug.Log("Adding "+ItemList.woodenSword.itemName);
			Inventory.AddItem(ItemList.woodenSword);
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
			Debug.Log("Hurting player");
			PlayerInfo.hp -= 20.0f;
			PlayerInfo.mp -= 20.0f;
		}
		
		HPText.guiText.text = PlayerInfo.hp.ToString();
		MPText.guiText.text = PlayerInfo.mp.ToString();

		// OK, so I'm going to leave MenuController alone, and have it ONLY for the field menu, just because I like your portrait thing, and I think it'd be a pain to texture a button and the text. We can figure out a way later
		if (menuController.GetOpenedState())
			showMenu = true;
		else
			showMenu = false;
			
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log("The attack power of your weapon is "+PlayerInfo.strDamageEquip);
			Debug.Log("Your overall attack power is "+PlayerInfo.strDamage);
		}
	}

	void OnGUI()
	{
		if(!haveInitialized)
			InitStyles();

		if(showInventory)
		{
			float vertShift = invSlotSize.y;	// This isn't necessary of course, but I think it looks cleaner
			int slotCount = 0;
			int index = 0;
			Consumable con;
			Equipment equip;

			// This only generates the inventory bounding box
			GUI.Box (new Rect(invRect), "");

			// This weird-ass math is a result of wanting the current page to start at 1, and not 0
			// So for example, if we were on page 3, we're checking to see if there are less than 30 items
			if(Inventory.invList.Count == 0)
			{
				// Don't show anything but the inventory box
			}
			else if(Inventory.invList.Count < 10+((currentPage-1)*10))
			{
				slotCount = Inventory.invList.Count-((currentPage-1)*10); // This should return the singles digit
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
				

				if(Inventory.invList[index] is Consumable)
				{
					con = (Consumable)Inventory.invList[index];
					slotStrings[i] = con.itemName + " (" + Inventory.invCount[index].ToString() + ")";
				}
				else if(Inventory.invList[index] is Equipment)
				{
					equip = (Equipment)Inventory.invList[index];
					slotStrings[i] = equip.itemName + " (" + Inventory.invCount[index].ToString() + ")";
				}

				// Because this is in the for loop, this will generate the GUI.Box for every necessary inventory item
				GUI.Box(new Rect(invInitPos.x, invInitPos.y+(vertShift*i), invSlotSize.x, invSlotSize.y), slotStrings[i], slotStyles[i]);

				if (rectList.Count < slotCount)
				{
					rectList.Add(new Rect(invInitPos.x, invInitPos.y+(vertShift*i), invSlotSize.x, invSlotSize.y));
					Debug.Log("Adding item to rectlist");
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
		
		// This is the status screen code. So long wheeeeee
		if (showStatus == true)
		{
			GUI.Box(statusRect, "Current Parameters\n\nHP: "+PlayerInfo.hp+"\nMP: "+PlayerInfo.mp+"\n\nStrength: "+PlayerInfo.strength+"\nAptitude: "+PlayerInfo.aptitude+"\nCharisma: "+PlayerInfo.charisma+"\nAgility: "+PlayerInfo.agility+"\nCunning: "+PlayerInfo.cunning+"\n\nDefense: "+PlayerInfo.HPDefense+"\nMental Defense: "+PlayerInfo.MPDefense+"\nPhysical Power: "+PlayerInfo.strDamage+"\nPhysical Finesse: "+PlayerInfo.strAcc+"\nMagical Power: "+PlayerInfo.aptDamage+"\nMagical Finesse: "+PlayerInfo.aptAcc+"\nSocial Power: "+PlayerInfo.chaDamage+"\nSocial Finesse: "+PlayerInfo.chaAcc+"\nPhysical Evasion: "+PlayerInfo.HPEvasion+"\nMental Alertness: "+PlayerInfo.MPEvasion+"\n\nAchievements\n\nSlain the Schmoo: "+GameState.SchmooSlain);

			if(Input.GetMouseButtonDown(0))
			{
				Event e = Event.current;

				if(!statusRect.Contains(e.mousePosition))
					showStatus = false;
			}
		}

		if (showConfirm)
		{
			Debug.Log("Showing confirmation now");
			if (GUI.Button(new Rect(0.5f*Screen.width-50.0f,0.5f*Screen.height, 80, 30), "YES!"))
			{
				Debug.Log("Yes has been clicked");
				if(Inventory.invList[itemIndex] is Consumable)
				{
					Consumable item = (Consumable)Inventory.invList[itemIndex];
					Debug.Log("Using a "+item.itemName+", there were "+Inventory.invCount[itemIndex].ToString());
					PlayerInfo.UseConsumable(item);
					Inventory.invCount[itemIndex]--;
					Debug.Log("...and now there are "+Inventory.invCount[itemIndex].ToString());
				}
				else if(Inventory.invList[itemIndex] is Equipment)
				{
					Debug.Log("We've found an equipment item. We know where this is");
					Equipment item = (Equipment)Inventory.invList[itemIndex];

					if(PlayerInfo.equipment[item.slot].itemName != "")
					{
						PlayerInfo.UnequipItem(item.slot);
						Debug.Log("Unequipping a "+PlayerInfo.equipment[item.slot].itemName);
					}

					PlayerInfo.EquipItem(item);
					Debug.Log("Equipping a "+item.itemName);
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

		if (showMenu)
		{
			float xShift = 100.0f;

//			GUILayout.BeginHorizontal();
//			if(GUI.Button(new Rect(20, 300, 80, 30), "Equipment"))
//			{
//			}
//			if(GUILayout.Button("Abilities"))
//			{
//			}
//			if(GUILayout.Button("Items"))
//			{
//			}
//			if(GUILayout.Button("Status"))
//			{
//			}
//			GUILayout.EndHorizontal();

			if(GUI.Button(new Rect(20, 600, 80, 30), "Equipment"))
			{
				// Go to equip menu
			}

			if (GUI.Button(new Rect(20+xShift, 600, 80, 30), "Abilities"))
			{
				// Go to abilities menu
			}

			if (GUI.Button (new Rect(20+(2*xShift), 600, 80, 30), "Items"))
			{
				showInventory = true;
			}

			if (GUI.Button (new Rect(20+(3*xShift), 600, 80, 30), "Status"))
			{
				showStatus = true;
			}
		}
	}


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
