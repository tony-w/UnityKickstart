using UnityEngine;
using System.Collections;

public class SkeletonAI : MonoBehaviour {
	public float MovementForce;
	public float MaxSpeed;
	public float DistToChase;
	public float DistToAttack;
	public GameObject RedPlayer;
	public GameObject BluePlayer;

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}
	
	void FixedUpdate () {
		Vector3 vecToRedPlayer = RedPlayer.transform.position - this.transform.position;
		float distToRedPlayer = vecToRedPlayer.magnitude;
		Vector3 vecToBluePlayer = BluePlayer.transform.position - this.transform.position;
		float distToBluePlayer = vecToBluePlayer.magnitude;

		if (distToRedPlayer <= this.DistToChase) {
			if (distToRedPlayer > DistToAttack) {
				// Chase the red player down!
				this.animation.Play("run", PlayMode.StopAll);
				this.rigidbody.AddForce (vecToRedPlayer.normalized * MovementForce);
			} else {
				// ATTACK!!
				this.animation.Play("attack", PlayMode.StopAll);
			}
		} else if (distToBluePlayer <= this.DistToChase){
			if (distToBluePlayer > DistToAttack) {
				// Chase the blue player down!
				this.animation.Play("run", PlayMode.StopAll);
				this.rigidbody.AddForce (vecToBluePlayer.normalized * MovementForce);
			} else {
				// ATTACK!!
				this.animation.Play("attack", PlayMode.StopAll);
			}
		} else {
			// Neither player is very close; just wait around for one to show up.
			this.animation.Play("waitingforbattle", PlayMode.StopAll);
		}
		if (this.rigidbody.velocity.magnitude > MaxSpeed)
		{
			// Slow this monster down!
			this.rigidbody.velocity = this.rigidbody.velocity.normalized * MaxSpeed;
		}
	}
}