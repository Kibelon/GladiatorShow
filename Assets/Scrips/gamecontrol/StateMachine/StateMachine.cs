/********************************************
* 	Maded by Jesús Gracia Güell 15/7/2017	*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	public State[] states;
	public int currentState = 0;
	public float[] stateMachineVar;
	public MonoBehaviour scriptControlled;

//:::::::::::::::::::::::::::: Publicly available Interface ::::::::::::::::::::::::::::::::::::::::
	/// <summary>
	/// Sets the variable of index index to the given value.
	/// </summary>
	/// <param name="index">Index.</param>
	/// <param name="value">Value.</param>
	public void setVar(int index, float value){
		stateMachineVar [index] = value;
	}

//:::::::::::::::::::::::::::: Hiden functions ::::::::::::::::::::::::::::::::::::::::
	void Start () {
		if (states.Length > currentState) { 
			//runing enter calls
			foreach (Action a in states [currentState].onEnter) {
				scriptControlled.Invoke(a.actionName, 0);
			}
		} else {
			print ("Error! The initial state does not exist");
			this.gameObject.SetActive (false);
		}
	}

	void FixedUpdate () {
		if (!doingTransitions ()) {
			//invoking while in functions
			foreach (Action a in states [currentState].whileIn) {
				scriptControlled.Invoke (a.actionName, 0);
			}
		}
	}

	private bool doingTransitions(){
		//checking if it has to transit state
		foreach( Transition t in states [currentState].transitions){
			foreach (Condition c in t.conditions) {
				if (c.check (stateMachineVar [c.varToCheck])) {
					//transitioning state
					transiting(t.destinationState);
					return true;
				}
			}
		}
		return false;
	}

	private void transiting(int newState){
		//exit calls
		foreach (Action a in states [currentState].onExit) {
			scriptControlled.Invoke (a.actionName, 0);
		}
		//enter calls
		foreach (Action a in states [newState].onEnter) {
			scriptControlled.Invoke (a.actionName, 0);
		}
		//changing state
		currentState = newState;
	}
		
}
