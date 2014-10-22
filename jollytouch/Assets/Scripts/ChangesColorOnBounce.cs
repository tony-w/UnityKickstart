using UnityEngine;
using System.Collections;

public class ChangesColorOnBounce : MonoBehaviour
{
	void Start ()
	{
	
	}
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		Bouncer bouncer = collision.gameObject.GetComponent<Bouncer>();
		if (bouncer)
		{
			bouncer.MixColor (this.GetComponent<Draggable>());
		}
	}
}
