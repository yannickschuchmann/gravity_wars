using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class StartMenu : MonoBehaviour {

	public Canvas playerOneCanvas;
	public Canvas playerTwoCanvas;
	public Canvas levelCanvas;
	public Canvas amountCanvas;
	public Canvas optionsCanvas;
	public Canvas creditsCanvas;
	public Slider volumeSlider;
	private Canvas startCanvas;
	private Canvas activeCanvas;
	private float volSliderValue = 0.8f;

	private InputField amountInput;

	private int LevelNumber = 1;

	// Use this for initialization
	void Start () {
		playerOneCanvas = playerOneCanvas.GetComponent<Canvas>();
		playerTwoCanvas = playerTwoCanvas.GetComponent<Canvas>();
		levelCanvas = levelCanvas.GetComponent<Canvas>();
		amountCanvas = amountCanvas.GetComponent<Canvas>();
		optionsCanvas = optionsCanvas.GetComponent<Canvas>();
		creditsCanvas = creditsCanvas.GetComponent<Canvas>();

		amountInput = amountCanvas.transform.Find("Amount").GetComponent<InputField>();
		amountInput.text = GameProperties.AmountCharacters.ToString();

		playerOneCanvas.enabled = false;
		playerTwoCanvas.enabled = false;
		levelCanvas.enabled = false;
		amountCanvas.enabled = false;
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

	public void OnToTwoPressed() {
		if (addPlayerToGameProperties(playerOneCanvas))
			switchCanvas(playerTwoCanvas);
	}

	public void OnToLevelPressed() {
		if (addPlayerToGameProperties(playerTwoCanvas))
			switchCanvas(levelCanvas);
	}

	public void OnToAmountPressed() {
		// add levels
		ToggleGroup levelGroup = levelCanvas.transform.Find("Level").GetComponent<ToggleGroup>();
		Toggle activeLevel = levelGroup.ActiveToggles().FirstOrDefault();

		switch (activeLevel.name)
		{
		case "First":
			LevelNumber = 1;
			break;
		case "Second":
			LevelNumber = 2;
			break;
		case "Third":
			LevelNumber = 3;
			break;
		default:
			LevelNumber = 1;
			break;
		}

		if (LevelNumber != null)
			switchCanvas(amountCanvas);
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
		int amount = int.Parse(amountInput.text);

		if (amount != null) {
			GameProperties.AmountCharacters = amount;
			Application.LoadLevel(LevelNumber);
		}
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
