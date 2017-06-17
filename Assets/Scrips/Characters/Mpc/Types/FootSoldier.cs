using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSoldier : MpcController {

	protected override void getToEnemy(){
		//rotating
		Vector3 forward = enemy.transform.position - this.gameObject.transform.position;
		forward.y = 0;
		this.gameObject.transform.rotation = Quaternion.LookRotation (forward, Vector3.up);
		//advancing
		applyMoovement(Vector2.right * walkSpeed);
	}

	protected override void getToPlace(){
		//rotating
		Vector3 forward = objectivePosition - this.gameObject.transform.position;
		forward.y = 0;
		this.gameObject.transform.rotation = Quaternion.LookRotation (forward, Vector3.up);
		//advancing
		applyMoovement(Vector2.right * walkSpeed);

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
	}

}
