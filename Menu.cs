using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public Button start;
	public Button quit;
	public Button MENU;

	void Start () {
		start = start.GetComponent<Button> ();
		quit = quit.GetComponent<Button> ();
		MENU = MENU.GetComponent<Button> ();
	}
	


	public void pressStart(){
		SceneManager.LoadScene(1);
	}

	public void pressQuit(){
		Application.Quit();
	}

	public void pressMenu(){
		SceneManager.LoadScene (0);
	}
}
