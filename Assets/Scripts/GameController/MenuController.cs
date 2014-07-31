using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{

	private bool hasBeenOpened = false;
	
	void OnMouseDown()
	{
		// Just reverse the state
		hasBeenOpened = !hasBeenOpened;
	}

	// Getter
	public bool GetOpenedState()
	{
		return hasBeenOpened;
	}
}
