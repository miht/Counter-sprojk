using UnityEngine;
using System.Collections;

public class FlashLightAutomation : MonoBehaviour {


	public Transform crosshair;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = new Vector3 (crosshair.position.x, crosshair.position.y, transform.position.z);
		transform.position = newPos;
	}

	public void SetLit(bool val) {
		GetComponent<Light> ().enabled = val;
	}

	public void Press() {
		GetComponent<Light> ().enabled = !GetComponent<Light> ().enabled;
	}
}
