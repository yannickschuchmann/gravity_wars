using UnityEngine;
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

	// Use this for initialization
	void Start () {
		if (GameProperties.PlayerModels.Count == 0) {
			GameProperties.PlayerModels.Add(new PlayerModel("Markus", 0));
			GameProperties.PlayerModels.Add(new PlayerModel("Bertha", 0	));
		}

		SpawnPlayers();


		this.NextTurn();
	}

	void SpawnPlayers() {
		foreach(PlayerModel pl in GameProperties.PlayerModels) {
			GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			player.transform.parent = transform;
			player.transform.localPosition = new Vector3(2.0f, 0, 0);
			Player playerScript = player.GetComponent<Player>();
			playerScript.playerName = pl.getName();
			playerScript.race = pl.getRace();
			Players.Add(playerScript);
			
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
			
			character.transform.position = new Vector3(12.0f + (float) i, -7.0f, 0);
			Character characterScript = character.GetComponent<Character>();
			playerScript.Characters.Add(characterScript);
		}
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
