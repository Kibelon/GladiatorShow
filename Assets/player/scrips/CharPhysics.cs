/*
 * Creado por Jesús Gracia Güell 8/8/2016
 * 
 * Controlador del movimiento y colisiones del personaje
 */

using UnityEngine;
using System.Collections;

abstract public class CharPhysics : MonoBehaviour {
	//variables de movimiento
	public Vector3 Velocidad = Vector3.zero; //velocidad del personaje
	public float SaltoVel = 10;//velocidad de salto
	public float YNormal = 0.5f;//Longitud minima de la normal del suelo para que el personaje se pueda mover
	public float YSlipNormal = 0.8f;//normal at wich the character slips
	public float slipVel = 1;//the slipping speed

	//variables necessarias
	protected CharacterController Capsula;//cuerpo de colisiones del personaje (Tiene que existir en el objeto)
	private Vector3 gravedad;//fuerza de la gravedad
	protected Vector3 NormalSuelo = Vector3.zero;//la normal del suelo que tocamos
	public Transform Eje;//transform del objeto que determinara la direccion delantera



	//processo de inicio
	protected virtual void Start (){
		Capsula = this.gameObject.GetComponent<CharacterController>();//obteniendo capsula
		gravedad = Physics.gravity;//obtniendo gravedad del mundo
	}
		
	///<summary> Actualiza el personaje (Tiene que llamarse a cada fotograma)</summary>
	protected void Actualizar () {

		//control slipping
		if (Capsula.isGrounded  && NormalSuelo.y <= YSlipNormal) {
			Vector3 slipForfce = new Vector3 (NormalSuelo.x, 0, NormalSuelo.z);
			Velocidad += slipVel * slipForfce.normalized;
		}

		//aplicando movimiento
		Capsula.Move (Velocidad * Time.deltaTime);

		//controlando gravedad
		if (Capsula.isGrounded && NormalSuelo.y > YSlipNormal){
			Velocidad.y = gravedad.y;
		}else{
			Velocidad += gravedad * Time.deltaTime;
		}

	}

	///<summary>Aplicar movimienro de andar relativo</summary>
	protected void Mover (Vector2 velIn){
		Vector2 aux = new Vector2 (-Eje.forward.x, Eje.forward.z);
		aux = aux * velIn.y;
		aux += new Vector2 (Eje.forward.z, Eje.forward.x) * velIn.x;
		Velocidad = new Vector3 (aux.y, Velocidad.y, aux.x);
	}

	///<summary>Para que el personaje pueda ser empujado en velocidad absoluta</summary>
	public void Empujar (Vector3 Vin){
		Velocidad = Vin;
	}

	///<summary>Ejecuta un salto</summary>
	protected void Saltar (){

		if (NormalSuelo.y > YSlipNormal && Capsula.isGrounded) {//permitimos saltar solo si el suelo es plano i estamos en el suelo
			Velocidad.y = SaltoVel;	
		}
	}

	///<summary>En chocar contra algo captura la normal</summary>
	void OnControllerColliderHit(ControllerColliderHit hit) {
		NormalSuelo = hit.normal;//capturamos la normal del suelo
	}
		

}
