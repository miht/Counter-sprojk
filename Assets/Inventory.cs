using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {

	public Transform[] inventory = new Transform[5];
	public float switchItemsDelay = 0.3f;
	public float pickUpRadius = 2f;

	public Image weaponDisplayImage;

	CombatController cc;
	float counter = 0f;
	int currentSlot = 0;

	public AudioClip ac_switchItem;
	public AudioClip ac_dropItem;
	public AudioClip ac_pickUpItem;

	public Transform flashlight;

	AudioSource as_switchItem;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CombatController> ();
		as_switchItem = AddAudio(ac_switchItem, false, false, 0.2f);
		counter = switchItemsDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) {
			if(counter >= switchItemsDelay) {
				if(Input.GetKeyDown("1")) {
					counter = 0f;
					currentSlot = 0;
				}
				if(Input.GetKeyDown("2")) {
					counter = 0f;
					currentSlot = 1;
				}
				if(Input.GetKeyDown("3")) {
					counter = 0f;
					currentSlot = 2;
				}
				if(Input.GetKeyDown("4")) {
					counter = 0f;
					currentSlot = 3;
				}
				if(Input.GetKeyDown("g")) {
					counter = 0f;
					DropOrPickUp ();
				}
			}
			if(Input.GetKeyDown("f")) {
				flashlight.GetComponent<FlashLightAutomation> ().Press ();
			}

		}
		if (counter == 0f) {
			CombatController cc = GetComponent<CombatController> ();
			cc.weapon = inventory [currentSlot];

			if(cc.IsArmed()) {
				weaponDisplayImage.enabled = true;
				weaponDisplayImage.sprite = cc.weapon.GetComponent<SpriteRenderer> ().sprite;
				if(cc.weapon.tag != "Grenade") {
					cc.weapon.GetComponent<BaseWeapon> ().Deploy ();
				}
					
			}
			else {
				weaponDisplayImage.enabled = false;
			}
			as_switchItem.Play ();

		}
		counter += Time.deltaTime;
	}

	public void ClearCurrentCell() {
		inventory [currentSlot] = null;
		weaponDisplayImage.enabled = false;
	}

	protected AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource> ();
		newAudio.clip = clip; 
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	void DropOrPickUp() {
		Collider2D[] weaponsNearby = Physics2D.OverlapCircleAll (transform.position, pickUpRadius);
		Collider2D weaponNearby = null;
		foreach(Collider2D col in weaponsNearby) {
			if (col.tag == "Weapon") {
				weaponNearby = col;
				break;
			}
		}	
		DropWeapon ();

		if(weaponNearby != null) {
			PickUpWeapon (weaponNearby.transform);
		}
	}

	void PickUpWeapon(Transform newWeapon) {
		print (newWeapon.ToString());

		newWeapon.GetComponent<SpriteRenderer> ().enabled = false;
		newWeapon.GetComponent<Collider2D> ().enabled = false;

		inventory [currentSlot] = newWeapon;
		cc.weapon = newWeapon;
		weaponDisplayImage.enabled = true;
		weaponDisplayImage.sprite = newWeapon.GetComponent<SpriteRenderer> ().sprite;
		cc.weapon.GetComponent<BaseWeapon> ().Deploy ();
	}

	void DropWeapon() {
		if(inventory[currentSlot] != null) {

			inventory [currentSlot].transform.position = new Vector3 (transform.position.x, transform.position.y, inventory [currentSlot].transform.position.z);
			inventory[currentSlot].GetComponent<SpriteRenderer> ().enabled = true;
			inventory[currentSlot].GetComponent<Collider2D> ().enabled = true;
			ClearCurrentCell ();
		}

	}

}
