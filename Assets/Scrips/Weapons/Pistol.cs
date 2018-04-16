/********************************************
 * Maded by Jesús Gracia Güell 18/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, Weapon {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public Transform endOfBarrel;
	public GameObject bulet;
	public GameObject lightFlash;
	private Animation lightFlashAnim;
	public float buletSpeed = 10f;
	public float acuracy = 0.01f;

	//+++++++++++++++++++++++++++++ Button names ++++++++++++++++++++++++++++++
	public string fireButton = "Shoot";

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
			if (Input.GetAxis (fireButton) == 0) {
				triggered = false;
			}
		}else{
			if (Input.GetAxis (fireButton) > 0) {
				atack ();
				triggered = true;
			}
		}
	}

	public void exit(){

	}

	void Start () {
		amunition = 20;
		lightFlashAnim = lightFlash.GetComponent<Animation>();
	}

	public void atack (){

		Instantiate (bulet, endOfBarrel.position, Quaternion.Euler (endOfBarrel.eulerAngles + (Random.insideUnitSphere * acuracy)));
		lightFlashAnim.Play ();

	}

	public void die(){
		Destroy (this.gameObject);
	}
}
