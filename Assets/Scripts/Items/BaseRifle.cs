using UnityEngine;
using System.Collections;

public class BaseRifle: BaseGun
{

	// Use this for initialization
	void Start ()
	{
		GunType = GunTypes.AUTOMATIC;
		InitializeGun ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GunCheck ();
	}

	public float ShotOffset {
		get{return shotOffset;}
		set{shotOffset = value;}
	}

	public override void Fire (Vector3 muzzlePos)
	{
		if (!Reloading && FireReady) {
			Shoot (muzzlePos);
		} else {
			print ("Reloading or is not ready for fire!");
		}
	}
}
