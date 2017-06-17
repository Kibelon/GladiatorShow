using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public float speed = 10;
	public float gravity = 9.8f;
	public float damage = 10;
	public float timeStuck = 10;
	private Vector3 currentSpeed;
	public string damagableTag = "Destructible";
	//reserve space for frame functions
	private Vector3 nextPos;
	private RaycastHit hit;
	private bool done = false;

	void Start () {
		currentSpeed = this.transform.forward.normalized * speed;
	}
	
	void die(){
		Destroy (this.gameObject);
	}

	public void setSpeed (float newSpeed){
		speed = newSpeed;
		currentSpeed = this.transform.forward.normalized * newSpeed;
	}

	void Update () {
		if (!done) {
			nextPos = this.transform.position + currentSpeed * Time.deltaTime;
			if (Physics.Linecast (this.transform.position, nextPos, out hit)) {
				if (hit.collider.tag == damagableTag) {
					hit.collider.gameObject.SendMessage ("hurt", damage);
				}
				this.transform.SetParent (hit.transform);
				this.transform.position = hit.point; //stucks the arrow at a predictible depth
				done = true;
				Invoke ("die", timeStuck);
			} else {
				this.transform.position = nextPos;
				this.gameObject.transform.forward = currentSpeed;
				currentSpeed.y -= gravity * Time.deltaTime;
			}
		}
	}
}
