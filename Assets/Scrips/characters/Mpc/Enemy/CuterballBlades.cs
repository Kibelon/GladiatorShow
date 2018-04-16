/********************************************
 * Maded by Jesús Gracia Güell 30/6/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuterballBlades : MonoBehaviour {

	private CutterBall parent;

	void Start(){
		parent = this.GetComponentInParent<CutterBall> ();
	}

	void OnCollisionEnter(Collision other){
		parent.bladeContact (other);
	}

}
