using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public int PlayerNumber;
	
	public float Movement
	{
		get
		{
			return Input.GetAxis (string.Format ("Forward[{0}]", this.PlayerNumber));
		}
	}

	public float Rotation
	{
		get
		{
			return Input.GetAxis (string.Format ("Rotation[{0}]", this.PlayerNumber));
		}
	}
}
