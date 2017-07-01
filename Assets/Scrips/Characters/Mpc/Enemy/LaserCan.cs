/********************************************
 * Maded by Jesús Gracia Güell 29/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCan : Walker, Damagable {
	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float close = 20;
	public float inRange = 9;
	public float engage = 5;
	public float engagmentTime = 4;
	private float sqInRange;
	public float runningSpeed = 10;
	public float walkingSpeed = 5;
	public float turningSpeed = 1;
	public float damage = 10;
	public GameObject[] lasersGO = { null, null, null };
	public Collider meleHitbox; //contains the hitbox that will hurt while attacking
	public DamageType atackType = DamageType.pirsing;
	public bool imGood = false;
	public float health = 50;
	public float[] damageResistance = { 1, 1, 0, 0.5f, 0.5f };
	private Animator animator;
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	private MonoBehaviour objective; //the enemy that the MPC whants to kill
	private Vector3 destinationShift; //The shift on the objective positin applied so the MPC does not run straight into it.
	private bool atacking = false;
	private bool engaged = false;
	private float lasteEngagement = 0;
	private Weapon[] lasers = { null, null, null };

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

	//:::::::::::::::::::::::::::: Animation Stuff ::::::::::::::::::::::::::::::::::::::::
	public void activateLaser(){
		lasers [0].atack ();
	}

	public void activateLasers(){
		for(int i = 0; i < 3; i++){
			lasers [i].atack ();
		}
	}

	public void deactivateLasers(){
		for(int i = 0; i < 3; i++){
			lasers [i].exit ();
		}
		animator.SetBool ("Attacking", false);
		animator.SetBool ("Engaged", false);
		atacking = false;
		engaged = false;
	}

	//:::::::::::::::::::::::::::: Hiden functions ::::::::::::::::::::::::::::::::::::::::
	override protected void Start () {
		animator = this.gameObject.GetComponent<Animator> ();
		if (imGood) {
			HiveMind.imaGoodGuy (this);
		} else {
			HiveMind.imaBadGuy (this);
		}
		//squaring distances
		close = close * close;
		sqInRange = inRange * inRange;
		engage = engage * engage;

		// Laser inisialization
		for(int i = 0; i < 3; i++){
			lasers [i] = lasersGO [i].GetComponent<Weapon> ();
		}

		base.Start ();
	}

	// Update is called once per frame
	void Update () {

		if (objective != null) {
			if (atacking == false) {
				if (engaged == false) {
					//====================================== follow objective ======================================
					float distance = Vector3.SqrMagnitude (this.gameObject.transform.position - objective.transform.position);
					//-------------------------- Rotation --------------------------
					Vector3 desiredRotation;
					if (distance > sqInRange) {
						desiredRotation = objective.transform.position - (this.transform.position + destinationShift);
					} else {
						desiredRotation = objective.transform.position - this.transform.position;
					}
					desiredRotation.y = 0;
					this.transform.forward = Vector3.RotateTowards (this.transform.forward, desiredRotation, turningSpeed * Time.deltaTime, 0);

					//-------------------------- Moovement --------------------------
					if (distance > close) {
						applyMoovement (new Vector2 (runningSpeed, 0));
					} else {
						if (distance > sqInRange) {
							applyMoovement (new Vector2 (walkingSpeed, 0));
						} else {
							if (distance > engage) {
								applyMoovement (Vector2.zero);
								RaycastHit hit;
								if (Physics.Linecast (this.transform.position, this.transform.position + this.transform.forward * inRange, out hit)) {
									if (hit.collider.gameObject == objective.gameObject) {
										animator.SetBool ("Attacking", true);
										atacking = true;
									}
								}
							} else {
								engaged = true;
								animator.SetBool ("Engaged", true);
								lasteEngagement = Time.time;
								applyMoovement (Vector2.zero);
							}
						}
					}
				} else {
					//engaged
					this.gameObject.transform.Rotate (new Vector3 (0, turningSpeed * 57.3f * Time.deltaTime, 0));// 57.3 is so to transform radians to degrees
					Vector3 moovement = objective.transform.position - this.transform.position;
					moovement.y = 0;
					applyPush (moovement.normalized * walkingSpeed);
					if (Time.time - lasteEngagement > engagmentTime && (objective.transform.position - this.transform.position).sqrMagnitude > engage) {
						animator.SetBool ("Engaged", false);
					}
				}
			} else {
				//attacking
				applyMoovement (Vector2.zero);
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

		//Controling lasers
		if(atacking){
			lasers [0].framecall ();
		}
		if(engaged){
			for(int i = 0; i < 3; i++){
				lasers [i].framecall ();
			}
		}
	}
}
