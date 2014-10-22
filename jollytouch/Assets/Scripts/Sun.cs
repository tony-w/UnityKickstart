using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{

	void Start ()
	{
	}

	void Update ()
	{
		this.transform.position = Vector3.zero;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		GameObject.Destroy(collision.gameObject);
	}
}
