using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatController : MonoBehaviour {

	public Transform weapon;

	public Transform grenade_explosive;
	public Transform knife;

	public int grenade_expl_count = 1;
	public Text grenade_expl_label;
	public float throwForce = 5f;
	public float throwForceMax = 2000f;

	float missrateCounter = 0f;

	public Text ammo_tot;
	public Text ammo_mag;

	public Text txt_out_of_nades;
	bool shooting = false;

	//The "out of grenades" sound
	public AudioClip ac_out_of_nades;
	AudioSource as_out_of_nades;

	// Use this for initialization
	void Start () {
		weapon = GetComponent<Inventory> ().inventory [0];
		as_out_of_nades = AddAudio(ac_out_of_nades, false, false, 0.2f);
		grenade_expl_label.text = grenade_expl_count.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if(weapon != null) {
			GetInput ();
		}
	}

	void GetInput() {
		shooting = Input.GetMouseButton (0);
		if(shooting) {
			if(weapon.tag == "Grenade") {
				if(Input.GetMouseButtonDown(0)) {
					ThrowGrenade ();
					//Show animation for throwing grenade here
				}
			}
			else {
				Fire ();
				GetComponent<Animator> ().CrossFade ("Shooting", 0f);
			}
		}
		else {
			missrateCounter = 0f;
			if(weapon.tag != "Grenade") {
				//weapon.GetComponent<BaseWeaponClass>().ResetCounter();
			}

		}
		if(Input.GetKeyDown("r")) {
			weapon.GetComponent<BaseWeapon> ().PrepareReload ();
		}
	}

	void ThrowGrenade() {
		if(grenade_expl_count > 0) {
			ThrowGrenade (grenade_explosive);
			grenade_expl_count--;
			grenade_expl_label.text = grenade_expl_count.ToString();
			if(grenade_expl_count <= 0) {
				as_out_of_nades.Play ();
				txt_out_of_nades.GetComponent<BaseAlertClass> ().StartShowing ();
				weapon = null;
				GetComponent<Inventory> ().ClearCurrentCell ();
			}
		}
	}

	//Fire the weapon that is currently equipped. Pass the current location of the weapons muzzle as well as a spray duration counter.
	void Fire() {
		weapon.GetComponent<BaseWeapon> ().Fire (transform.Find ("Muzzle").transform.position);
		//weapon.GetComponent<BaseWeaponClass>().Fire(transform.Find ("Muzzle").transform.position, missrateCounter++);
	}

	void ThrowGrenade(Object gren) {
		Vector3 spawnPos = transform.Find ("Muzzle").transform.position;
		Vector3 tar = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 dir = new Vector3(tar.x, tar.y, spawnPos.z) - spawnPos;

		Transform gr = Instantiate(gren, spawnPos, new Quaternion(0f, 0f, 0f, 0f)) as Transform;
		gr.GetComponent<Rigidbody2D> ().AddForce (dir * throwForce * 10f); 
	}

	protected AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	public bool IsArmed() {
		return weapon != null;
	}
}
