using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Automata.
/// La primera letra debe ser mayúscula
//Debe poseer al menos 1 caracteres alfanuméricos
//Debe poseer al menos 6 caracteres
//Debe poseer al menos 1 número.
/// </summary>

public class Automata : MonoBehaviour {

	[SerializeField]
	private Text Usuario,pasword,NotificacionDescripcion,textUsuarioReg,textPaswordReg,NotificacionReg;
	[SerializeField]
	GameObject notificacion,panelRegistro,panelUsuario;

	// Use this for initialization
	void Start () {

		this.notificacion.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Cantidads the correcta caracteres.
	/// Debe poseer al menos 6 caracteres
	/// </summary>
	/// <returns><c>true</c>, if correcta caracteres was cantidaded, <c>false</c> otherwise.</returns>
	/// <param name="pasword">Pasword.</param>
	private bool CantidadCorrectaCaracteres(Text pasword){
		string text = pasword.text;
		bool respuesta = false;

		if(text.Length >= 6){
			respuesta = true;
		}

		Debug.Log ("Contiene6Caracteres: " + respuesta);
		return respuesta;
	}
	//Debe poseer al menos 1 número.
	private bool VerificarSiTieneUnNumero(Text pasword){
		string text = pasword.text;
		bool respuesta = false;
		for (int j = 0; j < text.Length; j++) {
			for (int i = 48; i < 58; i++) {

				if (text [j] == (char)i) {
					respuesta = true;
					//Debug.Log ("Letra" + (char)i);
					break;
				}


			}
		}

		Debug.Log ("VerificarSiTieneNumero: " + respuesta);
		return respuesta;
	}

	//Debe poseer al menos 1 caracteres alfanuméricos
	//Usando codigo Ascii
	private bool VerificarCaracterValido(Text pasword){
		string text = pasword.text;
		bool respuesta = false;

		for (int i = 0; i < text.Length; i++) {

			for (int j = 48; j < 123; j++) {
				//Recorrer valores alfanumericos
				//Debug.Log (text[i] + "-" + (char)j +"i="+i);
				if (j == 58) {
					j = 65;
				}else if(j == 91){
					j=97;
				}

				////////////////////////////////
				/// 

				if(text[i] == (char)j){
					respuesta = true;
					Debug.Log ("Letra" + (char)i);
					break;
				}



			}

		}

		Debug.Log ("VerificarCaracterValido 1 caracteres alfanuméricos: " + respuesta);
		return respuesta;
	}

	//La primera letra debe ser mayúscula
	private bool VerificarPrimerLetraMayuscula(Text pasword){
		string text = pasword.text;
		bool respuesta = false;
		if (text != null && !(text.Equals(""))) {


			for (int i = 65; i < 91; i++) {

				if (text [0] == (char)i) {
					respuesta = true;
					//Debug.Log ("Letra" + (char)i);
					break;
				}


			}
		}else {
			Debug.Log ("Cadena vacia");
		}
		Debug.Log ("VerificarPrimerLetraMayuscula: " + respuesta);
		return respuesta;
	}

	public void IngresarCuenta(){

		NotificacionDescripcion.text = "";
		//Variables
		bool primLetraMayusc = false, caracterValido = false, verificarNum = false, cantidadCaracteres = false;
		string mensajeAMostrar = "a\ab";

		if (this.pasword.text != null && !(this.pasword.text.Equals (""))) {
			primLetraMayusc =  VerificarPrimerLetraMayuscula (pasword);
			caracterValido = VerificarCaracterValido (pasword);
			verificarNum = VerificarSiTieneUnNumero(pasword);
			cantidadCaracteres = CantidadCorrectaCaracteres (pasword);
		} else {
			Debug.Log ("Cadena vacia");
		}

		if (!(primLetraMayusc) || !(caracterValido) || !(verificarNum) || !(cantidadCaracteres)) {

			StopCoroutine (MostrarNotificacion ());
			this.notificacion.SetActive (false);
			this.notificacion.GetComponent<Animator> ().enabled = true;
			StartCoroutine (MostrarNotificacion ());
			//Debug.Log ("Se Muestra");
			if (!(primLetraMayusc)) {
				NotificacionDescripcion.text = NotificacionDescripcion.text + "La primera letra debe ser Mayuscula" + "\n"; 
			}
			if (!(caracterValido)) {
				NotificacionDescripcion.text = NotificacionDescripcion.text + "Debe poseer al menos 1 caracteres alfanuméricos" + "\n"; 
			}
			if (!(verificarNum)) {
				NotificacionDescripcion.text = NotificacionDescripcion.text + "Debe poseer al menos 1 número." + "\n"; 
			}
			if (!(cantidadCaracteres)) {
				NotificacionDescripcion.text = NotificacionDescripcion.text + "Debe tener al menos 6 caracteres" + "\n"; 
			}


		} else {

			if (Usuario.text != null && !(Usuario.text.Equals (""))) {

				PlayerPrefs.SetString ("Usuario",Usuario.text);
				PlayerPrefs.SetString ("pasword",pasword.text);

				textUsuarioReg.text = PlayerPrefs.GetString ("Usuario");
				textPaswordReg.text = PlayerPrefs.GetString ("pasword");

				panelUsuario.SetActive (true);
				panelRegistro.SetActive (false);

			} else {
				//NotificacionReg.text = "";
				StopCoroutine (MostrarNotificacion ());
				this.notificacion.SetActive (false);
				this.notificacion.GetComponent<Animator> ().enabled = true;
				StartCoroutine (MostrarNotificacion ());
				NotificacionDescripcion.text = NotificacionDescripcion.text + "Campo usuario vacio" + "\n"; 
			}
		}

	}

	public void Salir(){

		Application.Quit ();
	}

	//Animation
	IEnumerator MostrarNotificacion(){

		this.notificacion.SetActive (true);
		yield return new WaitForSeconds (1.5f);
		this.notificacion.GetComponent<Animator> ().enabled = false;
		yield return new WaitForSeconds (8f);
		this.notificacion.GetComponent<Animator> ().enabled = true;
		//
		yield return new WaitForSeconds (0.5f);
		this.notificacion.SetActive (false);

	}

}
