using UnityEngine;
using System.Collections;

public class BasePistol: BaseGun
{
	// Use this for initialization
	void Start ()
	{
		GunType = GunTypes.SEMI_AUTOMATIC;
		InitializeGun ();
	}

	// Update is called once per frame
	void Update ()
	{
		GunCheck ();
		FireCheck ();
	}

	public override void Fire (Vector3 muzzlePos)
	{
		if (!Reloading && FireReady) {
			//Semi-automatic guns differ from automatic ones because these cannot be "sprayed" with. You have to click to shoot, and may
			//not hold down the key to shoot several shots
			if(Input.GetMouseButtonDown(0)) {
				print ("Howdie!");
				Shoot (muzzlePos);
			}
		} else {
			print ("Reloading or is not ready for fire!");
		}
	}
}
