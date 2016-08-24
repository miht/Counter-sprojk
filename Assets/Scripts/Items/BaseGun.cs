using UnityEngine;
using System.Collections;

public class BaseGun : BaseWeapon {

	//The bullet as well as the force of the bullet
	public Transform bullet;
	//How much the shot should deviate from the target's position
	public float shotOffset = 0f;
	//How fast the bullet will travel
	public float force;

	//The number of bullets left in the magazine
	public int mag_count;
	//The total capacity of the magazine
	public int mag_capacity;
	//The number of bullets left in the backpack
	public int total_count; 
	//The total capacity of the backpack
	public int total_capacity;

	//The time it takes to reload
	public float reloadTime;

	//Time time between each shot
	public float timeBetweenShots;

	//Whether this gun is ready to shoot or not, and the counter for the time between each shots
	bool fireReady = true;
	float timeBetweenShotsCounter = 0f;

	public AudioClip ac_fire;
	AudioSource as_fire;

	public AudioClip ac_reloadStart;
	AudioSource as_reloadStart;

	public AudioClip ac_reloadEnd;
	AudioSource as_reloadEnd;

	//Whether or not this weapon is currently reloading, as well as the counter for the reload time
	bool reloading = false;
	float reloadCounter = 0f;

	public enum GunTypes {
		AUTOMATIC,
		SEMI_AUTOMATIC
	}

	private GunTypes gunType;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void InitializeGun() {
		WeaponType = WeaponTypes.GUN;
		timeBetweenShotsCounter = TimeBetweenShots;
		reloadCounter = reloadTime;
		InitializeWeapon ();
		AS_Fire = AddAudio (ac_fire, false, false, 0.2f);
		AS_ReloadStart = AddAudio (ac_reloadStart, false, false, 0.2f);
		AS_ReloadEnd = AddAudio (ac_reloadEnd, false, false, 0.2f);

		UpdateAllLabels ();
	}

	public virtual void Shoot (Vector3 muzzlePos)
	{
		if(Mag_Count > 0) {
			ShootBullet (muzzlePos);
		}
		else {
			PrepareReload ();
		}


	}

	public void ShootBullet(Vector3 muzzlePos) {
		Vector3 spawnPos = muzzlePos;

		//Calculate the scattering angle of the shot
		float offsetConstant = shotOffset * 1f / 30f;
		float offsetX = Random.Range (-offsetConstant, offsetConstant);
		float offsetY = Random.Range (-offsetConstant, offsetConstant);

		Vector3 offsetVector = new Vector3 (offsetX, offsetY, 0f);

		Vector3 tar = Camera.main.ScreenToWorldPoint (Input.mousePosition) + offsetVector;
		Vector3 dir = (new Vector3 (tar.x, tar.y, spawnPos.z) - spawnPos).normalized;

		float angle = AngleBetweenTwoPoints (spawnPos, tar);

		Transform gr = Instantiate (bullet, spawnPos, Quaternion.Euler (new Vector3 (0f, 0f, angle - 90))) as Transform;

		gr.GetComponent<Rigidbody2D> ().AddForce (dir * Force); 
		gr.GetComponent<BulletAutomation> ().SetDamage (damage);

		Mag_Count--;
		UpdateMagLabel ();
		TimeBetweenShotsCounter = 0f;
		FireReady = false;

		print (AS_Fire.clip);
		AS_Fire.Play ();
	}

	public override void Stop() {
		Reloading = false;
		ReloadCounter = ReloadTime;
//		FireReady = true;
//		TimeBetweenShotsCounter = TimeBetweenShots;
		Img_Reloading_Bar.transform.localScale = new Vector3 (0f, 1f, 1f);
	}

	public Transform Bullet {
		get{return bullet;}
		set{bullet = value;}
	}
	public float Force {
		get{return force;}
		set{force = value;}
	}
	public float ShotOffset {
		get{return shotOffset;}
		set{shotOffset = value;}
	}
	public int Mag_Count {
		get{return mag_count;}
		set{mag_count = value;}
	}
	public int Mag_Capacity{
		get{return mag_capacity;}
		set{mag_capacity = value;}
	}
	public int Total_Count {
		get{return total_count;}
		set{total_count = value;}
	}
	public int Total_Capacity{
		get{return total_capacity;}
		set{total_capacity = value;}
	}
	public GunTypes GunType{
		get{return gunType;}
		set{gunType = value;}
	}
	public AudioSource AS_Fire {
		get{return as_fire;}
		set{as_fire = value;}
	}
	public AudioSource AS_ReloadStart {
		get{return as_reloadStart;}
		set{as_reloadStart = value;}
	}
	public AudioSource AS_ReloadEnd {
		get{return as_reloadEnd;}
		set{as_reloadEnd = value;}
	}
	public bool Reloading{
		get{return reloading;}
		set{reloading = value;}
	}
	public float ReloadTime{
		get{return reloadTime;}
		set{reloadTime = value;}
	}
	public float ReloadCounter{
		get{return reloadCounter;}
		set{reloadCounter = value;}
	}
	public float TimeBetweenShots {
		get{return timeBetweenShots;}
		set{timeBetweenShots = value;}
	}
	public float TimeBetweenShotsCounter {
		get{return timeBetweenShotsCounter;}
		set{timeBetweenShotsCounter = value;}
	}
	public bool FireReady {
		get{return fireReady;}
		set{fireReady = value;}
	}
	public bool HasMagAmmunition() {
		return Mag_Count != 0;
	}
	public bool HasTotalAmmunition() {
		return Total_Count != 0;
	}

	public override void PrepareReload() {
		if(!Reloading) {
			if(HasTotalAmmunition()) {
				if(Mag_Count == Mag_Capacity) {
					print("This magazine is already full");
				}
				else {
					ReloadStart ();
				}
			}
			else {
				print ("No bullets left in the backpack.");
			}
		}
		else {
			print ("Already reloading");
		}
	}

	//Check whether the counters need to be incremented or not
	public void GunCheck() {
		if(Reloading) {
			//Adjust the reloading bar
			Img_Reloading_Bar.transform.localScale = new Vector3 (ReloadCounter / ReloadTime, 1f, 1f);
			ReloadCounter += Time.deltaTime;
			if(ReloadCounter >= ReloadTime) {
				//Reloading may have been interrupted. If it has, do not finish the reload
				if(Reloading) {
					ReloadEnd ();
					//Reset the reloading bar
					Img_Reloading_Bar.transform.localScale = new Vector3 (0f, 1f, 1f);
				}

			}
		}
		else {
//			//If magazine count is zere AND if the current weapon is equipped, prepare reload
//			if(Mag_Count == 0) {
//				if(GameObject.FindGameObjectWithTag("Player").GetComponent<CombatController>().weapon == transform) {
//					PrepareReload ();
//				}
//				else {
//					Stop ();
//				}
//			}
		}
	}

	public virtual void FireCheck() {
		print ("Hola!");
		if(!FireReady) {
			TimeBetweenShotsCounter += Time.deltaTime;
			if(TimeBetweenShotsCounter >= TimeBetweenShots) {
				FireReady = true;
			}
		}
	}

	public void ReloadStart() {
		AS_ReloadStart.Play ();
		Reloading = true;
		reloadCounter = 0f;
	}

	public void ReloadEnd() {
		//The number of bullets to take from the backpack and insert into the magazine
		int numOfBullets = Mag_Capacity - Mag_Count;
		//See if the backpack has enough bullets. If not, just load all of the backpacks bullets into the magazine
		if(Total_Count >= numOfBullets) {
			Total_Count -= numOfBullets;
			Mag_Count += numOfBullets;
		}
		else {
			Mag_Count += Total_Count;
			Total_Count = 0;
		}
		Reloading = false;
		UpdateAllLabels ();
		AS_ReloadEnd.Play ();
	}

	public void UpdateMagLabel() {
		Txt_Mag_Count.text = Mag_Count.ToString();
	}

	public override void UpdateAllLabels() {
		UpdateMagLabel ();
		Txt_Total_Count.text = Total_Count.ToString();
	}
}
