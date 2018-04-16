/********************************************
 * Maded by Jesús Gracia Güell 6/7/2017		*
********************************************/
using System.Collections.Generic;
using UnityEngine;

public class ArenaFight : MonoBehaviour {
//:::::::::::::::::::::::::::: Class parameters ::::::::::::::::::::::::::::::::::::::::
	public EnemyDor[] dors;
	public EnemyWave[] waves;
	public int currentWave = 0;
	public float checkLatency = 2;
	private bool betweenWaves = false;
	public PlayerUI ui;

//:::::::::::::::::::::::::::: Internal Classes ::::::::::::::::::::::::::::::::::::::::
	[System.Serializable]
	public class EnemyWave {
		public GameObject[] enemyPacks;
	}

//:::::::::::::::::::::::::::: Publicly available Interface ::::::::::::::::::::::::::::::::::::::::
	public void advanceWave(){
		currentWave++;
		if (currentWave >= waves.Length) {
			playerWins ();
		} else {
			betweenWaves = true;
			spawnWave ();
		}
	}

	public void playerWins(){
		ui.showWin ();
	}

	public void spawnWave(){
		for(int i = 0; i < waves[currentWave].enemyPacks.Length; i++){
			if (waves [currentWave].enemyPacks [i] != null) {
				dors [i].spawnPack (waves [currentWave].enemyPacks [i]);
			}
		}
	}

	public void chechIfWaveIsOver(){
		if (betweenWaves) {
			if (HiveMind.getNumOfBadGuys () > 0) {
				betweenWaves = false;
			}
		} else {
			if(HiveMind.getNumOfBadGuys () == 0){
				advanceWave ();
			}
		}
	}

//:::::::::::::::::::::::::::: Hiden functions ::::::::::::::::::::::::::::::::::::::::
	void Start(){
		InvokeRepeating ("chechIfWaveIsOver", checkLatency, checkLatency);
	}
		
}
