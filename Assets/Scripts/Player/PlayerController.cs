using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	private float initialSpeed;
	private SceneChanger sceneChanger;
	private GameObject gameController;

	void Awake()
	{
		initialSpeed = speed;
		speed = 0.0f;
		gameController = GameObject.FindGameObjectWithTag("GameController");
		sceneChanger = gameController.GetComponent<SceneChanger>();
	}

	void Update()
	{
		// This isn't entirely right yet because we haven't corner-cased for enemy encounters (i.e. speed=0 & screen fading)
		if (gameController.guiTexture.color.a <= 0.05f)
		{
			speed = initialSpeed;
		}
	}
	void FixedUpdate()
	{
		transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;
	}


	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Enemy")
		{
			speed = 0.0f;
			FieldInfo.lastPlayerPosition = transform.position;
			FieldInfo.lastEncounteredObjectTag = other.gameObject.tag;
			sceneChanger.ChangeScene("Battle");
		}
		if (other.tag == "Exit")
		{
			speed = 0.0f;
			FieldInfo.lastPlayerPosition = transform.position;
			sceneChanger.ChangeScene("Town");
		}
	}

}
