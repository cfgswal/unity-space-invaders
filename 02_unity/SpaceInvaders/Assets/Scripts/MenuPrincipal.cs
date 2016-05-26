using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}


	void Update (){
	
	}


	public void jugadoruno(){

		SceneManager.LoadScene (1);
	
	}

	public void jugadordos(){

		SceneManager.LoadScene (2);

	}

	public void menuinicio(){
	
		SceneManager.LoadScene (0);
	}

	public void salir(){

		Application.Quit();
	}



}