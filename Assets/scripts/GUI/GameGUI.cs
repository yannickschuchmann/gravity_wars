using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public GameObject playerGUIPrefab;
	public GameObject playerGUISContainer;


	private GameObject[] playerGUIs;
	private bool drawed = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI() {
		if (!drawed) {
			draw();
			drawed = true;
		}
	}

	void draw() {
		int i = 0;
		playerGUIs = new GameObject[GameProperties.PlayerModels.Count];
		foreach(PlayerModel pl in GameProperties.PlayerModels) {
			GameObject element = Instantiate(playerGUIPrefab) as GameObject;

			adjustPlayerGUI(element, pl, i);
			playerGUIs[i] = element;
			i++;
		}
	}

	void adjustPlayerGUI(GameObject element, PlayerModel pl, int index) {
		Transform elTransform = element.transform;

		elTransform.SetParent(playerGUISContainer.transform);
		Text name = elTransform.FindChild("Name").GetComponent<Text>();
		Image lifebar = elTransform.FindChild("Lifebar").GetComponent<Image>();

		Debug.Log (GameProperties.playerColors[index]);
		name.color = GameProperties.playerColors[index];
		name.text = pl.getName();
		lifebar.color = GameProperties.playerColors[index];

	}
}
