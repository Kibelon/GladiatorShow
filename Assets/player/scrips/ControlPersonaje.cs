using UnityEngine;
using System.Collections;

public class ControlPersonaje : CharPhysics {
	 
	//movement stuf
	public float VelMovimiento = 2;//velocidad de movimiento del personaje
	public float mouseSensivility = 1; //the sensivility of the mouse
	//buton names
	public string EjeAdelante = "Vertical"; //Nombre del boton para ir adelante
	public string EjeLateral = "Horizontal"; //Nombre del eje para moverse lateralmente
	public string BotonSalto = "Jump";//Nombre del boton de salto
	public string BotonAtaque1 = "Fire1"; // Nombre del boton de ataque
	public string BotonAtaque2 = "Fire2"; // Nombre del boton de ataque
	public string Girar = "Mouse X";//Nombre del eje que gira al personaje

	//other things
	public Weapon firstWeapon; //It is the main weapon that you have equiped
	public Weapon secondWeapon; //It's the secondary weapon
	public float health = 100;


	protected override void Start () {
		HiveMind.imaGoodGuy (this);
		base.Start ();
	}

	public void hurt (float damage){
		health -= damage;
		if (health <= 0) {
			health = 0;
			Destroy (this.gameObject);
		}
	}
		

	// Update is called once per frame
	void Update () {

		//.........................................movement control...........................................
		Mover (new Vector2 (Input.GetAxis (EjeAdelante), Input.GetAxis (EjeLateral)) * VelMovimiento);//control movimiento

		if (Input.GetButton (BotonSalto)) {//control salto
			Saltar ();
		}

		//Mover el personaje
		Actualizar ();

		//.........................................rotation control...........................................

		this.gameObject.transform.Rotate (new Vector3 (0, Input.GetAxis (Girar) * mouseSensivility, 0));

		//.........................................atack control...........................................

		if (Input.GetButtonDown (BotonAtaque1) && firstWeapon != null) {
			firstWeapon.atack ();
		}

		if (Input.GetButtonDown (BotonAtaque2) && secondWeapon != null) {
			secondWeapon.atack ();
		}

	}
}
