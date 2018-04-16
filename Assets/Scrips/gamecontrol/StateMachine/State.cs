/********************************************
 * Maded by Jesús Gracia Güell 15/7/2017	*
********************************************/
using UnityEngine;
[System.Serializable]
public class State{

	public Transition[] transitions;
	public Action[] onEnter;
	public Action[] onExit;
	public Action[] whileIn;
}
