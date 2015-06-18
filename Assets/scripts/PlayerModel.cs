using UnityEngine;
using System.Collections;

public class PlayerModel {
	private string _name;
	private int _race;

	public PlayerModel(string name, int race) {
		_name = name;
		_race = race;
	}

	public string getName() {
		return _name;
	}

	public int getRace() {
		return _race;
	}
}