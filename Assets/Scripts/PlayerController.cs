using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;


		void FixedUpdate()
		{
			transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
			transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;
		}
		

			
		
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Enemy")
		{
			Application.LoadLevel("Battle");
		}
	}

}
