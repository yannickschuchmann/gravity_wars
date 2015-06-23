using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreen : MonoBehaviour {

	public Text winnerText;

	private TurnBased gameController;
	
	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<TurnBased>();
		winnerText = winnerText.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (gameController.gameOver) {
			GetComponent<Canvas>().enabled = true;
			PlayerModel winner = gameController.getWinner();
			winnerText.text = winner.getName();
			winnerText.color = GameProperties.playerColors[winner.playerIndex];
		}
	}
}
