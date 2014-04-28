using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	float speed = 5.0f;
	float radius = 1.0f;
	private Vector3 velocity = Vector3.zero;
	public GameController controller;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void FixedUpdate() {

		if(controller.levelFinished || controller.currentState == 0)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			return;
		}
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
		if(direction.magnitude > 1.0) direction.Normalize();

		if(direction.magnitude > 0){

			// lets set the direction according to the camera now.
			direction = Camera.main.transform.TransformDirection(direction) * speed * 2;
			// lets take the downward velocity from the current so that we dont get wierd physics results
			direction.y = rigidbody.velocity.y;
			
			// Now, lets keep track of a velocity.
			// This will let the ball move while we are not pressing anything.
			rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, direction, 3.0f * Time.deltaTime);
			// Now, lets break the rotation out from the movement.
			Vector3 rotation = new Vector3(rigidbody.velocity.z,0,-rigidbody.velocity.x) * 20;
			
			
			// Lets add some spin to make the ball move better
			rigidbody.angularVelocity = Vector3.Lerp(rigidbody.angularVelocity, rotation, 3.0f * Time.deltaTime);
		}
	}
}
