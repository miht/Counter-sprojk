using UnityEngine;
using System.Collections;

public class BaseWeaponClass : MonoBehaviour {

	public string name;
	public int damage;

	public Transform bullet;
	public float bulletSpeed = 100f;
	public float fireRate = 3f;

	public float reloadTime = 3f;

	public int chamberCapacity = 7;
	public int magazineCapacity = 21;

	private float counter = Time.deltaTime;

	AudioSource as_shooting1;
	AudioSource as_shooting2;
	AudioSource as_reloading;

	public AudioClip ac_shooting1;
	public AudioClip ac_shooting2;
	public AudioClip ac_reloading;

	void Awake() {
		as_shooting1 = AddAudio(ac_shooting1, false, false, 0.2f);
		as_shooting2 = AddAudio(ac_shooting2, false, false, 0.2f);
		as_reloading = AddAudio(ac_reloading, false, false, 0.2f);
	}

	// Use this for initialization
	void Start () {
		counter = fireRate;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Fire(Vector3 muzzlePos) {

		counter += Time.deltaTime;
		if(fireRate <= counter) {
			Vector3 spawnPos = muzzlePos;
			Vector3 tar = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 dir = (new Vector3(tar.x, tar.y, spawnPos.z) - spawnPos).normalized;
				
			float angle = AngleBetweenTwoPoints (spawnPos, tar);

			Transform gr = Instantiate(bullet, spawnPos, Quaternion.Euler (new Vector3 (0f, 0f, angle - 90))) as Transform;
			gr.GetComponent<Rigidbody2D> ().AddForce (dir * bulletSpeed); 
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

	AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

}