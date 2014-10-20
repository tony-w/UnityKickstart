using UnityEngine;
using System.Collections;
using Jolly;

public class Player : MonoBehaviour
{
	public float MovementForce;
	public float MaxSpeed;
	public float JumpForce;

	public GameObject GroundContactDelta;

	private PlayerController PlayerController;

	private bool CanJump;

	void Start ()
	{
		this.PlayerController = this.GetComponent<PlayerController>();

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
		float h = this.PlayerController.HorizontalMovementAxis;
		float v = this.PlayerController.VericalMovementAxis;

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

		bool jump = this.PlayerController.Jump;
		if (jump && this.CanJump)
		{
			this.rigidbody.AddForce(new Vector3(0.0f, this.JumpForce, 0.0f));
			this.CanJump = false;
		}
	}
}
