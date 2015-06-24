using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera cam = GetComponent<Camera>();
		float zoom = Mathf.Max(3, Mathf.Min(12, cam.orthographicSize + Input.GetAxis("Mouse ScrollWheel")));
		cam.orthographicSize = zoom;
	}
}
