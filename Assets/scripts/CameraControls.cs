using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Camera>().orthographicSize += Input.GetAxis("Mouse ScrollWheel");
	}
}
