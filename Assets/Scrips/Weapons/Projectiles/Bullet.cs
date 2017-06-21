/********************************************
 * Maded by Jesús Gracia Güell 18/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float speed = 10;
	public float damage = 10;
	public float timeStuck = 0.1f;
	public float ttl = 10;
	public LineRenderer motionBlur;
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
		Destroy (this.gameObject);
	}

	void Update () {
		if (!done) {
			Vector3[] line = new Vector3[2];
			line [0] = this.transform.position;
			nextPos = this.transform.position + currentSpeed * Time.deltaTime;
			if (Physics.Linecast (this.transform.position, nextPos, out hit)) {
				if (hit.collider.gameObject.GetComponent<Damagable> () != null) {
					hit.collider.gameObject.GetComponent<Damagable> ().hurt (damage, DamageType.pirsing);
				}
				line [1] = hit.point;
				motionBlur.SetPositions (line);
				this.transform.position = hit.point; //stucks the arrow at a predictible depth
				done = true;
			} else {
				line [1] = nextPos;
				motionBlur.SetPositions (line);
				this.transform.position = nextPos;
			}
		} else {
			Destroy (this.gameObject);
		}
	}
}
