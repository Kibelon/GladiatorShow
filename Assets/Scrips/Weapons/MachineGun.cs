using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour, Weapon {

	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public Transform endOfBarrel;
	public GameObject bulet;
	public float buletSpeed = 10f;
	public float acuracy = 0.01f;
	public float firerate = 0.5f; //time between shots

	//+++++++++++++++++++++++++++++ Button names ++++++++++++++++++++++++++++++
	public string fireButton = "Shoot";

	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	public bool triggered = false;
	public float lastShotTime = 0;

	public int amunition{
		get;
		set;
	}

	public WeaponType type {
		get;
	}

	public void framecall (){
		if (Input.GetAxis (fireButton) > 0 && Time.time - lastShotTime > firerate) {
			triggered = false;
			lastShotTime = Time.time;
			atack ();
		}

	}

	public void exit(){

	}

	void Start () {
		amunition = 20;
	}

	public void atack (){

		Instantiate (bulet, endOfBarrel.position, Quaternion.Euler (endOfBarrel.eulerAngles + (Random.insideUnitSphere * acuracy)));

	}

	public void die(){
		Destroy (this.gameObject);
	}
}
