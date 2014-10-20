using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float MovementForce;
	public float MaxSpeed;
	public float JumpForce;

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
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
		if (jump)
		{
			this.rigidbody.AddForce(new Vector3(0.0f, this.JumpForce, 0.0f));
		}
	}
}
