using UnityEngine;
using System.Collections;

public class GrenadeAutomation : MonoBehaviour {

	public float timer = 3f;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		Invoke ("Detonate", timer);
	}

	// Update is called once per frame
	void Update () {
		rb.velocity = rb.velocity * 0.98f;
	}

	void Detonate() {
		print ("Boom!");
		Destroy (gameObject);
	}
}
