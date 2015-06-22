using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float LifeTime = 10f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		LifeTime -= Time.deltaTime;

		if (LifeTime <= 0) Destroy(gameObject);

		Vector3 direction = GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
