using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour
{
	public float RotationSpeed;
	public AudioClip CollectedSound;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		Vector3 eulerRotation = this.transform.localRotation.eulerAngles;
		this.transform.rotation = Quaternion.Euler(eulerRotation.x, Time.time * this.RotationSpeed, eulerRotation.z);
	}

	void OnEnterTrigger(Collider other)
	{
		Debug.Log ("OnEnterTrigger");
		Player player = collider.gameObject.GetComponent<Player>();
		if (null == player)
		{
			return;
		}

		player.OnCollected(this);

		AudioSource.PlayClipAtPoint(this.CollectedSound, this.transform.position);

		DestroyObject (this.gameObject);
	}
}
