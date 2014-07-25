using UnityEngine;
using System.Collections;

public class FieldController : MonoBehaviour 
{
	public Texture seanTexture;
	private GameObject player;
	private GameObject HPText;
	private GameObject MPText;
	private MenuController menuController;
	private bool showButtons = false;

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

//		if (Input.GetKeyDown (KeyCode.H))
//		{
//			Debug.Log ("Halving HP and MP");
//			PlayerInfo.hp = 0.5f*PlayerInfo.hp;
//			PlayerInfo.mp = 0.5f*PlayerInfo.mp;
//		}
		
		HPText.guiText.text = PlayerInfo.hp.ToString();
		MPText.guiText.text = PlayerInfo.mp.ToString();

		// OK, so I'm going to leave MenuController alone, and have it ONLY for the field menu, just because I like your portrait thing, and I think it'd be a pain to texture a button and the text. We can figure out a way later
		if (menuController.GetOpenedState())
			showButtons = true;
		else
			showButtons = false;
	}

	void OnGUI()
	{
		if (showButtons)
		{
			float xShift = 100.0f;
			if(GUI.Button(new Rect(20, 300, 80, 30), "Equipment"))
			{
				// Go to equip menu
			}

			if (GUI.Button(new Rect(20+xShift, 300, 80, 30), "Abilties"))
			{
				// Go to abilities menu
			}

			if (GUI.Button (new Rect(20+(2*xShift), 300, 80, 30), "Items"))
			{
				// Go to items menu
			}

			if (GUI.Button (new Rect(20+(3*xShift), 300, 80, 30), "Status"))
			{
				// Go to status menu
			}
		}
	}
}
