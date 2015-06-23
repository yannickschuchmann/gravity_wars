using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int race = 0;
	public bool active = false;

	public PlayerModel model;

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
		return this.CountCharacters() > 0;
	}

	public void SetActive() {
		if (Characters.Count <= 0) return;
		this.active = true;
		activeCharacterIndex = (activeCharacterIndex + 1 >= Characters.Count) 
			? 0 
				: activeCharacterIndex + 1;
		Character activeCharacter = (Character) Characters[activeCharacterIndex];
		activeCharacter.SetActive();
		// TODO highlight player in GUI
	}

	public void SetInactive() {
		this.active = false;
		Character activeCharacter = (Character) Characters[activeCharacterIndex];
		activeCharacter.SetInactive();
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void characterDestroyed(Character character) {
		Characters.Remove(character);
		Destroy(character.gameObject);
	}
}
