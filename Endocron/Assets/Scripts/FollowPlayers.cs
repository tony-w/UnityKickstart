using UnityEngine;
using System.Collections;
using Jolly;

public class FollowPlayers : MonoBehaviour
{
	public GameObject Player;

	public const float OffsetFactor = 1.0f;

	private Vector3 CameraOffset;
	private Vector3 TargetCameraPosition;
	private Quaternion TargetCameraRotation;

	void Start ()
	{
		this.CameraOffset = this.camera.transform.position;
		this.CameraOffset.y = 0.0f;
		this.TargetCameraPosition = Player.transform.position;
		this.TargetCameraRotation = Player.transform.rotation;
	}

	void OnPreRender ()
	{
		this.camera.transform.position = this.TargetCameraPosition;
		this.camera.transform.rotation = this.TargetCameraRotation;
	}

	void Update ()
	{
		this.CameraOffset.x = Mathf.Sin (Player.transform.rotation.y * Mathf.PI / 180.0f) * OffsetFactor;
		this.CameraOffset.z = Mathf.Cos (Player.transform.rotation.z * Mathf.PI / 180.0f) * OffsetFactor;
		this.TargetCameraPosition = (Player.transform.position + this.CameraOffset);
		this.TargetCameraRotation.y = Player.transform.rotation.y;
	}
}
