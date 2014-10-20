using UnityEngine;
using System.Collections;
using Jolly;

public class Player : MonoBehaviour
{
	public float MovementForce;
	public float MaxSpeed;
	public float JumpForce;

	public GameObject GroundContactDelta;

	private bool CanJump;

	void Start ()
	{
		JollyDebug.Watch (this, "CanJump", delegate () {
			return this.CanJump;
		});
	}
	
	void Update ()
	{
		Ray ray = new Ray(this.transform.position, Vector3.down);
		float maximumDistance = -this.GroundContactDelta.transform.localPosition.y;
		this.CanJump = Physics.Raycast(ray, maximumDistance);
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis (string.Format ("Horizontal"));
		float v = Input.GetAxis (string.Format ("Vertical"));

		this.rigidbody.AddForce (new Vector3(h * this.MovementForce, 0.0f, v * this.MovementForce));

		float maxSpeedX = Mathf.Abs (this.MaxSpeed * h);
		if (Mathf.Abs(this.rigidbody.velocity.x) > maxSpeedX)
		{
			this.rigidbody.velocity = new Vector3(Mathf.Sign (this.rigidbody.velocity.x) * maxSpeedX, this.rigidbody.velocity.y, this.rigidbody.velocity.z);
		}	

		float maxSpeedZ = Mathf.Abs (this.MaxSpeed * v);
		if (Mathf.Abs(this.rigidbody.velocity.z) > maxSpeedZ)
		{
			this.rigidbody.velocity = new Vector3(this.rigidbody.velocity.x, this.rigidbody.velocity.y, Mathf.Sign (this.rigidbody.velocity.z) * maxSpeedZ);
    	}

		bool jump = Input.GetButtonDown ("Jump");
		if (jump && this.CanJump)
		{
			this.rigidbody.AddForce(new Vector3(0.0f, this.JumpForce, 0.0f));
			this.CanJump = false;
		}
	}
}
