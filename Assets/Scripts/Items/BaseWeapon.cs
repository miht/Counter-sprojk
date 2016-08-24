using UnityEngine;
using System.Collections;

public class BaseWeapon : MonoBehaviour {

	public string name;
	public int weaponID;
	public int damage;
	public Sprite sprite;
	public AudioClip ac_deployed;

	AudioSource as_deployed;
	SpriteRenderer sr;

	public enum WeaponTypes {
		UNARMED, 
		KNIFE, 
		GUN, 
		GRENADE, 
		MISC
	}

	private WeaponTypes weaponType;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}

	public void Initialize() {
		SpriteRenderer = GetComponent<SpriteRenderer> ();
		as_deployed = AddAudio (ac_deployed, false, false, 0.2f);
		SpriteRenderer.sprite = sprite;
	}

	public string WeaponName {
		get{return name;}
		set{name = value;}
	}
	public int WeaponID {
		get{return weaponID;}
		set{weaponID = value;}
	}
	public int WeaponDamage {
		get{return damage;}
		set{damage = value;}
	}
	public Sprite WeaponSprite {
		get{return sprite;}
		set{sprite = value;}
	}
	public WeaponTypes WeaponType {
		get{return weaponType;}
		set{weaponType = value;}
	}
	public AudioSource AS_Deployed{
		get{return as_deployed;}
		set{as_deployed = value;}
	}
	public void Deploy() {
		as_deployed.Play ();
	}

	public SpriteRenderer SpriteRenderer {
		get{return sr;}
		set{sr = value;}
	}

	//Handle the firing mechanism in this method
	public virtual void Fire() {
	}

	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}
}
