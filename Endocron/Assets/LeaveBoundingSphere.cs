using UnityEngine;
using System.Collections;

public class StopMovement : MonoBehaviour {
	
	void OnTriggerExit(Collider collider) {
		collider.GetComponent<Player> ().LeavingBounds = true;
	}
}
