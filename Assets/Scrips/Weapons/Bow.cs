/********************************************
 * Maded by Jesús Gracia Güell 18/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, Weapon {

	public Transform endOfBarrel;
	public GameObject bulet;
	public float buletSpeed = 10f;
	public float acuracy = 0.01f;
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

	void Start () {
		amunition = 20;
	}
	
	public void atack (){
		
		Instantiate (bulet, endOfBarrel.position, Quaternion.Euler( endOfBarrel.eulerAngles + (Random.insideUnitSphere * acuracy))).SendMessage("setSpeed",buletSpeed);

	}

	public void die(){
		Destroy (this.gameObject);
	}
}
