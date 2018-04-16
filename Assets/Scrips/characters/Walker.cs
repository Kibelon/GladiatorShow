/********************************************
 * Maded by Jesús Gracia Güell 17/6/2017	*
********************************************/

using UnityEngine;
using System.Collections;

/// <summary>
/// Controls all the phisics regarding the moovement of the character
/// </summary>
abstract public class Walker : MonoBehaviour {
//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	//+++++++++++++++++++++++++++++ Constant parameters ++++++++++++++++++++++++++++++
	protected CharacterController capsulePointer; //Capsule collider for the character (It must exist in the object)
	public float jumpSpeed = 10; //Speed applied upon jumping
	public float yNormal = 0.5f; //Maximum y normal length upon the character can not walk.
	public float ySlipNormal = 0.8f; //Minimum normal at wich the character slips
	public float slipSpeed = 1; //The slipping speed
	private float gravity; //The value of gravity
	public Transform forwardPointer; //Transform that determines the forward direction of the character

	//+++++++++++++++++++++++++++++ Runtime parameters ++++++++++++++++++++++++++++++
	public Vector3 speed = Vector3.zero; //Current character speed
	protected Vector3 floorNormal = Vector3.zero; //Current normal of the floor


//:::::::::::::::::::::::::::: Publicly available Interface ::::::::::::::::::::::::::::::::::::::::
	/// <summary>
	/// Applies a forced speed to the character.
	/// </summary>
	/// <param name="Vin">Global velocity in.</param>
	public void applyPush (Vector3 Vin){
		speed = Vin;
	}

	/// <summary>
	/// Applies the walking moovement.
	/// </summary>
	/// <param name="velIn">walking velocity in.</param>
	protected void applyMoovement (Vector2 velIn){
		Vector2 aux = new Vector2 (-forwardPointer.forward.x, forwardPointer.forward.z);
		aux = aux * velIn.y;
		aux += new Vector2 (forwardPointer.forward.z, forwardPointer.forward.x) * velIn.x;
		speed = new Vector3 (aux.y, speed.y, aux.x);
	}

	/// <summary>
	/// Trigers a jump
	/// </summary>
	protected void applyJump (){

		if (floorNormal.y > ySlipNormal && capsulePointer.isGrounded) {//permitimos saltar solo si el suelo es plano i estamos en el suelo
			speed.y = jumpSpeed;	
		}
	}

	/// <summary>
	/// updates the character position. Must be called each frame.
	/// </summary>
	protected void refresh () {

		//slipping control
		if (capsulePointer.isGrounded  && floorNormal.y <= ySlipNormal) {
			Vector3 slipForfce = new Vector3 (floorNormal.x, 0, floorNormal.z);
			speed += slipSpeed * slipForfce.normalized;
		}

		//applying moovement
		capsulePointer.Move (speed * Time.deltaTime);

		//gravity control
		if (capsulePointer.isGrounded && floorNormal.y > ySlipNormal){
			speed.y = gravity;
		}else{
			speed.y -= (gravity * gravity)/2 * Time.deltaTime;
		}

	}

//:::::::::::::::::::::::::::: Hiden functions ::::::::::::::::::::::::::::::::::::::::
	protected virtual void Start (){
		capsulePointer = this.gameObject.GetComponent<CharacterController>();//obteniendo capsula
		gravity = Physics.gravity.y;//obtniendo gravedad del mundo
	}
		
	void OnControllerColliderHit(ControllerColliderHit hit) {
		floorNormal = hit.normal;//capturamos la normal del suelo
	}
		

}
