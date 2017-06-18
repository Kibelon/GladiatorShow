/********************************************
 * Maded by Jesús Gracia Güell 17/6/2017	*
********************************************/

using UnityEngine;
using System.Collections;

public class PlayerControl : Walker, Damagable {
//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float walkingSpeed = 2;
	public float mouseSensivility = 1;
	public Transform headCamera;
	public float[] damageResistance = { 1, 1, 1, 0.5f, 0.5f };
	public Transform hand1; // The hands that hold the weapons
	public Transform hand2;

	//+++++++++++++++++++++++++++++ Button names ++++++++++++++++++++++++++++++
	public string forwardAxis = "Vertical";
	public string sidewaysAxis = "Horizontal";
	public string jumpButton = "Jump";
	public string atackButton1 = "Fire1";
	public string atackButton2 = "Fire2";
	public string rotationXAxis = "Mouse X";
	public string rotationYAxis = "Mouse Y";

	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	public Weapon firstWeapon;
	public Weapon secondWeapon;
	public float health = 100;

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
			HiveMind.imDead (this, true);
			Destroy (this.gameObject);
		}
	}

//:::::::::::::::::::::::::::: Hiden functions ::::::::::::::::::::::::::::::::::::::::
	protected override void Start () {
		HiveMind.imaGoodGuy (this);
		base.Start ();
	}
		
	void Update () {

		//.........................................movement control...........................................
		//walking
		applyMoovement (new Vector2 (Input.GetAxis (forwardAxis), Input.GetAxis (sidewaysAxis)) * walkingSpeed);

		//jumping
		if (Input.GetButton (jumpButton)) {
			applyJump ();
		}

		//Update the character position
		refresh ();

		//.........................................rotation control...........................................

		this.gameObject.transform.Rotate (new Vector3 (0, Input.GetAxisRaw (rotationXAxis) * mouseSensivility, 0));
		headCamera.Rotate(new Vector3 (-Input.GetAxisRaw(rotationYAxis) * mouseSensivility / 2, 0, 0));
		if (headCamera.localEulerAngles.x > 70 && headCamera.localEulerAngles.x < 180) {
			headCamera.localEulerAngles = new Vector3 (70, 0, 0);
		}
		if (headCamera.localEulerAngles.x < 290 && headCamera.localEulerAngles.x > 180) {
			headCamera.localEulerAngles = new Vector3 (290, 0, 0);
		}

		//.........................................atack control...........................................

		//--- Aiming ---
		RaycastHit hit;
		if (Physics.Raycast (headCamera.position, headCamera.forward, out hit, 100)) {
			hand1.LookAt (hit.point);
			hand2.LookAt (hit.point);
		} else {
			hand1.localRotation = Quaternion.identity;
			hand2.localRotation = Quaternion.identity;
		}

		if (Input.GetAxis (atackButton1) > 0 && firstWeapon != null) {
			firstWeapon.atack ();
		}

		if (Input.GetAxis (atackButton2) > 0 && secondWeapon != null) {
			secondWeapon.atack ();
		}

	}
}
