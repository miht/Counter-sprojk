using UnityEngine;
using System.Collections;

public class BulletAutomation : MonoBehaviour {

	int damage = 30;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

	}

	public void SetDamage(int dmg) {damage = dmg;}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "NPC") {
			coll.gameObject.GetComponent<HealthScript> ().ChangeHealth (-damage);
		}
		else if(coll.gameObject.tag == "Grenade") {
			coll.gameObject.GetComponent<GrenadeAutomation>().Detonate();
		}
		Destroy (gameObject);
	}
}
