using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ControlAlienDisparo : MonoBehaviour {
	
	// Fuerza de lanzamiento del disparo
	private float fuerza = 0.2f;

	// Acceso al prefab del disparo
	public Rigidbody2D disparo;
	// Conexión al marcador, para poder actualizarlo
	private GameObject marcador;

	// Por defecto, 100 puntos por cada alien
	private int puntos = 100;

	// Objeto para reproducir la explosión de un alien
	private GameObject efectoExplosion;

	// Use this for initialization
	void Start ()
	{
		// Localizamos el objeto que contiene el marcador
		marcador = GameObject.Find ("Marcador");


	}

	// Update is called once per frame
	void Update ()
	{
		// Objeto para reproducir la explosión de un alien
		efectoExplosion = GameObject.Find ("EfectoExplosion");
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		// Detectar la colisión entre el alien y otros elementos

		// Necesitamos saber contra qué hemos chocado
		if (coll.gameObject.tag == "Disparo" || coll.gameObject.tag == "Disparo2") {

			// Sonido de explosión
			GetComponent<AudioSource> ().Play ();

			// Sumar la puntuación al marcador
			marcador.GetComponent<ControlMarcador> ().puntos += puntos;

			// El disparo desaparece (cuidado, si tiene eventos no se ejecutan)
			Destroy (coll.gameObject);

			// El alien desaparece (no hace falta retraso para la explosión, está en otro objeto)
			efectoExplosion.GetComponent<AudioSource> ().Play ();
			Destroy (gameObject);

		} else if (coll.gameObject.tag == "Nave") {
			SceneManager.LoadScene (3);
		}
	}
	void disparar ()
	{
		// Hacemos copias del prefab del disparo y las lanzamos
		Rigidbody2D d = (Rigidbody2D)Instantiate (disparo, transform.position, transform.rotation);


		// Desactivar la gravedad para este objeto, si no, ¡se cae!
		d.gravityScale = 0;


		// Posición de partida, en la punta de la nave
		d.transform.Translate (Vector2.up * 5.65f);
		d.transform.Translate (Vector2.left * 0.25f);

	
		// Lanzarlo
		d.AddForce (Vector2.down * fuerza, ForceMode2D.Impulse);	
			
	}
}
