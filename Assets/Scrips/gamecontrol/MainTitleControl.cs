using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTitleControl : MonoBehaviour {

	public EnemyDor[] goodDors;
	public EnemyDor[] badDors;
	public GameObject[] badGuys;
	public GameObject[] goodGuys;
	public GameObject cameraAxis;
	public int minimumToSpawn = 3;
	public float cameraRotationgSpeed = 1;
	public float timeBetweenChecks = 1;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("checkFight", 0, timeBetweenChecks);
	}
	
	// Update is called once per frame
	void Update () {
		cameraAxis.transform.Rotate (Vector3.up * cameraRotationgSpeed * Time.deltaTime);
	}

	private void checkFight(){
		if (HiveMind.getNumOfBadGuys() <= minimumToSpawn) {
			foreach (EnemyDor d in badDors) {
				d.spawnPack (badGuys [Random.Range (0, badGuys.Length)]);
			}
		}
		if (HiveMind.getNumOfGoodGuys() <= minimumToSpawn) {
			foreach (EnemyDor d in goodDors) {
				d.spawnPack (goodGuys [Random.Range (0, goodGuys.Length)]);
			}
		}
	}

	public void play(){
		HiveMind.clearData ();
		SceneManager.LoadScene (1);
	}
	public void exit(){
		Application.Quit ();
	}
}
