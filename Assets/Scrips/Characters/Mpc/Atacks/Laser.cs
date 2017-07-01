/********************************************
 * Maded by Jesús Gracia Güell 18/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, Weapon {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float damage = 10;
	public float range = 100;
	public LineRenderer laserBeam;
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	private RaycastHit hit;
	private bool active = false;

	//:::::::::::::::::::::::::::: Publicly available Interface ::::::::::::::::::::::::::::::::::::::::
	public int amunition {
		get;
		set;
	}

	public WeaponType type {
		get;
	}
		
	public void exit(){
		active = false;
		laserBeam.enabled = false;
	}

	public void atack (){
		active = true;
		laserBeam.enabled = true;
		//repositioning laser
		Vector3[] line = new Vector3[2];
		line [0] = this.transform.position;
		if (Physics.Linecast (this.transform.position, this.transform.position + this.transform.forward * range, out hit)) {
			line [1] = hit.point;
		} else {
			line [1] = this.transform.position + this.transform.forward * range;
		}
		laserBeam.SetPositions (line);
	}

	public void framecall (){
		if (active) {
			Vector3[] line = new Vector3[2];
			line [0] = this.transform.position;
			if (Physics.Linecast (this.transform.position, this.transform.position + this.transform.forward * range, out hit)) {
				line [1] = hit.point;
				if (hit.collider.gameObject.GetComponent<Damagable> () != null) {
					hit.collider.gameObject.GetComponent<Damagable> ().hurt (damage * Time.deltaTime, DamageType.fire);
				}
			} else {
				line [1] = this.transform.position + this.transform.forward * range;
			}
			laserBeam.SetPositions (line);
		}
	}
		
}
