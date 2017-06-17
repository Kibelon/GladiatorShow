using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class of all weapons
/// </summary>
public abstract class Weapon : MonoBehaviour {

	private bool atacking = false;
	protected int amunition = 0;
	private float health = 100; //health of the weapon
	public const float MAX_HEALTH = 100;

	public abstract void atack ();

	public bool isAtacking (){
		return atacking;
	}

	public int getAmunition (){
		return amunition;
	}

	public int setAmunition (){
		return amunition;
	}

	public float gethealth (){
		return health;
	}

	public void hurt (float damage){
		health -= damage;
		if (health <= 0) {
			health = 0;
			die ();
		}
	}

	public void heal (float amount){
		health += amount;
		if (health > MAX_HEALTH) {
			health = MAX_HEALTH;
		}
	}

	public abstract void die ();
}
