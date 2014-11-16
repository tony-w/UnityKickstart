using UnityEngine;
using System.Collections;

public class FollowPlayersSnow : MonoBehaviour {

   public GameObject Player;

	// Use this for initialization
	void Start () {
	  transform.position = Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	  transform.position = Player.transform.position;
	}
}
