using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string playerName = "";
	public int index;
	public bool active = false;

	private ArrayList Characters = new ArrayList();
	private int activeCharacterIndex = 0;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform) {
			Characters.Add(child.GetComponent<Character>());
		}
	}

	public int CountCharacters() {
		return transform.childCount;
	}

	public bool Alive() {
		return this.CountCharacters() > 0;
	}

	public void SetActive() {
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
