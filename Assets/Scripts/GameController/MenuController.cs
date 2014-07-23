using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject MenuBox;

	void OnMouseDown()
	{
		GameObject equipment;
		GameObject abilities;
		GameObject inventory;
		GameObject status;
		Transform equipLabel;
		Transform abilitiesLabel;
		Transform inventoryLabel;
		Transform statusLabel;

		Vector3 relVector = new Vector3(0.16f, 0, 0);
		Vector3 basePosition = new Vector3(0.1f, 0.05f, 0);

		equipment = Instantiate(MenuBox, basePosition, transform.rotation) as GameObject;
		abilities = Instantiate(MenuBox, basePosition+relVector, transform.rotation) as GameObject;
		inventory = Instantiate(MenuBox, basePosition+(2*relVector), transform.rotation) as GameObject;
		status = Instantiate(MenuBox, basePosition+(3*relVector), transform.rotation) as GameObject;

		equipLabel = equipment.transform.FindChild("MenuLabel");
		abilitiesLabel = abilities.transform.Find("MenuLabel");
		inventoryLabel = inventory.transform.Find("MenuLabel");
		statusLabel = status.transform.Find ("MenuLabel");

		equipLabel.guiText.text = "Equipment";
		abilitiesLabel.guiText.text = "Abilities";
		inventoryLabel.guiText.text = "Inventory";
		statusLabel.guiText.text = "Status";
	}
}
