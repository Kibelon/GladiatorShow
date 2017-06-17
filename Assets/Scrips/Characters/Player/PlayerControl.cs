/********************************************
 * Maded by Jesús Gracia Güell 17/6/2017	*
********************************************/

using UnityEngine;
using System.Collections;

public class PlayerControl : Walker {
//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	public float walkingSpeed = 2;
	public float mouseSensivility = 1;
	public Transform headCamera;

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
	public void hurt (float damage){
		health -= damage;
		if (health <= 0) {
			health = 0;
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

		if (Input.GetAxis (atackButton1) > 0 && firstWeapon != null) {
			firstWeapon.atack ();
		}

		if (Input.GetAxis (atackButton2) > 0 && secondWeapon != null) {
			secondWeapon.atack ();
		}

	}
}
