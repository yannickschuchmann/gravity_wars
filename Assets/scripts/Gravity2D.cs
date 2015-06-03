using UnityEngine;
using System.Collections;

public class Gravity2D : MonoBehaviour {

	public GameObject WorldObjectsHolder;
	public float GravityForce = 2.0f;
	public float GravityDistance = 5.0f;
	public Material aeroMaterial;

	// Use this for initialization
	void Start () {
		Renderer renderer = gameObject.GetComponent<Renderer>();
		Collider2D collider = gameObject.GetComponent<Collider2D>();

		float radius = GravityDistance * 2;

		GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		cylinder.transform.parent = transform;
		cylinder.transform.position = new Vector3(
			transform.position.x + collider.offset.x, transform.position.y + collider.offset.y, 0.1f);
		cylinder.transform.rotation *= Quaternion.Euler(90, 0, 0);
		cylinder.transform.localScale = new Vector3(radius, 0.01f, radius);
		cylinder.GetComponent<Renderer>().material = aeroMaterial;
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		this.IterateObjects(WorldObjectsHolder.transform);
	}

	void IterateObjects(Transform objsHolder) {
		foreach (Transform child in objsHolder.transform) {
			if (child.GetComponent<Rigidbody2D>()) this.PerformGravity(child.gameObject);
			this.IterateObjects(child.transform);
		}
	}


	void PerformGravity(GameObject obj) {
		Vector2 direction = transform.position - obj.transform.position;
		if (direction.magnitude > GravityDistance) {
			return;
		}
		Rigidbody2D objRigidbody2D = obj.GetComponent<Rigidbody2D>();
		
		float force = GetComponent<Rigidbody2D>().mass * objRigidbody2D.mass /
			Mathf.Pow(direction.magnitude, 2);

		if (direction != Vector2.zero) {
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
			obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}

		objRigidbody2D.AddForce(direction * force);
	}
}
