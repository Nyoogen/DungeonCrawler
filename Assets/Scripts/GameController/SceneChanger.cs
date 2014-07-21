using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour 
{
	public float fadeSpeed = 1.5f;
	
	private bool sceneStarting = true;
	private bool sceneEnding = false;

	private string sceneString;
	
	void Awake ()
	{
		guiTexture.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
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
	}
	
	void FadeToBlack ()
	{
		guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
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
