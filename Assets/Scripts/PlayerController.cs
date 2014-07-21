using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;


		void FixedUpdate()
		{
			transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
			transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;
		}
		

			
		
	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Enemy")
		{
			Application.LoadLevel("Battle");
}
	}

}
