using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

	public Transform endOfBarrel;
	public GameObject bulet;
	public float buletSpeed = 10f;
	public float acuracy = 0.01f;

	void Start () {
		amunition = 20;
	}
	
	override public void atack (){

		Instantiate (bulet, endOfBarrel.position, Quaternion.Euler( endOfBarrel.eulerAngles + (Random.insideUnitSphere * acuracy))).SendMessage("setSpeed",buletSpeed);

	}

	override public void die(){
		Destroy (this.gameObject);
	}
}
