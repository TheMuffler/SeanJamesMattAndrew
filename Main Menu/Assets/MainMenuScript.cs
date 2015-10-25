using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	enum Menu{
		main,
		characterSelect,
		options
	}


	public CanvasGroup mainMenu;
	public CanvasGroup optionsMenu;
	public Button optionsButton;
	public CanvasGroup quitMenu;
	public Button quitButton;
	public Button startButton;
	public CanvasGroup charSelect;
	public Camera UICamera;
	Menu menuPosition;
	
	// Use this for initialization
	void Start () {
	
		mainMenu = mainMenu.GetComponent<CanvasGroup> ();
		optionsMenu = optionsMenu.GetComponent<CanvasGroup> ();
		quitMenu = quitMenu.GetComponent<CanvasGroup> ();
		charSelect = charSelect.GetComponent<CanvasGroup> ();
		startButton = startButton.GetComponent<Button> ();
		optionsButton = optionsButton.GetComponent<Button> ();
		quitButton = quitButton.GetComponent<Button> ();
		UICamera = UICamera.GetComponent<Camera> ();

		menuPosition = Menu.main;

	}

	public void StartPress() {

		mainMenu.alpha = 0;
		charSelect.alpha = 1;
		startButton.enabled = false;
		optionsButton.enabled = false;
		quitButton.enabled = false;

		menuPosition = Menu.characterSelect;


	}

	public void ReturnToMainMenu() {
	
		mainMenu.alpha = 1;
		charSelect.alpha = 1;
		startButton.enabled = true;
		optionsButton.enabled = true;
		quitButton.enabled = true;

		menuPosition = Menu.main;
	}


	public void StartGame () {
	
		Application.LoadLevel (1);
	
	}


	public void OptionsPress () {
	
		optionsMenu.alpha = 1;
		startButton.enabled = false;
		optionsButton.enabled = false;
		quitButton.enabled = false;

		menuPosition = Menu.options;
	
	}

	public void OptionsExit () {

		optionsMenu.alpha = 1;
		startButton.enabled = true;
		optionsButton.enabled = true;
		quitButton.enabled = true;

		menuPosition = Menu.main;

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

	void FixedUpdate() {

		if (menuPosition == Menu.main) {
			Vector3 relativePos = (mainMenu.transform.position - UICamera.transform.position);
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			
			Quaternion current = UICamera.transform.localRotation;
			
			UICamera.transform.rotation = Quaternion.Slerp(current, rotation, Time.deltaTime);


		}

		if (menuPosition == Menu.characterSelect) {
			Vector3 relativePos = (charSelect.transform.position - UICamera.transform.position);
			Quaternion rotation = Quaternion.LookRotation(relativePos);

			Quaternion current = UICamera.transform.localRotation;
			
			UICamera.transform.rotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
		}


	}


	// Update is called once per frame
	void Update () {
	
	}
}
