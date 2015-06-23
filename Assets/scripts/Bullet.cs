using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float LifeTime = 10f;

	public GameObject spawnedFrom;

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

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject == spawnedFrom) return;

		OnCollision(coll);
	}

	void OnCollision(Collider2D coll) {
		Destroy (this.gameObject);

		if (coll.gameObject.tag == "Player") {
			coll.gameObject.SendMessage("onDamage", 100);
		}
	}
}
