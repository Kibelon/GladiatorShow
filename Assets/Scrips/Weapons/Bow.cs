/********************************************
 * Maded by Jesús Gracia Güell 18/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, Weapon {
//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public Transform endOfBarrel;
	public GameObject bulet;
	public float buletSpeed = 10f;
	public float acuracy = 0.01f;
	public float bowTension = 0;

	//+++++++++++++++++++++++++++++ Button names ++++++++++++++++++++++++++++++
	public string fireButton = "Shoot";
	public string pull = "Specialhav";

	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	public bool triggered = false;

	public int amunition{
		get;
		set;
	}

	public WeaponType type {
		get;
	}
		
	public void framecall (){
		if (triggered) {
			if (Input.GetAxis (fireButton) == 0 && Input.GetAxis(pull) == 0) {
				triggered = false;
			}
		}else{
			if (Input.GetAxis (fireButton) > 0) {
				Instantiate (bulet, endOfBarrel.position, Quaternion.Euler( endOfBarrel.eulerAngles + (Random.insideUnitSphere * acuracy))).SendMessage("setSpeed",buletSpeed * Input.GetAxis(pull));
				triggered = true;
			}
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
