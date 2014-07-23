using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour 
{
	private Transform player;
	private Vector3 relCameraPos;
	private float relCameraPosMag;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update ()
	{
		transform.position = player.position - new Vector3(0.0f,0.0f,3.0f);
	}
}
