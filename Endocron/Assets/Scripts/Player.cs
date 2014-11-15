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

	public bool IsOnGround { get; private set; }

	public Color HUDColor;

	public int Score { get; private set; }

	public Player ()
	{
		this.Score = 0;
	}

	void Start ()
	{
		this.PlayerController = this.GetComponent<PlayerController>();

		JollyDebug.Watch (this, "IsOnGround", delegate ()
		{
			return this.IsOnGround;
		});
	}

	void Update ()
	{
		Ray ray = new Ray(this.transform.position, Vector3.down);
		float maximumDistance = -this.GroundContactDelta.transform.localPosition.y;
		this.IsOnGround = Physics.Raycast(ray, maximumDistance);
	}

	void FixedUpdate ()
	{
		float move = this.PlayerController.Movement;
		float rot = this.PlayerController.Rotation;

		this.rigidbody.AddForce (new Vector3(move * this.MovementForce * Mathf.Cos (rot * Mathf.PI / 180.0f),
		                                     0.0f,
		                                     move * this.MovementForce * Mathf.Sin (rot * Mathf.PI / 180.0f)));

				
	}

	public void OnCollected(Collectable collectable)
	{
		this.Score++;
	}
}
