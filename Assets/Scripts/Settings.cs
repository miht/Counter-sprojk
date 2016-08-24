using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;

		//Ignore collision between bullets
		Physics2D.IgnoreLayerCollision(9, 9, true);
		//Ignore collision between bullets and weapons
		Physics2D.IgnoreLayerCollision(9, 10, true);
		//Ignore collision between weapons and the default layer
		Physics2D.IgnoreLayerCollision (0, 10, true);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
