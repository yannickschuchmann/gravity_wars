using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class StartMenu : MonoBehaviour {

	public Canvas playerOneCanvas;
	public Canvas playerTwoCanvas;
	public Canvas optionsCanvas;
	public Canvas creditsCanvas;
	public Slider volumeSlider;
	private Canvas startCanvas;
	private Canvas activeCanvas;
	private float volSliderValue = 0.8f;

	// Use this for initialization
	void Start () {
		playerOneCanvas = playerOneCanvas.GetComponent<Canvas>();
		playerTwoCanvas = playerTwoCanvas.GetComponent<Canvas>();
		optionsCanvas = optionsCanvas.GetComponent<Canvas>();
		creditsCanvas = creditsCanvas.GetComponent<Canvas>();

		playerOneCanvas.enabled = false;
		playerTwoCanvas.enabled = false;
		optionsCanvas.enabled = false;
		creditsCanvas.enabled = false;

		startCanvas = GetComponent<Canvas>();
		activeCanvas = startCanvas;
		activeCanvas.enabled = true;

		volumeSlider.value = volSliderValue;
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.GetComponent<AudioSource>().volume = volSliderValue;
	}

	void OnGUI() {
		volSliderValue = volumeSlider.value;
	}

	public void OnQuitPressed() {
		Application.Quit();
	}

	public void OnStartPressed() {
		switchCanvas(playerOneCanvas);
	}

	public void OnNextPressed() {
		if (addPlayerToGameProperties(playerOneCanvas))
			switchCanvas(playerTwoCanvas);
	}

	private bool addPlayerToGameProperties(Canvas playerCanvas) {
		InputField inputName = (InputField) playerCanvas.transform.Find("InputName").GetComponent<InputField>();
		ToggleGroup raceGroup = playerCanvas.transform.Find("Race").GetComponent<ToggleGroup>();
		Toggle activeRace = raceGroup.ActiveToggles().FirstOrDefault();

		if (activeRace.name != "" && inputName.text != "") {
			GameProperties.PlayerModels.Add(new PlayerModel(inputName.text, activeRace.name));
			return true;
		} else {
			return false;
		}
	}

	public void OnPlayPressed() {
		if (addPlayerToGameProperties(playerTwoCanvas))
			Application.LoadLevel(1);
	}

	private void switchCanvas(Canvas newCanvas) {
		activeCanvas.enabled = false;
		activeCanvas = newCanvas;
		activeCanvas.enabled = true;
	}

	public void OnOptionsPressed() {
		switchCanvas(optionsCanvas);
	}

	public void OnBackPressed() {
		switchCanvas(startCanvas);
	}
	
	public void OnCreditsPressed() {
		switchCanvas(creditsCanvas);
	}
	

}
