/********************************************
 * Maded by Jesús Gracia Güell 17/12/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, Damagable {
	//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float rotationSpeed = 1;
	public float timeBetweenShots = 0.1f;
	public float shootingRange = 10;
	public float acuracy = 0;
	public GameObject projectile;
	public Transform moovingPart;
	public Transform endOfBarrel;
	public float health = 50;
	public float[] damageResistance = { 1, 1, 1, 0.5f, 0.5f };
	public bool imGood = false;
	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	private MonoBehaviour objective;
	private float lastShot;
	private RaycastHit hit;

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
			HiveMind.imDead (this, imGood);
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
	}

	void Update () {
		if (objective != null) {
			float distance = Vector3.SqrMagnitude (moovingPart.position - objective.transform.position);
			Vector3 desiredRotation = objective.transform.position - moovingPart.position;
			moovingPart.forward = Vector3.RotateTowards (moovingPart.forward, desiredRotation, rotationSpeed * Time.deltaTime, 0);
			if((Time.time - lastShot) > timeBetweenShots){
				if(Physics.Raycast (endOfBarrel.transform.position, endOfBarrel.forward, out hit, 500)){
					if (hit.collider.gameObject == objective.gameObject) {
						//shoot
						Instantiate(projectile,endOfBarrel.position, Quaternion.Euler (endOfBarrel.eulerAngles + (Random.insideUnitSphere * acuracy)));
						lastShot = Time.time;
					}
				}
			}
			if(distance > shootingRange){
				objective = null;
			}
		} else {
			if (imGood) {
				objective = HiveMind.getClosestBadGuy (this.transform.position);
			} else {
				objective = HiveMind.getClosestGoodGuy (this.transform.position);
			}
		}
	}
}
