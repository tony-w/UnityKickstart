using UnityEngine;
using System.Collections;
using Jolly;

public class Satellite : MonoBehaviour {

	public float GravityForce;

	void Start ()
	{
	
	}
	
	void FixedUpdate ()
	{
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("GravitySource"))
		{
			Vector3 delta = (go.transform.position - this.transform.position);
			this.rigidbody2D.AddForce(delta.normalized.xy () * this.rigidbody2D.mass * go.rigidbody2D.mass / delta.magnitude);
		}
	}
}
