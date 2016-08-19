using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	GameObject player;
	public float followDistance = 15f;
	public float maxDistance = 8f;
	public float camSpeed = 10f;


	bool followingPlayer = true;


	Vector2 playPos;
	Vector2 camPos;
	Vector2 mousePos;

	Quaternion rotation;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Awake() {
		rotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {

		if(followingPlayer) {
			FollowPlayer ();
		}
		else {
			FollowMouse ();
		}
	}

	void FollowPlayer() {
		Vector3 tar = player.transform.position;
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(tar.x, tar.y, transform.position.z), camSpeed * Time.deltaTime);
	}

	void FollowMouse() {
		Vector3 tar = new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * camSpeed * 6, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * camSpeed * 6, 0f);
		transform.position = Vector3.MoveTowards (transform.position, transform.position + tar, Time.deltaTime * camSpeed * 5);

		 tar = transform.position - player.transform.position;
		Vector2 tar2 = Vector2.ClampMagnitude (tar, maxDistance);
		transform.position = new Vector3 (tar2.x + player.transform.position.x, tar2.y + player.transform.position.y, transform.position.z);
	}

	float CameraPlayerDistance() {
		return Vector2.Distance (player.transform.position, transform.position);
	}

	float MousePlayerDistance() {
		return Vector2.Distance (Camera.main.ScreenToWorldPoint (Input.mousePosition), transform.position);
	}

	void LateUpdate() {
		transform.rotation = rotation;
	}

	public void SetFollowingPlayer(bool val) {
		followingPlayer = val;
	}
}
