using UnityEngine;
using System.Collections;

public class BaseShotgun: BaseGun
{

	//The number of shards in one burst
	public int burstSize = 4;

	//The audio clip for when a shot is fired and the gun is pumped
	public AudioClip ac_pump;
	AudioSource as_pump;

	// Use this for initialization
	void Start ()
	{
		GunType = GunTypes.SEMI_AUTOMATIC;
		InitializeGun ();
		as_pump = AddAudio (ac_pump, false, false, 0.2f);
		AS_Pump.pitch = 1.5f;
	}

	// Update is called once per frame
	void Update ()
	{
		GunCheck ();
		FireCheck ();
	}

	public int BurstSize {
		get{return burstSize;}
		set{burstSize = value;}
	}
	public AudioSource AS_Pump {
		get{return as_pump;}
		set{as_pump = value;}
	}

	public override void Fire (Vector3 muzzlePos)
	{
		if (!Reloading && FireReady) {
			if(Input.GetMouseButtonDown(0)) {
				print ("Howdie!");
				Shoot (muzzlePos);
			}
		} else {
			print ("Reloading or is not ready for fire!");
		}
	}

	public override void Shoot(Vector3 muzzlePos) {
		if(Mag_Count > 0) {
			ShootShell (muzzlePos);
		}

		else {
			PrepareReload ();
		}
	}

	public void ShootShell(Vector3 muzzlePos) {
		Vector3 spawnPos = muzzlePos;

		for (int i = 0; i < BurstSize; i++){
			//Calculate the scattering angle of the shot
			float offsetConstant = ShotOffset;
			float offsetX = Random.Range (-offsetConstant, offsetConstant);
			float offsetY = Random.Range (-offsetConstant, offsetConstant);

			Vector3 offsetVector = new Vector3 (offsetX, offsetY, 0f);

			Vector3 tar = Camera.main.ScreenToWorldPoint (Input.mousePosition) + offsetVector;
			Vector3 dir = (new Vector3(tar.x, tar.y, spawnPos.z) - spawnPos).normalized;

			float angle = AngleBetweenTwoPoints (spawnPos, tar);

			Transform gr = Instantiate(bullet, spawnPos, Quaternion.Euler (new Vector3 (0f, 0f, angle - 90))) as Transform;

			gr.GetComponent<Rigidbody2D> ().AddForce (dir * Force); 
			//The damage per bullet is the total damage divided by the amount of shards in one burst.
			//The damage is thus maximized should the target be hit by all of the shards.
			gr.GetComponent<BulletAutomation> ().SetDamage (Mathf.RoundToInt(damage / burstSize));
		}

		Mag_Count--;
		UpdateMagLabel ();
		TimeBetweenShotsCounter = 0f;
		FireReady = false;

		print (AS_Fire.clip);
		AS_Fire.Play ();
	}

	public override void FireCheck() {
		print ("Shotgun fired!");
		if(!FireReady) {
			TimeBetweenShotsCounter += Time.deltaTime;
			if(TimeBetweenShotsCounter >= TimeBetweenShots) {
				FireReady = true;
				AS_Pump.Play ();
			}
		}
	}
}
