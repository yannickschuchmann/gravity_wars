﻿using UnityEngine;
using System.Collections;

public class TurnBased : MonoBehaviour {
	public int countdownSeconds = 3;
	public int turnSeconds = 5;
	public GUIStyle countdownStyle = new GUIStyle();
	public GUIStyle turnStyle = new GUIStyle();
	public GameObject playerPrefab;

	public GameObject racePrefab0;
	public GameObject racePrefab1;
	public GameObject racePrefab2;


	private ArrayList Players = new ArrayList();
	private int turnIndex = 0;
	public bool turnActive = false;
	private bool gameOver = false;
	private int activePlayerIndex = 0; // set this random on initialization 
	private int countdown;
	private int turn;

	private GameObject[] planets;

	// Use this for initialization
	void Start () {
		if (GameProperties.PlayerModels.Count == 0) {
			GameProperties.PlayerModels.Add(new PlayerModel("Markus", 0));
			GameProperties.PlayerModels.Add(new PlayerModel("Bertha", 0	));
		}
		planets = GameObject.FindGameObjectsWithTag("Planet");

		SpawnPlayers();

		this.NextTurn();
	}

	void SpawnPlayers() {
		foreach(PlayerModel pl in GameProperties.PlayerModels) {
			GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			player.transform.parent = transform;
			player.transform.localPosition = new Vector3(2.0f, 0, 0);
			Player playerScript = player.GetComponent<Player>();
			playerScript.model = pl;
			playerScript.race = pl.getRace();
			Players.Add(playerScript);

			pl.component = playerScript;

			SpawnCharacters(player);
		}
	}

	void SpawnCharacters(GameObject player) {
		Player playerScript = player.GetComponent<Player>();
		for(int i = 0; i < GameProperties.AmountCharacters; i++) {
			GameObject prefab;
			switch(playerScript.race)
			{
			case 0:
				prefab = racePrefab0;
				break;
			case 1:
				prefab = racePrefab1;
				break;
			case 2:
				prefab = racePrefab2;
				break;
			default:
				prefab = racePrefab0;
				break;
			}
			GameObject character = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
			character.transform.parent = player.transform;

			int randomPlanet = Random.Range(0, planets.Length);

			Vector3 positionOnPlanet = getRandomPlanetPosition(planets[randomPlanet]);

			character.transform.position = planets[randomPlanet].transform.position + positionOnPlanet;
			Character characterScript = character.GetComponent<Character>();
			playerScript.Characters.Add(characterScript);
		}
	}

	Vector3 getRandomPlanetPosition(GameObject planet) {
		float radius = planet.GetComponent<Gravity2D>().GravityDistance - 1;
		float xValue = Random.Range(-radius, radius);

		float negaPosi = (Random.Range(0, 2) < 1) ? -1 : 1;

		float yValue = negaPosi * Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(xValue, 2));

		return new Vector3(xValue, yValue, 0);
	}
	
	int CountAlive() {
		int sum = 0;
		foreach (Player player in Players) {
			if (player.Alive()) sum++;
		}
		return sum;
	}

	void NextTurn() {
		if (this.gameOver) return;
		this.BeforeTurn();
	}

	void BeforeTurn() {
		this.turnIndex++;	

		Player activePlayer = this.NextPlayer();
		activePlayer.SetActive();

		this.countdown = this.countdownSeconds;
		InvokeRepeating("decreaseCountdown", 1.0f, 1.0f);
	}


	Player NextPlayer() {
		Player activePlayer;
		while (true) {
			activePlayerIndex = (activePlayerIndex + 1 >= Players.Count) ? 0 : activePlayerIndex + 1;
			activePlayer = (Player) this.Players[activePlayerIndex];
			if (activePlayer.Alive()) return activePlayer;
		}
	}

	void AfterTurn() {
		this.turnActive = false;
		Player activePlayer = (Player) this.Players[activePlayerIndex];
		activePlayer.SetInactive();
		this.NextTurn();
	}

	void decreaseCountdown() {
		this.countdown--;
		if (countdown == 0) {
			CancelInvoke("decreaseCountdown");
			this.turn = this.turnSeconds;
			this.turnActive = true;
			InvokeRepeating("decreaseTurn", 1.0f, 1.0f);
		}
	}
	
	void decreaseTurn() {
		this.turn--;
		if (turn == 0) {
			CancelInvoke("decreaseTurn");
			this.AfterTurn();
		}
	}
	
	// timer
	void OnGUI() {
		if (countdown > 0) {
			GUI.Label(new Rect(Screen.width/2-200,Screen.height/2-45,400,90), countdown.ToString(), countdownStyle);
		}
		if (turn > 0) {
			GUI.Label(new Rect(Screen.width-410,Screen.height-90,400,90), turn.ToString(), turnStyle);
		}
	}	

	// Update is called once per frame
	void Update () {
		if (this.CountAlive() == 1) {
			this.gameOver = true;
			Debug.Log("Game over");
		}
	}
}
