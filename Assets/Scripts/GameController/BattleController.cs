using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour 
{
	public GameObject MenuBox;		// As the currently prefab'd menu boxes look pretty ugly, probably going to have to make specific ones for the battle screen
	private GameObject hpText;
	private GameObject mpText;
	private SceneChanger sceneChanger;
	private GameObject attack;
	private GameObject abilities;
	private GameObject inventory;
	private GameObject defend;
	private bool showButton = false;

	public Vector2 scrollPosition;
	public string battleText;
	
	// Item menu
	
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
	private int clickedIndex = -1;
	private bool showConfirm = false;
	
	void Awake ()
	{
		FieldInfo.PrintInfo();
		
		// Inventory code
		invRect = new Rect(invInitPos.x, invInitPos.y, invSlotSize.x, invSlotSize.y*10.0f);
	}
	// Use this for initialization
	void Start () 
	{
		hpText = GameObject.Find("HPText");
		mpText = GameObject.Find("MPText");
		hpText.guiText.text = PlayerInfo.hp.ToString();
		mpText.guiText.text = PlayerInfo.mp.ToString();
		sceneChanger = GetComponent<SceneChanger>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		hpText.guiText.text = PlayerInfo.hp.ToString();

		if (EnemyInfo.hp <= 0.0f)
		{
			GameState.SchmooSlain = true;
			FieldInfo.shouldDestroy = true;
			sceneChanger.ChangeScene("Field");
		}

		// B for button, I guess
		if(Input.GetKeyDown(KeyCode.B))
			showButton = true;

		// ...and V because it's next to B.
		if(Input.GetKeyDown(KeyCode.V))
			showButton = false;

		if(Input.GetKeyDown(KeyCode.P))
		{
			Inventory.AddItem(ItemList.potion);
		}

		if(Input.GetKeyDown(KeyCode.H))
		{
			PlayerInfo.hp -= 20.0f;
		}

	}

	// OnGUI seems to work like Update in that the game is constantly running through it
	// This also seems to be the only place that you can define what the buttons do. You can apparently change what the buttons look like elsewhere, but nothing else
	void OnGUI()
	{
		if(showButton==true)
		{
			float yShift = 40.0f;

			if(GUI.Button(new Rect(50, 50, 70, 40), "Attack"))
			{
				battleText += "You hit the "+EnemyInfo.label+" for "+PlayerInfo.strDamage+" damage!\n";
				EnemyInfo.hp -= PlayerInfo.strDamage;
				battleText += "The "+EnemyInfo.label+" has "+EnemyInfo.hp+" HP remaining!\n";
				showButton = false;
				scrollPosition.y = Mathf.Infinity;
			}

			GUI.Button(new Rect(50, 50+yShift, 70, 40), "Abilities");

			if(GUI.Button(new Rect(50, 50+(2*yShift), 70, 40), "Items"))
			{
				showInventory = true;
				//int index = Inventory.invList.IndexOf(ItemList.potion);
				//if(index < 0)
				//{
					// Potions haven't been added to the inventory yet
				//	Debug.Log("Can't use, no potion exists");
				//}
				//else
				//{
				//	if(Inventory.invCount[index] < 1)
				//	{
				//		Debug.Log("Can't use, no more potions left");
				//	}
				//	else
				//	{
				//		Debug.Log("Using a Potion from the inventory");
				//		Debug.Log("Current number of potions: "+Inventory.invCount[index]);
				//		PlayerInfo.UseConsumable(ItemList.potion);
				//		Inventory.invCount[index]--;
				//		Debug.Log("Number of potions after use: "+Inventory.invCount[index]);
				//	}

				//}
			}


			GUI.Button(new Rect(50, 50+(3*yShift), 70, 40), "Defend");
		}

		GUILayout.BeginArea(new Rect(0, 500, 250, 250));
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(200), GUILayout.Height(200));
		GUILayout.Label(battleText);
		GUILayout.EndScrollView();
		GUILayout.EndArea();
		
		// INVENTORY CODE
		
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
					if(clickedIndex >= 0)
						slotStyles[clickedIndex] = defaultStyle;
				}
				
			}
			
		}
		
		if (showConfirm)
		{
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
						Debug.Log("Unequipping a "+PlayerInfo.equipment[item.slot].itemName);
						PlayerInfo.UnequipItem(item.slot);
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
		
		// INVENTORY CODE ENDS HERE
		
		
	}
	
	// Inventory menu texture and styles, copied here
	
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
