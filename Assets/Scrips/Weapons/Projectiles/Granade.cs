/********************************************
 * Maded by Jesús Gracia Güell 23/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float speed = 10;
	public float gravity = 9.8f;
	public float ttl = 50;
	public float bounceFactor = 0.5f;
	public bool stick = false;
	public bool explodeOnTouch = false;
	public GameObject explosion;
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	private Vector3 currentSpeed;
	private Vector3 nextPos;
	private RaycastHit hit;
	private bool done = false;

	void Start () {
		currentSpeed = this.transform.forward.normalized * speed;
		Invoke ("die", ttl);
	}

	void die(){
		Instantiate (explosion, this.transform.position, Quaternion.identity);
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
				if (explodeOnTouch) {
					Invoke ("die", 0);
				} else {
					if (stick) {
						this.transform.SetParent (hit.transform);
						this.transform.position = hit.point; //stucks the arrow at a predictible depth
						done = true;
					} else {
						//bounce
						this.transform.position = hit.point;
						currentSpeed = Vector3.Reflect (currentSpeed, hit.normal) * bounceFactor;
					}
				}
			} else {
				this.transform.position = nextPos;
				this.gameObject.transform.forward = currentSpeed;
				currentSpeed.y -= gravity * Time.deltaTime;
			}
		}
	}
}