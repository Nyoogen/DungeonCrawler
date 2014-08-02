using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	private float initialSpeed;
	private bool finishedLoading;
	private SceneChanger sceneChanger;
	private GameObject gameController;

	void Awake()
	{
		initialSpeed = speed;
		speed = 0.0f;
		finishedLoading = false;
		gameController = GameObject.FindGameObjectWithTag("GameController");
		sceneChanger = gameController.GetComponent<SceneChanger>();
	}


	void Update()
	{
		if (gameController.guiTexture.color.a <= 0.05f)
		{
			// The only reason the finishedLoading bool exists is to make sure that this code doesn't reset the speed after
			// we encounter something
			if (!finishedLoading)
			{
				speed = initialSpeed;
				finishedLoading = true;
			}
		}
	}


	void FixedUpdate()
	{
		transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;
	}


	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Schmoo")
		{
			speed = 0.0f;
			FieldInfo.lastPlayerPosition = transform.position;
			FieldInfo.lastEncounteredObjectTag = other.gameObject.tag;	// Should probably be storing the object name or something here, because if we have multiple "Enemy" tagged objects, this might end up destroying all of them
			EnemyInfo.SetEnemyInfo(EnemyList.schmoo);
			sceneChanger.ChangeScene("Battle");
		}
		if (other.tag == "Exit")
		{
			speed = 0.0f;
			Vector3 exitCenter = new Vector3(other.transform.position.x, other.transform.position.y, 0);
			Vector3 playerCenter = new Vector3(transform.position.x, transform.position.y, 0);
			Vector3 directionVector = playerCenter-exitCenter;
			FieldInfo.lastPlayerPosition = transform.position+(directionVector.normalized*0.1f);
			sceneChanger.ChangeScene("Town");
		}
	}

}
