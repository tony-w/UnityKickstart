﻿using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
	public float RotationSpeed;
	public AudioClip CollectedSound;
	public string Name;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		Vector3 eulerRotation = this.transform.localRotation.eulerAngles;
		this.transform.rotation = Quaternion.Euler(eulerRotation.x, Time.time * this.RotationSpeed, eulerRotation.z);
	}

	void OnTriggerEnter(Collider other)
	{
		Player player = other.gameObject.GetComponent<Player>();
		if (null == player)
		{
			return;
		}

		if (player.OnCollected (this.Name)) {
			AudioSource.PlayClipAtPoint (this.CollectedSound, this.transform.position);
			DestroyObject (this.gameObject);
		}
	}
}
