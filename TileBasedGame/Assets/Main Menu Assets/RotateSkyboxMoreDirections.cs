using UnityEngine;
using System.Collections;

public class RotateSkyboxMoreDirections : RotateSkybox {
	

	public float x=0.00f;
	public float y=0.00f;
	public float z=0.0f;

	void Update () {	


		transform.Rotate (Vector3.up, speed * x * Time.deltaTime);
		transform.Rotate (Vector3.left, speed * y * Time.deltaTime);
		transform.Rotate (Vector3.forward, speed * z * Time.deltaTime);
	}
}
