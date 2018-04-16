/********************************************
 * Maded by Jesús Gracia Güell 9/7/2017		*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	private GameObject[] enemyes;
	public float spawnInterval = 0.5f;
	public Transform spawnPoint;

	private Stack<int> spawnQuew;

	public void setEnemyes(GameObject[] list){
		enemyes = list;
	}

	public void setSpawnOrder(Stack<int> idList){
		spawnQuew = idList;
		Invoke ("spawn", 0);
	}

	public void setInterval(float newInterval){
		spawnInterval = newInterval;
	}

	private void spawn(){
		Instantiate (enemyes[spawnQuew.Pop ()], spawnPoint.position, Quaternion.identity);
		if (spawnQuew.Count > 0) {
			Invoke ("spawn", spawnInterval);
		}
	}
}
