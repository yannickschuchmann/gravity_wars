using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string playerName = "";
	public int race = 0;
	public bool active = false;

	public GameObject racePrefab0;
	public GameObject racePrefab1;
	public GameObject racePrefab2;

	public bool isInitialized = false;

	public ArrayList Characters = new ArrayList();
	public int activeCharacterIndex = 0;

	// Use this for initialization
	void Start () {
		this.isInitialized = true;
	}

	public int CountCharacters() {
		return transform.childCount;
	}

	public bool Alive() {
		return true;
			//this.CountCharacters() > 0 && this.isInitialized;
	}

	public void SetActive() {
		if (Characters.Count <= 0) return;
		this.active = true;
		Character activeCharacter = (Character) Characters[activeCharacterIndex];
		activeCharacter.SetActive();
		// TODO highlight player in GUI
	}

	public void SetInactive() {
		this.active = false;
		Character activeCharacter = (Character) Characters[activeCharacterIndex];
		activeCharacter.SetInactive();
		activeCharacterIndex = (activeCharacterIndex + 1 >= Characters.Count) 
			? 0 
			: activeCharacterIndex + 1;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
