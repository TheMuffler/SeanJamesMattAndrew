using UnityEngine;
using System.Collections;

public class GameSceneCameraLogic : MonoBehaviour {

	public Transform focused = null;
	public float camSpeed = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f) {
			focused = null;
			transform.position += Vector3.right*h*Time.fixedDeltaTime*camSpeed;
			transform.position += Vector3.forward*v*Time.fixedDeltaTime*camSpeed;
		}

		if (focused != null) {
			transform.position = Vector3.Lerp(transform.position,focused.position-transform.forward*10,Time.fixedDeltaTime*camSpeed);
		}
	}
}
