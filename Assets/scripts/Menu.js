#pragma strict
/* Hauptmenu */

var image : Texture2D;
var image2 : Texture2D;
var content : GUIContent;
var volSliderValue : float = 0.8;

function Start(){
	image = Resources.Load("button_new_game");
	image2 = Resources.Load("button_new_game_hover");
}
function Update(){
	GetComponent(AudioSource).volume = volSliderValue;
}

function OnGUI(){
	GUI.skin.button.normal.background = image;
	GUI.skin.button.hover.background = image2;
	GUI.skin.button.active.background = image;
	
	if (GUI.Button(Rect(565,200,250,60), content)){
		Application.LoadLevel(1);
	}
	volSliderValue = GUI.HorizontalSlider (Rect(600, 458, 180, 30), volSliderValue, 0.0, 1.0);
}

