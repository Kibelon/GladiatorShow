/********************************************
 * Maded by Jesús Gracia Güell 22/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float speed = 10;
	public GameObject explosion;
	public GameObject visiblePart;
	public float timeStuck = 0;
	public float ttl = 10;
	public float diviationMagnitude = 0.1f;
	public float diviationTime = 0.1f;
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	private Vector3 currentSpeed;
	private Vector3 nextPos;
	private RaycastHit hit;
	private bool done = false;


	void Start () {
		currentSpeed = this.transform.forward.normalized * speed;
		Invoke ("explode", ttl);
		InvokeRepeating ("diviate",diviationTime, diviationTime);
	}

	void die(){
		Destroy (this.gameObject);
	}

	void diviate(){
		currentSpeed += Random.insideUnitSphere * diviationMagnitude;
		this.gameObject.transform.forward = currentSpeed;
	}

	void explode(){
		done = true;
		Instantiate (explosion, this.gameObject.transform.position, Quaternion.identity);
		Invoke ("die", timeStuck);
		//hide rocket
		var emisor = GetComponent<ParticleSystem> ().emission;
		emisor.enabled = false;
		visiblePart.SetActive (false);
	}

	void Update () {
		if (!done) {
			nextPos = this.transform.position + currentSpeed * Time.deltaTime;
			if (Physics.Linecast (this.transform.position, nextPos, out hit)) {
				this.transform.position = hit.point; //stucks the arrow at a predictible depth
				Invoke ("explode",0);
			} else {
				this.transform.position = nextPos;
			}
		}
	}
}
