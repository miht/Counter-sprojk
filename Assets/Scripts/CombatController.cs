using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour {

	public Transform grenade_explosive;
	public int grenade_expl_count = 1;
	public float throwForce = 5f;
	public float throwForceMax = 2000f;

	bool shooting = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
	}

	void GetInput() {
		shooting = Input.GetMouseButton (0);
		if(shooting) {
			Fire ();
		}
		else {
			if(Input.GetKeyDown("4")) {
				if(grenade_expl_count > 0) {
					ThrowGrenade (grenade_explosive);
					grenade_expl_count--;
				}
			}
		}
	}

	void Fire() {
		print ("Bam, bam, bam!");
	}

	void ThrowGrenade(Object gren) {
		Vector3 spawnPos = transform.Find ("Muzzle").transform.position;
		Vector3 tar = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 dir = new Vector3(tar.x, tar.y, spawnPos.z) - transform.position;

		Transform gr = Instantiate(gren, spawnPos, new Quaternion(0f, 0f, 0f, 0f)) as Transform;
		gr.GetComponent<Rigidbody2D> ().AddForce (dir * throwForce * 10f); 
		print (dir);
	}
}
