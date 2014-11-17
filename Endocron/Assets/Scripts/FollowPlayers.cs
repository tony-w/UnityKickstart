using UnityEngine;
using System.Collections;
using Jolly;

public class FollowPlayers : MonoBehaviour
{
	public GameObject Player;

	void Start ()
	{
		float height = Player.transform.localScale.y * 1.8f;
		Quaternion direction = Player.transform.rotation;
		Vector3 offsetDirection = new Vector3 (Mathf. Sin (direction.y * Mathf.PI / 180.0f), 0, Mathf.Cos (direction.y * Mathf.PI / 180.0f));
		this.camera.transform.position = Player.transform.position + new Vector3 (0.0f, height, 0.0f);
//		this.camera.transform.Translate (new Vector3(),);
		this.camera.transform.rotation = Player.transform.rotation;
	}

	void OnPreCull ()
	{
		float height = Player.transform.localScale.y * 1.8f;
		Quaternion direction = Player.transform.rotation;
		Vector3 offsetDirection = new Vector3 (Mathf. Sin (direction.y * Mathf.PI / 180.0f), 0, Mathf.Cos (direction.y * Mathf.PI / 180.0f));
		this.camera.transform.position = Player.transform.position + new Vector3(0.0f, height, 0.0f) + offsetDirection * 2.0f;
		this.camera.transform.rotation = Player.transform.rotation;
	}
}
