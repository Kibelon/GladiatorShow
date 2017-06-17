using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MpcController : Walker, Damagable {

	//AI states
	public enum States {Rest, Active, Defending, Atacking};

	public States state = States.Rest;		//current state
	public MonoBehaviour enemy;				//current enemy
	public Vector3	objectivePosition;		//the positionof deffense
	public float sightDistance = 10f;		//distance at witch detect enemys
	public bool amIgood = false;			//determines weather an MPC is an enemy or not
	public float[] damageResistance = { 1, 1, 1, 0.5f, 0.5f };

	public float health = 100;
	public Weapon weapon;
	public float atackFrequency = 1;
	public float atackdiference = 0.2f;
	private float nextAtack = 0;
	private float lastAtackTime = 0;
	public float atackRange = 0.5f;
	public float defendingRange = 5f;
	public float walkSpeed = 1;
	public float rotationSpeed = 1;

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
		
	protected override void Start () {
		if (amIgood) {
			HiveMind.imaGoodGuy (this);
		} else {
			HiveMind.imaBadGuy (this);
		}
		atackRange = atackRange * atackRange;
		sightDistance = sightDistance * sightDistance;
		InvokeRepeating ("updateState", Random.value, 0.7f);
		objectivePosition = this.transform.position;
		base.Start ();
	}

	void updateState () {

		if (enemy != null) {
			if (canISee (this.transform.position, enemy.gameObject, sightDistance)) {
			} else {
				if (amIgood) {
					enemy = HiveMind.getReachableBadGuy (this.transform.position, sightDistance);
				} else {
					enemy = HiveMind.getReachableGoodGuy (this.transform.position, sightDistance);
				}
			}
		} else {
			//there is no enemy, getting closest enemy
			if (amIgood) {
				enemy = HiveMind.getReachableBadGuy (this.transform.position, sightDistance);
			} else {
				enemy = HiveMind.getReachableGoodGuy (this.transform.position, sightDistance);
			}
		}

	}

	public static bool canISee(Vector3 me, GameObject you, float sightDistance){
		float distance = Vector3.SqrMagnitude (me - you.transform.position);
		sightDistance = sightDistance * sightDistance;
		if (distance < sightDistance) {
			RaycastHit hit;
			Physics.Linecast (me, you.transform.position, out hit);
			if (hit.collider == you.GetComponent<Collider>()) {
				return true;
			} else {
				return false;
			}
		}else {
			return false;
		}
	}

	protected virtual void getToEnemy (){
		print("going to enemy must be implemented");
	}

	protected virtual void getToPlace (){
		print("going to place must be implemented");
	}

	protected virtual void closeToEnemy (){
		print("Close to enemy must be implemented");
	}

	protected virtual void beingActive (){
		print("Being active must be implemented");
	}


	void Update () {
		if (enemy != null) {
			if ((this.transform.position - enemy.transform.position).sqrMagnitude < atackRange) {
				//the enemy is in range
				if ((Time.time - lastAtackTime) > nextAtack) {
					weapon.atack ();
					lastAtackTime = Time.fixedTime;
					nextAtack = atackFrequency + Random.Range (-atackdiference, atackdiference);
				}
				closeToEnemy ();
			} else {
				getToEnemy ();
			}
		} else {
			beingActive ();
		}

		refresh (); // updates the moovement
	}
}
