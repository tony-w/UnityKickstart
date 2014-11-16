using UnityEngine;
using System.Collections;
using Jolly;

public class FollowPlayers : MonoBehaviour
{
	public GameObject Player;

	public const float OffsetFactor = 1.0f;

	private Vector3 CameraOffset;
	private Vector3 TargetCameraPosition;

	void Start ()
	{
		this.CameraOffset = this.camera.transform.position;
		this.CameraOffset.y = 0.0f;
		this.TargetCameraPosition = Player.transform.position;
	}

	void OnPreCull ()
	{
		this.camera.transform.position = this.TargetCameraPosition;
		this.camera.transform.rotation = Player.transform.rotation;
	}

	void OnPreRender ()
	{
	}

	void Update ()
	{
		this.CameraOffset.x = Mathf.Sin (Player.transform.rotation.y * Mathf.PI / 180.0f) * OffsetFactor;
		this.CameraOffset.z = Mathf.Cos (Player.transform.rotation.z * Mathf.PI / 180.0f) * OffsetFactor;
		this.TargetCameraPosition = (Player.transform.position + this.CameraOffset);
	}
}
