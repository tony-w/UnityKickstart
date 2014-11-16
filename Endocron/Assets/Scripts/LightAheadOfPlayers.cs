using UnityEngine;
using System.Collections;

public class LightAheadOfPlayers : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {
		this.transform.position = Player.transform.position;
		this.transform.rotation = Player.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Player.transform.position;
		this.transform.rotation = Player.transform.rotation;
	}
}
