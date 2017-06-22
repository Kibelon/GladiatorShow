/********************************************
 * Maded by Jesús Gracia Güell 22/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float damage = 10;
	public float ttl = 1;
	public float radius = 2;
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++

	// Use this for initialization
	void Start () {
		Invoke ("die",ttl);
		foreach (Collider hit in Physics.OverlapSphere(this.transform.position, radius)) {
			if (hit.gameObject.GetComponent<Damagable> () != null) {
				hit.gameObject.GetComponent<Damagable> ().hurt (damage / Vector3.Distance (this.transform.position, hit.transform.position), DamageType.blunt);
			}
		}
	}

	private void die (){
		Destroy (this.gameObject);
	}
}
