using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Jolly;

public class Player : MonoBehaviour
{
	public float MovementForce;
	public float RotationSpeed;
	public float MaxSpeed;
	public float JumpForce;

	public float SpawnRadius;
	public float SpawnArea;

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
		Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
		while (location.y * SpawnArea < 0)
			location = Random.insideUnitCircle * this.SpawnRadius;
		Vector3 worldPosition = new Vector3 (location.x, 5, location.y);

		this.transform.position = worldPosition;

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
		float translate = this.PlayerController.Movement;
		float rotation = this.PlayerController.Rotation;

		translate *= Time.deltaTime;
		rotation *= Time.deltaTime;
		if (translate != 0.0f)
			transform.Translate (translate * Mathf.Sin (transform.rotation.y * Mathf.PI / 180.0f) * MaxSpeed,
		                     	0,
		                     	translate * Mathf.Cos (transform.rotation.y * Mathf.PI / 180.0f) * MaxSpeed);
		if (rotation != 0.0f)
			transform.Rotate (0, rotation * RotationSpeed, 0);
	}

	public void OnCollected(Collectable collectable)
	{
		this.Score++;
		Inventory.Add (collectable);
	}
}
