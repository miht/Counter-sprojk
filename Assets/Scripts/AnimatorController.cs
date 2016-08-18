using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		SetAnimation ();
	}

	void SetAnimation() {
		anim.SetBool ("Moving", Mathf.Abs(Input.GetAxis ("Horizontal")) + Mathf.Abs(Input.GetAxis ("Vertical")) > 0.01);
	}
}
