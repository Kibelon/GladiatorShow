using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDude : MpcController {

	private Bow bow; 
	//frame variables
	private float travelTime = 0;
	private Vector3 distanceVector;
	private Vector3 forwardVector;
	private float arrowSpeed;

	protected override void Start () {
		bow = weapon.GetComponent<Bow> ();
		arrowSpeed = bow.buletSpeed;
		base.Start ();
	}

	protected override void getToEnemy(){
		//rotating
		/*Vector3 forward = enemy.transform.position - this.gameObject.transform.position;
		forward.y = 0;
		this.gameObject.transform.rotation = Quaternion.LookRotation (forward, Vector3.up);
		//advancing
		Mover(Vector2.right * walkSpeed);*/
		applyMoovement(Vector2.zero);

	}

	protected override void beingActive(){
		applyMoovement(Vector2.zero);
	}

	protected override void closeToEnemy (){
		Vector3 forward = enemy.transform.position - this.gameObject.transform.position;
		forward.y = 0;
		this.gameObject.transform.rotation = Quaternion.LookRotation (forward, Vector3.up);
		//advancing
		applyMoovement(Vector2.zero);
		//aiming
		distanceVector = enemy.transform.position - this.gameObject.transform.position;
		travelTime = Mathf.Sqrt(distanceVector.x * distanceVector.x + distanceVector.z * distanceVector.z)/(arrowSpeed); // T = D/V
		forwardVector = this.transform.forward;
		forwardVector.y = ((distanceVector.y + (4.9f * (travelTime * travelTime)))/travelTime)/ arrowSpeed; // Vy = (y - (1/2) * g * T^2) / T
		weapon.transform.forward = forwardVector;
	}
}
