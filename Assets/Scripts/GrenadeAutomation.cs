using UnityEngine;
using System.Collections;

public class GrenadeAutomation : MonoBehaviour {

	public float timer = 3f;

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

	void Detonate() {
		StartCoroutine(Explode());
	}

	IEnumerator Explode() {
		as_exploding.Play ();
		yield return new WaitForSeconds(as_exploding.clip.length);
		print ("Boom!");
		Destroy (gameObject);
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
