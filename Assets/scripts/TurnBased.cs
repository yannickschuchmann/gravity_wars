using UnityEngine;
using System.Collections;

public class TurnBased : MonoBehaviour {
	public int countDownSeconds = 3;
	public int turnSeconds = 30;

	private ArrayList Players = new ArrayList();
	private int roundIndex = 0;
	private bool roundActive = false;
	private bool gameOver = false;
	private int activePlayerIndex = 0; // set this random on initialization 

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform) {
			Players.Add(child.gameObject.GetComponent<Player>());
		}

		this.NextRound();
	}

	int CountAlive() {
		int sum = 0;
		foreach (Player player in Players) {
			if (player.Alive()) sum++;
		}
		return sum;
	}

	void NextRound() {
		if (this.gameOver) return;
		this.roundIndex++;
		this.roundActive = true;
		this.BeforeRound();


	}

	void BeforeRound() {
		Player activePlayer = this.NextPlayer();
		activePlayer.SetActive();
	}

	Player NextPlayer() {
		Player activePlayer;
		while (true) {
			activePlayerIndex = (activePlayerIndex + 1 >= Players.Count) ? 0 : activePlayerIndex++;
			activePlayer = (Player) this.Players[activePlayerIndex];
			if (activePlayer.Alive()) return activePlayer;
		}
	}

	void AfterRound() {
		this.roundActive = false;
	}

	// Update is called once per frame
	void Update () {
		if (this.CountAlive() == 1) {
			this.gameOver = true;
			Debug.Log("Game over");
		}
	}
}
