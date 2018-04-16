/********************************************
 * Maded by Jesús Gracia Güell 6/7/2017		*
********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour {

	public Text hp;
	public GameObject deadSchreen;
	public GameObject pausedSchreen;
	public Text tytle;

	public void Start(){
		hp.text = 100.ToString();
	}

	public void hpUpdate(int value){
		hp.text = value.ToString();
	}

	public void showDead(){
		deadSchreen.SetActive (true);
	}

	public void showWin(){
		deadSchreen.SetActive (true);
		tytle.color = Color.yellow;
		tytle.text = "--You Win--";
	}

	public void reloadScene(){
		HiveMind.clearData ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
	public void loadMainMenu(){
		Time.timeScale = 1;
		HiveMind.clearData ();
		SceneManager.LoadScene (0);
	}

	public void setPause(bool isPaused){
		pausedSchreen.SetActive (isPaused);
	}
}
