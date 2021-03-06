﻿/********************************************
 * Maded by Jesús Gracia Güell 21/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float speed = 10;
	public float gravity = 9.8f;
	public float damage = 10;
	public float timeStuck = 10;
	public float ttl = 50;
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

	public void setSpeed (float newSpeed){
		speed = newSpeed;
		currentSpeed = this.transform.forward.normalized * newSpeed;
	}

	void Update () {
		if (!done) {
			nextPos = this.transform.position + currentSpeed * Time.deltaTime;
			if (Physics.Linecast (this.transform.position, nextPos, out hit)) {
				if (hit.collider.gameObject.GetComponent<Damagable> () != null){
					hit.collider.gameObject.GetComponent<Damagable> ().hurt (damage, DamageType.pirsing);
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
