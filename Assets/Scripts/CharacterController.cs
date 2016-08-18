using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float speed = 0f;
	public float jumpForce = 0f;

	private float movex = 0f;
	private float movey = 0f;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {
		GetInput ();
	}

	bool Looking() {
		return Input.GetKey ("left shift");
	}

	void GetInput() {
		movex = Input.GetAxis ("Horizontal");
		movey = Input.GetAxis ("Vertical");
		Vector2 move = new Vector2 (movex, movey).normalized * Time.deltaTime * speed;

		if (!Looking()) {
			transform.Translate(move, Space.World);
		}

		print (rb.velocity);
		Camera.main.GetComponent<CameraFollowPlayer> ().SetFollowingPlayer (!Looking ());
	}
}
