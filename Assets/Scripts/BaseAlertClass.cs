using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseAlertClass : MonoBehaviour {

	public float duration = 3f;

	float counter = 0f;
	bool showing = false;

	// Use this for initialization
	void Start () {
		GetComponent<CanvasRenderer> ().SetAlpha (0f);
	}

	public void StartShowing() {
		if(!showing) {
			showing = true;
		}
		GetComponent<CanvasRenderer> ().SetAlpha (1f);
		counter = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(showing) {
			CheckTime ();
		}
	}

	void CheckTime() {
		counter += Time.deltaTime;
		if(counter >= duration) {
			print ("Whaaaat");
			showing = false;
			GetComponent<CanvasRenderer> ().SetAlpha (0f);
			counter = 0f;
		}
		else {
			GetComponent<CanvasRenderer> ().SetAlpha (1 - (counter / duration));
		}
	}
}
