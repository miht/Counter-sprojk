using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public float speed = 0f;
	public float jumpForce = 0f;

	private float movex = 0f;
	private float movey = 0f;

	private Rigidbody2D rb;
	private CapsuleCollider cc;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CapsuleCollider> ();
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
			rb.velocity = move;
		}
		else {
			rb.velocity = new Vector2 (0f, 0f);
		}
		Camera.main.GetComponent<CameraFollowPlayer> ().SetFollowingPlayer (!Looking ());
	}
}
