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
	private string[] Inventory;
	private Animator animator;

	void Start ()
	{
		this.Inventory = new string[InventorySize];
		for (int i = 0; i < InventorySize; i++) this.Inventory [i] = null;

		Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
		while (location.y * SpawnArea < 0)
			location = Random.insideUnitCircle * this.SpawnRadius;
		Vector3 worldPosition = new Vector3 (location.x, 5, location.y);

		this.transform.position = worldPosition;

		freezePosition = this.transform.position;
		freezeRotation = this.transform.rotation;

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
	public bool OnCollected(string item)
	{
		return AddItemToInventory (item);
	}

	public bool IsHoldingItem(string itemName) {
		for (int i = 0; i < this.Inventory.Length; i++) {
			if (null != this.Inventory [i] && this.Inventory [i].Equals(itemName)) {
				return true;
			}
		}
		return false;
	}

	/**
	 * Return true if there was room in the player's inventory for the object.
	 */
	private bool AddItemToInventory(string item) {
		if (item.Equals ("Endocron") && this.IsHoldingItem ("Endocron")) {
			return false;
		}
		for (int i = 0; i < this.Inventory.Length; i++) {
			if (null == this.Inventory [i]) {
				this.Inventory [i] = item;
				if (item.Equals("Hoverboard")) {
					this.MaxSpeed = 30;
					this.RotationSpeed = 60;
				}
				return true;
			}
		}
		return false;
	}

	private void PrintInventory() {
		Debug.Log("Inventory items:");
		for (int i = 0; i < this.Inventory.Length; i++) {
			Debug.Log(null == this.Inventory [i] ? "<Empty>" : Inventory[i]);
		}
	}

	public void Kill() {
		// Respawn the Player's half of the Endocron, if they have it

		//	Respawn the Player at a new location
		Vector2 location = Random.insideUnitCircle * this.SpawnRadius;
		while (location.y * SpawnArea < 0)
			location = Random.insideUnitCircle * this.SpawnRadius;
		Vector3 worldPosition = new Vector3 (location.x, 5, location.y);

		this.transform.position = worldPosition;
	}
}
