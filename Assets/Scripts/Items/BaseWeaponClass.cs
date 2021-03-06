﻿using UnityEngine;
using System.Collections;

public class BaseWeaponClass : MonoBehaviour {

	public string name;
	public int damage;
	public Sprite image;

	public Transform bullet;
	public float bulletSpeed = 100f;

	//The speed that of which the weapon will fire
	public float fireRate = 3f;

	//How far from the target that the bullets will spread
	public float missOffset = 1f;

	//The time it takes to reload the weapon
	public float reloadTime = 3f;

	public bool shotgun = false;
	public float pumpTime = 1f;
	public float shellSpread = 15f;
	public int numberOfShellsInBurst = 3;


	public int chamberCapacity = 7;
	public int magazineCapacity = 21;

	protected float counter = 0f;

	AudioSource as_shooting1;
	AudioSource as_shooting2;
	AudioSource as_reloading;
	AudioSource as_pickedUp;

	public AudioClip ac_shooting1;
	public AudioClip ac_shooting2;
	public AudioClip ac_reloading;
	public AudioClip ac_pickedUp;

	void Awake() {
		as_shooting1 = AddAudio(ac_shooting1, false, false, 0.2f);
		as_shooting2 = AddAudio(ac_shooting2, false, false, 0.2f);
		as_reloading = AddAudio(ac_reloading, false, false, 0.2f);
		as_pickedUp = AddAudio (ac_pickedUp, false, false, 0.2f);
	}

	// Use this for initialization
	void Start () {
		counter = Time.deltaTime;
		GetComponent<SpriteRenderer> ().sprite = image;
		counter = fireRate;
	}

	//The method that is called whenever the weapon is picked up
	public void Deploy() {
		as_pickedUp.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Fire(Vector3 muzzlePos, float missrateCounter) {
		if(shotgun) {
			FireShell (muzzlePos, missrateCounter);
		}
		else {
			FireShot (muzzlePos, missrateCounter);
		}
	}


	public void FireShell(Vector3 muzzlePos, float missrateCounter) {
		counter += Time.deltaTime;
		if(fireRate <= counter) {
			Vector3 spawnPos = muzzlePos;

			Vector3[] burstVectors = new Vector3[numberOfShellsInBurst];
			for (int i = 0; i < numberOfShellsInBurst; i++){
				//Calculate the scattering angle of the shot
				float offsetConstant = missOffset;
				float offsetX = Random.Range (-offsetConstant, offsetConstant);
				float offsetY = Random.Range (-offsetConstant, offsetConstant);

				Vector3 offsetVector = new Vector3 (offsetX, offsetY, 0f);

				Vector3 tar = Camera.main.ScreenToWorldPoint (Input.mousePosition) + offsetVector;
				Vector3 dir = (new Vector3(tar.x, tar.y, spawnPos.z) - spawnPos).normalized;

				float angle = AngleBetweenTwoPoints (spawnPos, tar);

				Transform gr = Instantiate(bullet, spawnPos, Quaternion.Euler (new Vector3 (0f, 0f, angle - 90))) as Transform;

				gr.GetComponent<Rigidbody2D> ().AddForce (dir * bulletSpeed); 
				gr.GetComponent<BulletAutomation> ().SetDamage (damage);
				chamberCapacity--;
			}
			counter = 0f;

			as_shooting1.Play ();

		}
	}

	public void FireShot(Vector3 muzzlePos, float missrateCounter) {

		counter += Time.deltaTime;
		if(fireRate <= counter) {
			Vector3 spawnPos = muzzlePos;

			//Calculate the scattering angle of the shot
			float offsetConstant = missOffset * missrateCounter / 30f;
			float offsetX = Random.Range (-offsetConstant, offsetConstant);
			float offsetY = Random.Range (-offsetConstant, offsetConstant);

			Vector3 offsetVector = new Vector3 (offsetX, offsetY, 0f);

			Vector3 tar = Camera.main.ScreenToWorldPoint (Input.mousePosition) + offsetVector;
			Vector3 dir = (new Vector3(tar.x, tar.y, spawnPos.z) - spawnPos).normalized;
				
			float angle = AngleBetweenTwoPoints (spawnPos, tar);

			Transform gr = Instantiate(bullet, spawnPos, Quaternion.Euler (new Vector3 (0f, 0f, angle - 90))) as Transform;

			gr.GetComponent<Rigidbody2D> ().AddForce (dir * bulletSpeed); 
			gr.GetComponent<BulletAutomation> ().SetDamage (damage);
			chamberCapacity--;
			counter = 0f;

			as_shooting1.Play ();

		}
	}
		
	public string GetName() {return name;}
	public int GetDamage() {return damage;}
	public float GetFireRate() {return fireRate;}
	public int GetChamberCapacity() {return chamberCapacity;}
	public int GetMagazineCapacity() {return magazineCapacity;}
	public void ResetCounter() {counter = fireRate;}

	float AngleBetweenTwoPoints (Vector3 a, Vector3 b)
	{
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}

	protected AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

}