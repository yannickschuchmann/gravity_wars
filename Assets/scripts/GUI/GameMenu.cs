using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMenu : MonoBehaviour {

	private Canvas MenuCanvas;
	public Canvas DecoCanvas;
	public Canvas PauseMenu;
	private bool paused = false;

	// Use this for initialization
	void Start () {
		MenuCanvas = GetComponent<Canvas>();
		DecoCanvas = DecoCanvas.GetComponent<Canvas>();
		PauseMenu = PauseMenu.GetComponent<Canvas>();
		Resume();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("ToggleMenu")) {
			paused = !paused;

			if (!paused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	public void OnResumePressed() {
		paused = false;
		Resume();
	}

	public void OnQuitPressed() {
		GameProperties.PlayerModels = new ArrayList();
		Application.LoadLevel(0);
	}

	public void Resume() {
		Time.timeScale = 1;
		MenuCanvas.enabled = false;
		DecoCanvas.enabled = false;
		PauseMenu.enabled = false;
	}

	public void Pause() {
		Time.timeScale = 0;
		MenuCanvas.enabled = true;
		DecoCanvas.enabled = true;
		PauseMenu.enabled = true;
	}
	
}
