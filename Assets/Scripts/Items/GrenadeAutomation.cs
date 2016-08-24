using UnityEngine;
using System.Collections;

public class GrenadeAutomation : MonoBehaviour {

	public float timer = 3f;
	public float explosiveRadius = 2f;
	public int damage = 3;

	bool exploding = false;

	AudioSource as_throwing;
	AudioSource as_bouncing;
	AudioSource as_exploding;

	public AudioClip ac_throwing;
	public AudioClip ac_bouncing;
	public AudioClip ac_exploding;

	Rigidbody2D rb;

	void Awake() {
		as_throwing = AddAudio(ac_throwing, false, false, 0.2f);
		as_bouncing = AddAudio(ac_bouncing, false, false, 0.2f);
		as_exploding = AddAudio(ac_exploding, false, false, 0.2f);
	}

	// Use this for initialization
	void Start () {
		as_throwing.Play ();
		rb = GetComponent<Rigidbody2D> ();
		Invoke ("Detonate", timer);
	}

	// Update is called once per frame
	void Update () {
		rb.velocity = rb.velocity * 0.98f;

	}

	void OnCollisionEnter2D(Collision2D coll) {
		as_bouncing.Play ();
	}

	public void Detonate() {
		transform.localScale = new Vector3 (10, 10, 1);
		transform.position += new Vector3 (0f, 0f, -1);

		//ADD FORCE HERE
		//Add a physics circle encirculating the center of the explosion. Every collider inside this circle shall
		//be assigned different 
		foreach(Collider2D c in Physics2D.OverlapCircleAll(transform.position, explosiveRadius)) {
			GameObject coll = c.gameObject;
			if(coll.tag == "NPC") {
				

				Vector3 center = transform.position;
				Vector3 target = new Vector3 (coll.transform.position.x, coll.transform.position.y, transform.position.z);
				float distanceFromGrenade = (target - center).magnitude;
				print (distanceFromGrenade);

				int dmg = Mathf.RoundToInt(damage * 10 / distanceFromGrenade);

				coll.GetComponent<HealthScript> ().ChangeHealth (-dmg);
			}
		}

		//Disable the rigid body
		GetComponent<Collider2D> ().enabled = false;
		StartCoroutine(Explode());
		StartCoroutine (MakeInvisible ());
	}

	IEnumerator MakeInvisible() {
		GetComponent<Animator> ().CrossFade ("HE_explode", 0f);

		//TIME WAITED HARD CODED. CHECK FOR BETTER SOLUTION!
		yield return new WaitForSeconds(1f);
		GetComponent<SpriteRenderer> ().enabled = false;
	}

	IEnumerator Explode() {
		if(!exploding) {
			exploding = true;
			as_exploding.Play ();
			yield return new WaitForSeconds(as_exploding.clip.length);
			Destroy (gameObject);
		}
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
