using UnityEngine;
using System.Collections;

public class WordWrapper : MonoBehaviour 
{
	public static string FormatGuiTextArea(GUIText guiText, float maxAreaWidth)
	{
		string[] words = guiText.text.Split(' ');
		string result = "";
		Rect textArea = new Rect();
		
		for(int i = 0; i < words.Length; i++)
		{
			// set the gui text to the current string including new word
			guiText.text = (result + words[i] + " ");
			// measure it
			textArea = guiText.GetScreenRect();
			// if it didn't fit, put word onto next line, otherwise keep it
			if(textArea.width > maxAreaWidth)
			{
				result += ("\n" + words[i] + " ");
			}
			else
			{
				result = guiText.text;
			}
		}
		return result;
	}
}
