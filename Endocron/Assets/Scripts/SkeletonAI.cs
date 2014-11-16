using UnityEngine;
using System.Collections;

public class SkeletonAI : MonoBehaviour {
	public float MovementForce;
	public float MaxSpeed;
	public float DistToChase;
	public float DistToAttack;
	public GameObject RedPlayer;
	public GameObject BluePlayer;

	private GameObject CurrentPlayerToAttack;

	private float TimeSinceAttacking = 0.0f;

	
	// Use this for initialization
	void Start () {
		this.RedPlayer = GameObject.Find("Red Player");
		this.BluePlayer = GameObject.Find("Blue Player");
		CurrentPlayerToAttack = null;
	}
	
	// Update is called once per frame
	void Update() {
		
	}
	
	void FixedUpdate () {
		Vector3 thisPosition = this.transform.position;
		Vector3 redPlayerPosition = RedPlayer.transform.position;
		Vector3 bluePlayerPosition = BluePlayer.transform.position;
		thisPosition.y = redPlayerPosition.y = bluePlayerPosition.y = 0;
		Vector3 vecToRedPlayer = redPlayerPosition - thisPosition;
		float distToRedPlayer = vecToRedPlayer.magnitude;
		Vector3 vecToBluePlayer = bluePlayerPosition - thisPosition;
		float distToBluePlayer = vecToBluePlayer.magnitude;

		if (distToRedPlayer <= this.DistToChase && distToRedPlayer < distToBluePlayer) {
			CurrentPlayerToAttack = this.RedPlayer;

			this.transform.LookAt(redPlayerPosition);
			if (distToRedPlayer > DistToAttack) {
				// Chase the red player down!
				this.animation.Play("run", PlayMode.StopAll);
				this.rigidbody.AddForce (vecToRedPlayer.normalized * MovementForce);
			} else {
				// ATTACK!!
				this.animation.Play("attack", PlayMode.StopAll);
				TimeSinceAttacking += Time.deltaTime;
			}
		} else if (distToBluePlayer <= this.DistToChase){
			CurrentPlayerToAttack = this.BluePlayer;

			this.transform.LookAt(bluePlayerPosition);
			if (distToBluePlayer > DistToAttack) {
				// Chase the blue player down!
				this.animation.Play("run", PlayMode.StopAll);
				this.rigidbody.AddForce (vecToBluePlayer.normalized * MovementForce);
			} else {
				// ATTACK!!
				this.animation.Play("attack", PlayMode.StopAll);
				TimeSinceAttacking += Time.deltaTime;
			}
		} else {
			// Neither player is very close; just wait around for one to show up.
			this.rigidbody.velocity = Vector3.zero;
			this.animation.Play("waitingforbattle", PlayMode.StopAll);
		}
		if (this.rigidbody.velocity.magnitude > MaxSpeed)
		{
			// Slow this monster down!
			this.rigidbody.velocity = this.rigidbody.velocity.normalized * MaxSpeed;
		}

		if (TimeSinceAttacking >= 1.0f) {
			CurrentPlayerToAttack.GetComponent<Player>().Kill();
			CurrentPlayerToAttack = null;
			TimeSinceAttacking = 0.0f;
		}
	}
}