using UnityEngine;
using System.Collections;
using Jolly;

public class FollowPlayers : MonoBehaviour
{
	public GameObject Player;

	void Start ()
	{
		float height = Player.transform.localScale.y * 1.8f;
		this.camera.transform.position = Player.transform.position + new Vector3(0.0f, height, 0.0f);
		this.camera.transform.rotation = Player.transform.rotation;
	}

	void OnPreCull ()
	{
		float height = Player.transform.localScale.y * 1.8f;
		this.camera.transform.position = Player.transform.position + new Vector3(0.0f, height, 0.0f);
		this.camera.transform.rotation = Player.transform.rotation;
	}
}
