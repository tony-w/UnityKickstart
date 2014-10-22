using UnityEngine;
using System.Collections;
using Jolly;

public class Draggable : MonoBehaviour
{
	Vector2 lastTouch;

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}

	void OnTouchDown (Vector2 position)
	{
		Debug.Log ("OnTouchDown");
		this.lastTouch = position;
	}

	void OnTouch (Vector2 position)
	{
		Debug.Log ("OnTouch");
		this.transform.position += (position - this.lastTouch).xyz(0.0f);
		this.lastTouch = position;
	}
}
