/********************************************
 * Maded by Jesús Gracia Güell 18/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, Weapon {
	
	public Transform sphereOrigin;
	public float sphereRadius = 1;
	public float damagePower = 10;
	public string damagableTag = "Destructible";
	public string fireButton = "Fire1";

	public int amunition{
		get;
		set;
	}

	public WeaponType type {
		get;
	}

	public void framecall (){
		if(Input.GetButtonDown(fireButton)){
			atack ();
		}
	}

	public void exit(){

	}

	public void atack (){

		foreach (RaycastHit hit in Physics.SphereCastAll(sphereOrigin.position, sphereRadius, sphereOrigin.forward,0)) {
			if (hit.collider.gameObject.GetComponent<Damagable> () != null){
				hit.collider.gameObject.GetComponent<Damagable> ().hurt (damagePower, DamageType.pirsing);
			}
		}
	}
		

}
