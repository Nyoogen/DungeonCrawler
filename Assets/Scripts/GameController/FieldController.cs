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
	private string[] slotStrings = new string[10];
	private GUIStyle[] slotStyles = new GUIStyle[10];
	private List<Rect> rectList = new List<Rect>();
	private int currentPage = 1;
	private int itemIndex;
	private int clickedIndex;
	private bool hasChangedPage = false;
	private bool showConfirm = false;

	void Awake ()
	{
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
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			Debug.Log("HP is: "+PlayerInfo.hp);
			Debug.Log("MP is: "+PlayerInfo.mp);
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
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
	}

	void OnGUI()
	{
		if(!haveInitialized)
			InitStyles();

		if(showInventory)
		{
			Vector2 slotSize = new Vector2(400,25);
			Vector2 initPos = new Vector2(300,100);
			float vertShift = slotSize.y;
			int slotCount = 0;
			int index = 0;
			Consumable con;
			Equipment equip;

			// This only generates the inventory bounding box
			GUI.Box (new Rect(initPos.x, initPos.y, slotSize.x, slotSize.y*10.0f), "");

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

				GUI.Box(new Rect(initPos.x, initPos.y+(vertShift*i), slotSize.x, slotSize.y), slotStrings[i], slotStyles[i]);

				if (rectList.Count < slotCount)
				{
					rectList.Add(new Rect(initPos.x, initPos.y+(vertShift*i), slotSize.x, slotSize.y));
					Debug.Log("Adding item to rectlist");
				}
			}

			if(Input.GetMouseButtonDown(0))
			{
				for(int i=0; i<rectList.Count; i++)
				{
					Event e = Event.current;
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
			}

		}

		if (showConfirm)
		{
			if (GUI.Button(new Rect(0.5f*Screen.width-50.0f,0.5f*Screen.height, 80, 30), "YES!"))
			{
				Debug.Log("Using a Potion from the inventory");
				Debug.Log("Current number of potions: "+Inventory.invCount[itemIndex]);
				if(Inventory.invList[itemIndex] is Consumable)
				{
					Consumable con = (Consumable)Inventory.invList[itemIndex];
					Debug.Log("Using a "+con.itemName+", there were "+Inventory.invCount[itemIndex].ToString());
					PlayerInfo.HealHP(con.getHPField());
					PlayerInfo.HealMP(con.getMPField());
					Inventory.invCount[itemIndex]--;
					Debug.Log("...and now there are "+Inventory.invCount[itemIndex].ToString());
				}
				else if(Inventory.invList[itemIndex] is Equipment)
				{
					// Gotta equip it here
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
			if(GUI.Button(new Rect(20, 300, 80, 30), "Equipment"))
			{
				// Go to equip menu
			}

			if (GUI.Button(new Rect(20+xShift, 300, 80, 30), "Abilities"))
			{
				// Go to abilities menu
			}

			if (GUI.Button (new Rect(20+(2*xShift), 300, 80, 30), "Items"))
			{
				showInventory = true;
			}

			if (GUI.Button (new Rect(20+(3*xShift), 300, 80, 30), "Status"))
			{
				// Go to status menu
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
