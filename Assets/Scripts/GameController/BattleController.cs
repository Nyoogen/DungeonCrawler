using UnityEngine;
using System.Collections;

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
	
	void Awake ()
	{
		FieldInfo.PrintInfo();
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

//		// Just for testing battle end
//		if (Input.GetKeyDown(KeyCode.E))
//		{
//			FieldInfo.shouldDestroy = true;
//			sceneChanger.ChangeScene("Field");
//
//		}

		// B for button, I guess
		if(Input.GetKeyDown(KeyCode.B))
			showButton = true;

		// ...and V because it's next to B.
		if(Input.GetKeyDown(KeyCode.V))
			showButton = false;

		if(Input.GetKeyDown(KeyCode.P))
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
				EnemyInfo.hp -= 20;
				Debug.Log ("Enemy health is: "+EnemyInfo.hp);
				showButton = false;
			}

			GUI.Button(new Rect(50, 50+yShift, 70, 40), "Abilities");

			if(GUI.Button(new Rect(50, 50+(2*yShift), 70, 40), "Items"))
			{
				int index = Inventory.invList.IndexOf(ItemList.potion);
				if(index < 0)
				{
					// Potions haven't been added to the inventory yet
					Debug.Log("Can't use, no potion exists");
				}
				else
				{
					if(Inventory.invCount[index] < 1)
					{
						Debug.Log("Can't use, no more potions left");
					}
					else
					{
						Debug.Log("Using a Potion from the inventory");
						Debug.Log("Current number of potions: "+Inventory.invCount[index]);
						PlayerInfo.Heal(ItemList.potion.getHPField());
						Inventory.invCount[index]--;
						Debug.Log("Number of potions after use: "+Inventory.invCount[index]);
					}

				}
			}


			GUI.Button(new Rect(50, 50+(3*yShift), 70, 40), "Defend");
		}
	}
}
