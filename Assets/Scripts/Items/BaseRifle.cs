using UnityEngine;
using System.Collections;

public class BaseRifle: BaseGun {

	// Use this for initialization
	void Start () {
		GunType = GunTypes.AUTOMATIC;
		Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Fire() {
		if(Reloading) {
			print ("Reloading!");
		}
		else {
			print ("Fired!");
		}
	}
}
