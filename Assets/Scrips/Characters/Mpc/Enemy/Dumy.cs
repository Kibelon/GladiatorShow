/********************************************
 * Maded by Jesús Gracia Güell 24/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumy : Walker, Damagable {
//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float close = 10;
	public float inRange = 1;
	private float sqInRange;
	public float runningSpeed = 10;
	public float walkingSpeed = 5;
	public float turningSpeed = 10;
	public float damage = 10;
	public Collider meleHitbox; //contains the hitbox that will hurt while attacking
	public DamageType atackType = DamageType.pirsing;
	public bool imGood = false;
	public float health = 50;
	public float[] damageResistance = { 1, 1, 1, 0.5f, 0.5f };
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	private MonoBehaviour objective; //the enemy that the MPC whants to kill
	private Vector3 destinationShift; //The shift on the objective positin applied so the MPC does not run straight into it.

//:::::::::::::::::::::::::::: Publicly available Interface ::::::::::::::::::::::::::::::::::::::::
	public void hurt (float value, DamageType type){

		switch (type){
		case DamageType.blunt:
			health -= value * damageResistance [0];
			break;
		case DamageType.pirsing:
			health -= value * damageResistance [1];
			break;
		case DamageType.fire:
			health -= value * damageResistance [2];
			break;
		case DamageType.electric:
			health -= value * damageResistance [3];
			break;
		case DamageType.corrosive:
			health -= value * damageResistance [4];
			break;
		}
		if (health <= 0) {
			health = 0;
			HiveMind.imDead (this, false);
			Destroy (this.gameObject);
		}
	}

//:::::::::::::::::::::::::::: Hiden functions ::::::::::::::::::::::::::::::::::::::::
	void Start () {
		if (imGood) {
			HiveMind.imaGoodGuy (this);
		} else {
			HiveMind.imaBadGuy (this);
		}
		//squaring distances
		close = close * close;
		sqInRange = inRange * inRange;

		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {

		if (objective != null) {
			//====================================== follow objective ======================================
			float distance = Vector3.SqrMagnitude(this.gameObject.transform.position - objective.transform.position);
			//-------------------------- Rotation --------------------------
			Vector3 desiredRotation;
			if (distance > sqInRange) {
				desiredRotation = objective.transform.position - (this.transform.position + destinationShift);
			} else {
				desiredRotation = objective.transform.position - this.transform.position;
			}
			desiredRotation.y = 0;
			this.transform.forward = Vector3.RotateTowards (this.transform.forward, desiredRotation, turningSpeed, 0);

			//-------------------------- Moovement --------------------------
			if (distance > close) {
				applyMoovement (new Vector2 (runningSpeed, 0));
			} else {
				if (distance > sqInRange) {
					applyMoovement (new Vector2 (walkingSpeed, 0));
				} else {
					applyMoovement (Vector2.zero);
				}
			}

		} else {
			//====================================== get new objective ======================================
			if (imGood){
				objective = HiveMind.getClosestBadGuy (this.transform.position);
			}else{
				objective = HiveMind.getClosestGoodGuy (this.transform.position);
			}
			//get new random side
			Vector2 aux = Random.insideUnitCircle.normalized * inRange;
			destinationShift = new Vector3 (aux.x, 0, aux.y);
			//stop moovement
			applyMoovement (Vector2.zero);
		}

		//Update the character position
		refresh ();
	}
}
