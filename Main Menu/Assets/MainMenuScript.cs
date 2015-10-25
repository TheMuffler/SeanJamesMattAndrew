using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public CanvasGroup mainMenu;
	public CanvasGroup optionsMenu;
	public Button optionsButton;
	public CanvasGroup quitMenu;
	public Button quitButton;
	public Button startButton;
	public CanvasGroup charSelect;
	
	// Use this for initialization
	void Start () {
	
		mainMenu = mainMenu.GetComponent<CanvasGroup> ();
		optionsMenu = optionsMenu.GetComponent<CanvasGroup> ();
		quitMenu = quitMenu.GetComponent<CanvasGroup> ();
		charSelect = charSelect.GetComponent<CanvasGroup> ();
		startButton = startButton.GetComponent<Button> ();
		optionsButton = optionsButton.GetComponent<Button> ();
		quitButton = quitButton.GetComponent<Button> ();

	}

	public void StartPress() {

		mainMenu.alpha = 0;
		charSelect.alpha = 1;
		startButton.enabled = false;
		optionsButton.enabled = false;
		quitButton.enabled = false;

	}

	public void ReturnToMainMenu() {
	
		mainMenu.alpha = 1;
		charSelect.alpha = 0;
		startButton.enabled = true;
		optionsButton.enabled = true;
		quitButton.enabled = true;
	}


	public void StartGame () {
	
		Application.LoadLevel (1);
	
	}


	public void OptionsPress () {
	
		optionsMenu.alpha = 1;
		startButton.enabled = false;
		optionsButton.enabled = false;
		quitButton.enabled = false;
	
	}

	public void OptionsExit () {

		optionsMenu.alpha = 0;
		startButton.enabled = true;
		optionsButton.enabled = true;
		quitButton.enabled = true;

	}


	public void QuitPress () {
		
		quitMenu.alpha = 1;
		startButton.enabled = false;
		optionsButton.enabled = false;
		quitButton.enabled = false;

	}

	public void CancelQuit () {
	
		quitMenu.alpha = 0;
		startButton.enabled = true;
		optionsButton.enabled = true;
		quitButton.enabled = true;	

	}

	public void ConfirmQuit () {
	
		Application.Quit ();
	
	}



	// Update is called once per frame
	void Update () {
	
	}
}
