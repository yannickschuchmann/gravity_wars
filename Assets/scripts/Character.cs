using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public int hp = 100;
	private bool active = false;

	public float acceleration = 100f;
	public float jumpPower = 200f;

	public float crossHairAcceleration = 1.5f;
	public float crossHairDistance = 2.0f;
	public GameObject crossHairPrefab;
	private GameObject crossHair;
	private float crossHairAngle = 90;

	public GameObject shootPrefab;
	private GameObject trajectory;

	private Rigidbody2D rigid;
	private bool isGrounded;
	private float faceDirection = -1;

	private TurnBased gameController;
	public Player player;

	private Animator animator;

	private float shootLoading = 0;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody2D>();
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<TurnBased>();

		//trajectory = transform.Find("trajectory").gameObject;

		crossHair = Instantiate(crossHairPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		crossHair.transform.parent = transform;
		crossHair.transform.localPosition = new Vector3(-2.0f, 0, 0);

		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		crossHair.GetComponent<Renderer>().enabled = this.active;

		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

		if (!this.active) return;

		if (gameController.turnActive) this.Controls();

		this.SetCamera();
	}

	void SetCamera() {
		GameObject camera = GameObject.Find("Main Camera");
		camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
	}

	private void Controls() {
		// get input and limit to maxSpeed
		float move = Input.GetAxis("Horizontal") * acceleration;
		rigid.AddRelativeForce(new Vector2(move,0));

		animator.SetFloat("moving", Mathf.Abs(move));

		// flip to right side
		faceDirection = (move == 0) ? faceDirection : (move < 0) ? 1 : -1;
		transform.localScale = new Vector2(faceDirection, 1);
		
		// jumping
		if (Input.GetButtonDown("Jump")) {
			rigid.AddRelativeForce(new Vector2(0, jumpPower * 100));
		}
		
		/*if (Input.GetButton("Fire1")) {
			shootLoading++;
		}*/
		
		if (Input.GetButtonDown("Fire1")) {
			shootLoading = 10f;

			Vector2 shootDirection = transform.position - crossHair.transform.position;
			
			GameObject shoot = Instantiate(shootPrefab, transform.position, Quaternion.identity) as GameObject;

			shoot.GetComponent<Bullet>().spawnedFrom = this.gameObject;

			Rigidbody2D shootRigid = shoot.GetComponent<Rigidbody2D>();
			shootRigid.AddRelativeForce(shootLoading * -shootDirection);
			//shootLoading = 0;
		}
		
		// update crosshair position
		crossHairAngle = 
			Mathf.Min(180, Mathf.Max(0, crossHairAngle - crossHairAcceleration * Input.GetAxis("Vertical")));
		float crossHairRad = - crossHairAngle / 180 * Mathf.PI;
		crossHair.transform.localPosition = 
			crossHairDistance * new Vector3(Mathf.Sin(crossHairRad), Mathf.Cos(crossHairRad));
		
		//drawFlightPath(transform.position, crossHair.transform.position);
	}

	public void onDamage(int hitPoints) {
		hp -= hitPoints;

		if (hp <= 0) player.characterDestroyed(this);
	}

	public void SetActive() {
		this.active = true;
	}

	public void SetInactive() {
		this.active = false;
	}

	private void drawFlightPath(Vector2 startPos, Vector2 startVelocity) {
		LineRenderer line = trajectory.GetComponent<LineRenderer>();
		line.SetPosition(0, startPos);
		line.SetPosition(1, startVelocity);
	}
}
