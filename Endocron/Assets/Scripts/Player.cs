using UnityEngine;
using System.Collections;
using Jolly;

public class Player : MonoBehaviour
{
	public float MovementForce;
	public float RotationSpeed;
	public float MaxSpeed;
	public int InventorySize = 4;

	private Vector3 freezePosition = Vector3.zero;
	private Quaternion freezeRotation = Quaternion.identity;

	public float SpawnRadius;
	public float SpawnArea;

	public GameObject GroundContactDelta;
	private PlayerController PlayerController;
	public bool IsOnGround { get; private set; }
	public Color HUDColor;
	private Collectable[] Inventory;
	private int NumItemsHeld;

	private Animator animator;
	public Player ()
	{
		this.Inventory = new Collectable[InventorySize];
		NumItemsHeld = 0;
	}

	void Start ()
	{
		freezePosition = this.transform.position;
		freezeRotation = this.transform.rotation;
		Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
		while (location.y * SpawnArea < 0)
			location = Random.insideUnitCircle * this.SpawnRadius;
		Vector3 worldPosition = new Vector3 (location.x, 5, location.y);

		this.transform.position = worldPosition;

		this.PlayerController = this.GetComponent<PlayerController>();

		this.animator = gameObject.GetComponent<Animator> ();

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
		if (PlayerController.PlayerNumber == 0 && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
		    || PlayerController.PlayerNumber == 1 && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))) {
			transform.Translate (translate * Mathf.Sin (transform.rotation.y * Mathf.PI / 180.0f) * MaxSpeed,
		                     	0,
		                     	translate * Mathf.Cos (transform.rotation.y * Mathf.PI / 180.0f) * MaxSpeed);
			freezePosition = this.transform.position;
			animator.Play("walking");
		} else {
			this.transform.position = freezePosition;
		}
		if (PlayerController.PlayerNumber == 0 && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
		    || PlayerController.PlayerNumber == 1 && Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate (0, rotation * RotationSpeed, 0);
			freezeRotation = this.transform.rotation;
			animator.Play(rotation < 0 ? "left_turn" : "right_turn");
		} else {
			this.transform.rotation = freezeRotation;
		}

		if (translate == 0.0f && rotation == 0.0f) {
			animator.Play("idle");
		}
	}

	/**
	 * Return true if there was room in the player's inventory for the object.
	 */
	public bool OnCollected(Collectable collectable)
	{
		if (this.NumItemsHeld < this.Inventory.Length) {
			this.Inventory[this.NumItemsHeld++] = collectable;
			return true;
		}
		return false;
	}

	private void PrintInventory() {
		Debug.Log("Inventory items:");
		for (int i = 0; i < NumItemsHeld; i++) {
			Debug.Log(Inventory[i].Name);
		}
	}
}
