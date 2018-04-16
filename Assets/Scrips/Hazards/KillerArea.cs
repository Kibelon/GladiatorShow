/********************************************
 * Maded by Jesús Gracia Güell 27/12/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerArea : MonoBehaviour {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float damage = 10;
	public DamageType type;
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<Damagable> () != null) {
			other.gameObject.GetComponent<Damagable> ().hurt (damage, type);
		}
	}
}
