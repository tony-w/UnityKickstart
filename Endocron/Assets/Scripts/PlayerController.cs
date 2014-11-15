﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public int PlayerNumber;
	
	public float Movement
	{
		get
		{
			return Input.GetAxis (string.Format ("Horizontal[{0}]", this.PlayerNumber));
		}
	}

	public float Rotation
	{
		get
		{
			return Input.GetAxis (string.Format ("Vertical[{0}]", this.PlayerNumber));
		}
	}
}
