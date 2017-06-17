using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HiveMind {

	private static LinkedList<MonoBehaviour> goodGuys = new LinkedList<MonoBehaviour>(); //a list containing the good guys in the scene
	private static LinkedList<MonoBehaviour> badGuys = new LinkedList<MonoBehaviour>(); // a list containing the bad guys in the scene

	public static void imaGoodGuy(MonoBehaviour mp){
		goodGuys.AddLast (mp);
	}

	public static void imaBadGuy(MonoBehaviour mp){
		badGuys.AddLast (mp);

	}

	public static void imDead(MonoBehaviour mpc, bool good){
		if (good) {
			goodGuys.Remove (mpc);
		} else {
			badGuys.Remove (mpc);
		}
	}


	public static void clearData(){
		goodGuys.Clear();
		badGuys.Clear();
	}

	public static bool canISee(Vector3 me, GameObject you, float sightDistance){
		float distance = Vector3.SqrMagnitude (me - you.transform.position);
		sightDistance = sightDistance * sightDistance;
		if (distance < sightDistance) {
			RaycastHit hit;
			Physics.Linecast (me, you.transform.position, out hit);
			if (hit.collider == you.GetComponent<Collider>()) {
				return true;
			} else {
				return false;
			}
		}else {
			return false;
		}
	}

	public static MonoBehaviour getClosestBadGuy(Vector3 position){
	
		return getclosestThing <MonoBehaviour>(position, badGuys);
	}

	public static MonoBehaviour getReachableBadGuy(Vector3 position, float reach){

		return getclosestReachable <MonoBehaviour>(position, reach, badGuys);
	}

	public static MonoBehaviour getReachableGoodGuy(Vector3 position, float reach){

		return getclosestReachable <MonoBehaviour>(position, reach, goodGuys);
	}

	public static MonoBehaviour getClosestGoodGuy(Vector3 position){

		return getclosestThing <MonoBehaviour>(position, goodGuys);
	}

	private static T getclosestThing<T>(Vector3 position, LinkedList<T> list) where T : MonoBehaviour{
		T result = null;
		float lastDistance = 0;
		foreach (T a in list) {
			if (result == null) {
				result = a;
				lastDistance = (position - a.transform.position).sqrMagnitude;
			} else {
				if (lastDistance > (position - a.transform.position).sqrMagnitude) {
					result = a;
					lastDistance = (position - a.transform.position).sqrMagnitude;
				}

			}

		}
		return result;
	}
	private static T getclosestReachable<T>(Vector3 position, float squaredRange, LinkedList<T> list) where T : MonoBehaviour{
		T result = null;
		float lastDistance = 0;
		float currentDistance;
		RaycastHit hit;
		foreach (T a in list) {
			currentDistance = (position - a.transform.position).sqrMagnitude;
			if (currentDistance < squaredRange) {
				if ((lastDistance != 0 && currentDistance < lastDistance) || (lastDistance == 0)) {
					Physics.Linecast (position, a.transform.position, out hit);
					if (hit.collider == a.GetComponent<Collider>()) {
						result = a;
						lastDistance = currentDistance;
					}
				}
			}
			 
		}
		return result;
	}
}
