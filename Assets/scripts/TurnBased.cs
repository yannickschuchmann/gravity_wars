using UnityEngine;
using System.Collections;

public class TurnBased : MonoBehaviour {
	public int countdownSeconds = 3;
	public int turnSeconds = 5;
	public GUIStyle countdownStyle = new GUIStyle();
	public GUIStyle turnStyle = new GUIStyle();

	private ArrayList Players = new ArrayList();
	private int turnIndex = 0;
	public bool turnActive = false;
	private bool gameOver = false;
	private int activePlayerIndex = 1; // set this random on initialization 
	private int countdown;
	private int turn;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform) {
			Players.Add(child.gameObject.GetComponent<Player>());
		}

		this.NextTurn();
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
