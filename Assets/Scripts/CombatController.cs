using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatController : MonoBehaviour {

	public Transform grenade_explosive;
	public Transform weapon;

	public int grenade_expl_count = 1;
	public float throwForce = 5f;
	public float throwForceMax = 2000f;

	public Text ammo_tot;
	public Text ammo_mag;


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
			weapon.GetComponent<BaseWeaponClass>().ResetCounter();
			if(Input.GetKeyDown("4")) {
				if(grenade_expl_count > 0) {
					ThrowGrenade (grenade_explosive);
					grenade_expl_count--;
				}
			}
		}
	}

	void Fire() {
		weapon.GetComponent<BaseWeaponClass>().Fire(transform.Find ("Muzzle").transform.position);
	}

	void ThrowGrenade(Object gren) {
		Vector3 spawnPos = transform.Find ("Muzzle").transform.position;
		Vector3 tar = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 dir = new Vector3(tar.x, tar.y, spawnPos.z) - spawnPos;

		Transform gr = Instantiate(gren, spawnPos, new Quaternion(0f, 0f, 0f, 0f)) as Transform;
		gr.GetComponent<Rigidbody2D> ().AddForce (dir * throwForce * 10f); 
		print (dir);
	}
}
