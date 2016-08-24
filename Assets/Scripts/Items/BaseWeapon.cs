using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseWeapon : MonoBehaviour {

	public string name;
	public int weaponID;
	public int damage;
	public Sprite sprite;

	//The label displaying the number of bullets left in the magazine
	public Text txt_mag_count;
	//The label displaying the number of bullets left in the backpack
	public Text txt_total_count;
	//The image serving as a loading bar for reloading the weapon
	public Image img_reloading_bar;

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

	public void InitializeWeapon() {
		SpriteRenderer = GetComponent<SpriteRenderer> ();
		AS_Deployed = AddAudio (ac_deployed, false, false, 0.2f);
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
		AS_Deployed.Play ();
	}
	public Text Txt_Mag_Count {
		get{return txt_mag_count;}
		set{txt_mag_count = value;}
	}
	public Text Txt_Total_Count {
		get{return txt_total_count;}
		set{txt_total_count = value;}
	}
	public Image Img_Reloading_Bar {
		get{return img_reloading_bar;}
		set{img_reloading_bar = value;}
	}

	public SpriteRenderer SpriteRenderer {
		get{return sr;}
		set{sr = value;}
	}
	//Handle the reloading mechanism here
	public virtual void PrepareReload() {
		
	}

	//Handle the firing mechanism in this method
	public virtual void Fire(Vector3 muzzlePos) {
	}

	//Stop all processes within the weapon, such as reloading
	public virtual void Stop() {
		
	}

	public virtual void UpdateAllLabels() {
		Txt_Mag_Count.text = "";
		Txt_Total_Count.text = "";
	}

	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	public float AngleBetweenTwoPoints (Vector3 a, Vector3 b)
	{
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
}
