/********************************************
 * Maded by Jesús Gracia Güell 18/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 10;
	public float damage = 10;
	public float timeStuck = 0.1f;
	public float ttl = 10;
	private Vector3 currentSpeed;
	//reserve space for frame functions
	private Vector3 nextPos;
	private RaycastHit hit;
	private bool done = false;

	void Start () {
		currentSpeed = this.transform.forward.normalized * speed;
		Invoke ("die", ttl);
	}

	void die(){
		Destroy (this.gameObject);
	}

	void Update () {
		if (!done) {
			nextPos = this.transform.position + currentSpeed * Time.deltaTime;
			if (Physics.Linecast (this.transform.position, nextPos, out hit)) {
				if (hit.collider.gameObject.GetComponent<Damagable> () != null){
					hit.collider.gameObject.GetComponent<Damagable> ().hurt (damage, DamageType.pirsing);
				}
				this.transform.position = hit.point; //stucks the arrow at a predictible depth
				done = true;
				Invoke ("die", timeStuck);
			} else {
				this.transform.position = nextPos;
			}
		}
	}
}
