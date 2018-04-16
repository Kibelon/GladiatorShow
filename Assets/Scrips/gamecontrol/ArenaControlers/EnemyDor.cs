/********************************************
 * Maded by Jesús Gracia Güell 12/7/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDor : MonoBehaviour {

	public Transform zeroPoint;
	public Vector3 boxSize; // Box used to see if the box is empty
	public float checkBoxLatency = 1;
	private Animator animator;
	private GameObject toSpawn;
	public bool isOpen = false;
	public bool spawning = false;

	void Start () {
		animator = this.GetComponent<Animator> ();
		InvokeRepeating ("isEmpty", checkBoxLatency, checkBoxLatency);
	}

	public void spawnPack(GameObject pack){
		animator.SetBool ("spawning", true);
		animator.SetBool ("stayOpen", true);
		isOpen = true;
		spawning = true;
		toSpawn = pack;
	}

	public void spawn(){
		for (int i = 0; i < toSpawn.transform.childCount;i++) {
			Instantiate (toSpawn.transform.GetChild(i), zeroPoint.position + zeroPoint.transform.TransformVector(toSpawn.transform.GetChild(i).position), zeroPoint.rotation);
		}
		animator.SetBool ("spawning", false);
		spawning = false;
	}

	private void isEmpty(){
		if (!Physics.CheckBox (zeroPoint.position, boxSize / 2, zeroPoint.rotation)) {
			if (isOpen && !spawning) {
				animator.SetBool ("stayOpen", false);
				isOpen = false;
			}
		} else {
			if(!isOpen){
				animator.SetBool ("stayOpen", true);
				isOpen = true;
			}
		}
	}
}