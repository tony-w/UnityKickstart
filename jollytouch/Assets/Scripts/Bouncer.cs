using UnityEngine;
using System.Collections;
using Jolly;

public class Bouncer : MonoBehaviour
{
	public float DespawnHeight;

	void Start ()
	{
	
	}

	void Update ()
	{
		if (this.transform.position.y < this.DespawnHeight)
		{
			GameObject.Destroy (this.gameObject);
		}
	}
	
	void FixedUpdate ()
	{
		/*foreach (GameObject go in GameObject.FindGameObjectsWithTag("GravitySource"))
		{
			Vector3 delta = (go.transform.position - this.transform.position);
			this.rigidbody2D.AddForce(delta.normalized.xy () * this.rigidbody2D.mass * go.rigidbody2D.mass / delta.sqrMagnitude);
		}*/
	}

	public void MixColor (Draggable collidedWith)
	{
		this.Color = collidedWith.touchedColor;
	}

	public Color Color
	{
		get
		{
			SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
			return spriteRenderer.color;
		}
		set
		{
			SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
			spriteRenderer.color = value;
		}
	}
}
