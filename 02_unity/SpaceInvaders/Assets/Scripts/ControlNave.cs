﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlNave : MonoBehaviour
{

	// Velocidad a la que se desplaza la nave (medido en u/s)
	private float velocidad = 20f;

	// Fuerza de lanzamiento del disparo
	private float fuerza = 0.5f;

	// Acceso al prefab del disparo
	public Rigidbody2D disparo;

	// Objeto para reproducir la explosión de un alien
	private GameObject efectoExplosion;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()

	{

		// Objeto para reproducir la explosión de un alien
		efectoExplosion = GameObject.Find ("EfectoExplosion");

		// Calculamos la anchura visible de la cámara en pantalla
		float distanciaHorizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

		// Calculamos el límite izquierdo y el derecho de la pantalla
		float limiteIzq = -1.0f * distanciaHorizontal;
		float limiteDer = 1.0f * distanciaHorizontal;

		// Tecla: Izquierda
		if (Input.GetKey (KeyCode.A)) {

			// Nos movemos a la izquierda hasta llegar al límite para entrar por el otro lado
			if (transform.position.x > limiteIzq) {
				transform.Translate (Vector2.left * velocidad * Time.deltaTime);
			} else {
				transform.position = new Vector2 (limiteDer, transform.position.y);			
			}
		}

		// Tecla: Derecha
		if (Input.GetKey (KeyCode.D)) {

			// Nos movemos a la derecha hasta llegar al límite para entrar por el otro lado
			if (transform.position.x < limiteDer) {
				transform.Translate (Vector2.right * velocidad * Time.deltaTime);
			} else {
				transform.position = new Vector2 (limiteIzq, transform.position.y);			
			}
		}

		// Disparo
		if (Input.GetKeyDown (KeyCode.S)) {
			disparar ();
		}
	}

	void disparar ()
	{
		// Hacemos copias del prefab del disparo y las lanzamos
		Rigidbody2D d = (Rigidbody2D)Instantiate (disparo, transform.position, transform.rotation);
		Rigidbody2D c = (Rigidbody2D)Instantiate (disparo, transform.position, transform.rotation);

		// Desactivar la gravedad para este objeto, si no, ¡se cae!
		d.gravityScale = 0;
		c.gravityScale = 0;

		// Posición de partida, en la punta de la nave
		d.transform.Translate (Vector2.up * 0.65f);
		d.transform.Translate (Vector2.left * 0.25f);

		c.transform.Translate (Vector2.up * 0.65f);
		c.transform.Translate (Vector2.right * 0.25f);
		// Lanzarlo
		d.AddForce (Vector2.up * fuerza, ForceMode2D.Impulse);	
		c.AddForce (Vector2.up * fuerza, ForceMode2D.Impulse);	
	}
	void OnCollisionEnter2D (Collision2D coll)
	{
		// Detectar la colisión entre el nave y otros elementos

		// Necesitamos saber contra qué hemos chocado
		if (coll.gameObject.tag == "Alien") {
			
			// Sonido de explosión
			GetComponent<AudioSource> ().Play ();

			// El alien desaparece (no hace falta retraso para la explosión, está en otro objeto)
			efectoExplosion.GetComponent<AudioSource> ().Play ();
			Destroy (gameObject);
			SceneManager.LoadScene (0);

		} 
	}

}
