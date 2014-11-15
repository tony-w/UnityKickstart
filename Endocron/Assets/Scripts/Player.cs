﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Jolly;

public class Player : MonoBehaviour
{
	public float MovementForce;
	public float RotationSpeed;
	public float MaxSpeed;
	public float JumpForce;

	public GameObject GroundContactDelta;

	private PlayerController PlayerController;

	public bool IsOnGround { get; private set; }

	public Color HUDColor;

	public int Score { get; private set; }

	public List<Collectable> Inventory;

	public Player ()
	{
		this.Score = 0;
		this.Inventory = new List<Collectable>();
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
		Vector3 dir = Vector3.zero;
		dir.y = this.PlayerController.Rotation;
/*
		if (dir != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.LookRotation(dir),
				Time.deltaTime * RotationSpeed
			);
		}
*/
		float translate = this.PlayerController.Movement;
		float rotation = this.PlayerController.Rotation;

		translate *= Time.deltaTime;
		transform.Translate (translate * Mathf.Sin (transform.rotation.y * Mathf.PI / 180.0f) * MaxSpeed,
		                     0,
		                     translate * Mathf.Cos (transform.rotation.y * Mathf.PI / 180.0f) * MaxSpeed);
		transform.Rotate (0, rotation * RotationSpeed, 0);

		/*
		float h = this.PlayerController.Movement;,
		float v = this.PlayerController.Rotation;

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
    	*/
	}

	public void OnCollected(Collectable collectable)
	{
		this.Score++;
		Inventory.Add (collectable);
	}
}
