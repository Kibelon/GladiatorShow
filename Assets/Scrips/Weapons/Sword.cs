using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
	
	public Transform sphereOrigin;
	public float sphereRadius = 1;
	public float damagePower = 10;
	public string damagableTag = "Destructible";

	override public void atack (){

		foreach (RaycastHit hit in Physics.SphereCastAll(sphereOrigin.position, sphereRadius, sphereOrigin.forward,0)) {
			if (hit.collider.gameObject.GetComponent<Damagable> () != null){
				hit.collider.gameObject.GetComponent<Damagable> ().hurt (damagePower, DamageType.pirsing);
			}
		}
	}

	override public void die(){
		Destroy (this.gameObject);
	}

}
