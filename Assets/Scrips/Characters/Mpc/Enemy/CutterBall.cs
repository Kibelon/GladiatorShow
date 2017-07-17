/********************************************
 * Maded by Jesús Gracia Güell 30/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterBall : MonoBehaviour , Damagable {
	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float close = 10;
	public float runningSpeed = 10;
	public float walkingSpeed = 5;
	public float turningSpeed = 720;
	public float damage = 10;
	public DamageType atackType = DamageType.pirsing;
	public bool imGood = false;
	public float health = 50;
	public float[] damageResistance = { 1, 1, 1, 0.5f, 0.5f };
	public GameObject blades;
	public float hitStrength = 100;
	public float stunnedPeriod = 2;
	private CharacterController sphere;
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	private MonoBehaviour objective; //the enemy that the MPC whants to kill
	private bool stunned = false;
	private Vector3 currentSpeed;
	private float lastStunned;


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

	public void bladeContact(Collision hit){
		if (hit.collider.gameObject != this.gameObject) {
			if (hit.collider.gameObject.GetComponent<Damagable> () != null) {
				hit.collider.gameObject.GetComponent<Damagable> ().hurt (damage, DamageType.pirsing);
			}

			stunned = true;
			lastStunned = Time.time;
			currentSpeed = (this.gameObject.transform.position - hit.contacts[0].point).normalized * hitStrength;
		}


	}

	//:::::::::::::::::::::::::::: Hiden functions ::::::::::::::::::::::::::::::::::::::::
	void Start () {
		if (imGood) {
			HiveMind.imaGoodGuy (this);
		} else {
			HiveMind.imaBadGuy (this);
		}
		sphere = this.gameObject.GetComponent<CharacterController> ();
		//squaring distances
		close = close * close;

	}

	// Update is called once per frame
	void Update () {

		if (objective != null) {
			if (stunned == false) {
				//====================================== follow objective ======================================
				float distance = Vector3.SqrMagnitude (objective.transform.position - this.transform.position);
				//-------------------------- Rotation --------------------------
				blades.transform.Rotate (new Vector3 (0, 0, turningSpeed * Time.deltaTime));

				//-------------------------- Moovement --------------------------
				Vector3 moovement = (objective.transform.position - this.transform.position).normalized;
				if (distance > close) {
					sphere.Move (moovement * runningSpeed * Time.deltaTime);
				} else {
					sphere.Move (moovement * walkingSpeed * Time.deltaTime);
				}
			} else {
				//is stunned
				float lerpFactor = (Time.time - lastStunned)/stunnedPeriod;
				sphere.Move (Vector3.Lerp(currentSpeed, Vector3.zero,lerpFactor) * Time.deltaTime);
				blades.transform.Rotate (new Vector3 (0, 0,Mathf.Lerp(0, turningSpeed,lerpFactor) * Time.deltaTime));
				if (Time.time - lastStunned > stunnedPeriod) {
					stunned = false;
				}
			}

		} else {
			//====================================== get new objective ======================================
			if (imGood){
				objective = HiveMind.getClosestBadGuy (this.transform.position);
			}else{
				objective = HiveMind.getClosestGoodGuy (this.transform.position);
			}
		}
	}
}