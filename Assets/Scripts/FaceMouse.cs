using UnityEngine;
using System.Collections;

public class FaceMouse : MonoBehaviour {

	public float angleOffset = 0f;

	Vector3 mousePos;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		RotateCamera ();
	}

	void RotateCamera() {
		mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));	
		rb.transform.eulerAngles = new Vector3 (0, 0, angleOffset + Mathf.Atan2 ((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg);
	}
}
