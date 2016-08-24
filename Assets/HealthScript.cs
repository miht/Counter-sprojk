using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int maxHealth = 100;
	public int health = 100;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0) {
			Die ();
		}
	}

	public void ChangeHealth(int hp) {
		health += hp;
	}

	void Die() {
		transform.Rotate (new Vector3 (0f, 0f, 180f));
		GetComponent<Rigidbody2D> ().isKinematic = true;
		GetComponent<Collider2D> ().enabled = false;
		anim.CrossFade ("NPC_dead", 0f);

		GetComponent<HealthScript> ().enabled = false;
	}
}
