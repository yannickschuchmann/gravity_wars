using UnityEngine;
using System.Collections;

public class PlayerModel {
	private string _name;
	private int _race;

	public Player component;

	public ArrayList characters;

	public PlayerModel(string name, string race) {
		_name = name;
		switch(race)
		{
		case "Chameleon":
			_race = 0; break;
		case "Turtle":
			_race = 1; break;
		case "Squirrel":
			_race = 2; break;
		default:
			_race = 0; break;
		}
	}

	public string getName() {
		return _name;
	}

	public int getRace() {
		return _race;
	}
}