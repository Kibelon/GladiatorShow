/********************************************
 * Maded by Jesús Gracia Güell 22/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float damage = 10;
	public float ttl = 1;
	public float initialRadius = 1;
	public float finalRadius = 2;
	public float reach = 4;
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	private Vector3 center;
	private float displacement;
	private List<GameObject> damaged;

	// Use this for initialization
	void Start () {
		Invoke ("die",ttl);
		damaged = new List<GameObject> ();
		displacement = 0;
		float radius = initialRadius;
		while (displacement < reach) {
			radius = Mathf.Lerp (initialRadius, finalRadius, reach / displacement);
			foreach (Collider hit in Physics.OverlapSphere(this.transform.position + this.transform.forward * (displacement + radius), radius)) {
				if (!damaged.Contains(hit.gameObject) && hit.gameObject.GetComponent<Damagable> () != null) {
					hit.gameObject.GetComponent<Damagable> ().hurt (damage / Vector3.Distance (this.transform.position, hit.transform.position), DamageType.blunt);
					damaged.Add (hit.gameObject);
				}

			}
			displacement += radius;
		}
	}

	private void die (){
		Destroy (this.gameObject);
	}
}
