using UnityEngine;
using System.Collections;

public class CrosshairController : MonoBehaviour {

	public float crosshairDistance = 5f;

	GameObject player;
	Vector3 startPos;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {



	}

	void LateUpdate() {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos = new Vector3 (mousePos.x, mousePos.y, -2f);
		transform.position = mousePos;
	}
}
