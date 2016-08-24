using UnityEngine;
using System.Collections;

public class BaseGun : BaseWeapon {

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

	public AudioClip ac_fire;

	bool reloading = false;
	float reloadCounter = 0f;

	public enum GunTypes {
		AUTOMATIC,
		SEMI_AUTOMATIC
	}

	private GunTypes gunType;

	// Use this for initialization
	void Start () {
		reloadCounter = reloadTime;
		WeaponType = WeaponTypes.GUN;
	}
	
	// Update is called once per frame
	void Update () {
		if(Reloading) {
			ReloadCounter += Time.deltaTime;
			if(ReloadCounter >= ReloadTime) {
				Reloading = false;
			}
		}
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
	public float ReloadTime{
		get{return reloadTime;}
		set{reloadTime = value;}
	}
	public GunTypes GunType{
		get{return gunType;}
		set{gunType = value;}
	}
	public AudioClip AC_Fire{
		get{return ac_fire;}
		set{ac_fire = value;}
	}
	public bool Reloading{
		get{return reloading;}
		set{reloading = value;}
	}
	public float ReloadCounter{
		get{return reloadCounter;}
		set{reloadCounter = value;}
	}

	public bool HasMagAmmunition() {
		return Mag_Count != 0;
	}
	public bool HasTotalAmmunition() {
		return Total_Count != 0;
	}

	public void PrepareReload() {
		if(HasTotalAmmunition()) {
			if(Mag_Count == Mag_Capacity) {
				print("This magazine is already full");
			}
			else {
				Reloading = true;
				Reload ();
				reloadCounter = 0f;
			}
		}
		else {
			print ("No bullets left in the backpack.");
		}
	}

	public void Reload() {
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
	}
}
