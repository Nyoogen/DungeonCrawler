using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour 
{
	public float fadeSpeed = 1.5f;
//	public Color guiTextColor = Color.red;
	
	private bool sceneStarting = true;
	private bool sceneEnding = false;

	private string sceneString;
//	private GameObject fieldStatus;	// Only for Field scene
	
	void Awake ()
	{
//		Debug.Log (Application.loadedLevelName);
		guiTexture.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);

//		// We want the GUIText in FieldStatus to be fading in from black to red, so we need to set the initial color to black
//		if (Application.loadedLevelName == "Field")
//		{
//			fieldStatus = GameObject.Find("FieldStatus");
//			foreach(Transform child in fieldStatus.transform)
//			{
//				child.guiText.color = Color.black;
//			}
//		}
	}
	
	void Update ()
	{
		if (sceneStarting)
		{
			StartScene ();
		}

		if (sceneEnding)
		{
			EndScene();
		}
	}
	
	void FadeToClear ()
	{
		guiTexture.color = Color.Lerp (guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);

//		// Because GUIText is a GUI element, the black texture doesn't blanket over them when transitioning, so I'm lerping these colors too
//		// Here, I'm identifying all of the children of FieldStatus (NameText, HPText, and MPText) and looping through them. The effect is that I'm lerping all three GUIText elements
//		if (Application.loadedLevelName == "Field")
//		{
//			foreach(Transform child in fieldStatus.transform)
//			{
//				child.guiText.color = Color.Lerp (child.guiText.color, guiTextColor, fadeSpeed * Time.deltaTime);
//			}
//		}
	}
	
	void FadeToBlack ()
	{
		guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);

//		// See FadeToClear's comment
//		if (Application.loadedLevelName == "Field")
//		{
//			foreach(Transform child in fieldStatus.transform)
//			{
//				child.guiText.color = Color.Lerp (child.guiText.color, Color.black, fadeSpeed * Time.deltaTime);
//			}
//		}
	}
	
	public void StartScene ()
	{
		FadeToClear ();
		
		if (guiTexture.color.a <= 0.05f)
		{
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			sceneStarting = false;
		}
	}
	
	void EndScene ()
	{
		guiTexture.enabled = true;
		FadeToBlack ();

		if (guiTexture.color.a >= 0.90f)
		{
			Application.LoadLevel(sceneString);
		}
	}

	public void ChangeScene (string scene)
	{
		sceneEnding = true;
		sceneString = scene;
	}

}
