using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectMenu : MonoBehaviour {

	enum Chars {
		tank,
		medic,
		assassin,
		tech
	}

	public CanvasGroup charMenu, talentMenu; //character selection screen, talent selection screen: grouped for easy enable/disable
	public Text charDescription;			 //character description shown to player before they choose to add to team/select talents
	string tankDescription, medicDescription, assassinDescription, techDescription; //individual class descriptions, pull from class factories instead?
	public Button tankIcon, medicIcon, assassinIcon, techIcon, selectCharacter, beginGame;
	public CanvasGroup beginGameCG, team1,team2,team3;
	List<CanvasGroup> teamIcons;
	public Image tankImage, medicImage, assassinImage, techImage;
	public Image characterImage;
	List<Button> talentButtons;				 							//store the below panels for easy access
	public Button talent1, talent2, talent3, talent4, talent5, talent6;	//the panel buttons containing the talent images and descriptions
	public Image talent1Panel, talent2Panel, talent3Panel, talent4Panel, talent5Panel, talent6Panel;

	List<Text> talentDescriptionsList;
	public Text talentDescription1, talentDescription2, talentDescription3, talentDescription4, talentDescription5, talentDescription6; 

	List<Image> talentImages;
	public Image talentImage1, talentImage2, talentImage3, talentImage4, talentImage5, talentImage6; 

	List<ClassFactory> teamMembers;			 			//maintain a list of the team members for display

	List<Image> teamMemberIcons;						//the images showing the current team selection
	public Image teamMember1, teamMember2, teamMember3; //the individual image elements, placed in teamMemberIcons

	Chars currentChar;									//maintains current character selection from the enum

	// Use this for initialization
	void Start () {
	
		charMenu = charMenu.GetComponent<CanvasGroup> ();
		charDescription = charDescription.GetComponent<Text> ();

		tankIcon = tankIcon.GetComponent<Button> ();
		tankImage = tankImage.GetComponent<Image> ();
		tankImage.sprite = new TankFactory ().image;

		medicIcon = medicIcon.GetComponent<Button> ();
		medicImage = medicImage.GetComponent<Image> ();
		medicImage.sprite = new MedicFactory ().image;

		assassinIcon = assassinIcon.GetComponent<Button> ();
		assassinImage = assassinImage.GetComponent<Image> ();
		assassinImage.sprite = new AssassinFactory ().image;

		techIcon = techIcon.GetComponent<Button> ();
		techImage = techImage.GetComponent<Image> ();
		techImage.sprite = new TechFactory ().image;

		characterImage = characterImage.GetComponent<Image> ();

		selectCharacter = selectCharacter.GetComponent<Button> ();
		beginGame = beginGame.GetComponent<Button> ();
		beginGameCG = beginGameCG.GetComponent<CanvasGroup> ();
		beginGame.enabled = false;

		team1 = team1.GetComponent<CanvasGroup> ();
		team2 = team2.GetComponent<CanvasGroup> ();
		team3 = team3.GetComponent<CanvasGroup> ();

		teamIcons = new List<CanvasGroup> ();

		teamIcons.Add (team1);
		teamIcons.Add (team2);
		teamIcons.Add (team3);

		teamMembers = new List<ClassFactory>();
		teamMemberIcons = new List<Image> ();

		teamMember1 = teamMember1.GetComponent<Image> ();
		teamMember2 = teamMember2.GetComponent<Image> ();
		teamMember3 = teamMember3.GetComponent<Image> ();

		teamMemberIcons.Add (teamMember1);
		teamMemberIcons.Add (teamMember2);
		teamMemberIcons.Add (teamMember3);

		talent1Panel = talent1.GetComponent<Image> ();
		talent2Panel = talent2.GetComponent<Image> ();
		talent3Panel = talent3.GetComponent<Image> ();
		talent4Panel = talent4.GetComponent<Image> ();
		talent5Panel = talent5.GetComponent<Image> ();
		talent6Panel = talent6.GetComponent<Image> ();

		talent1Panel.color = new Color32 (3,133,241,208);
		talent2Panel.color = new Color32 (186, 41, 210, 255);
		talent3Panel.color = new Color32 (3,133,241,208);
		talent4Panel.color = new Color32 (186, 41, 210, 255);
		talent5Panel.color = new Color32 (3,133,241,208);
		talent6Panel.color = new Color32 (186, 41, 210, 255);

		talent1 = talent1.GetComponent<Button> ();
		talent2 = talent2.GetComponent<Button> ();
		talent3 = talent3.GetComponent<Button> ();
		talent4 = talent4.GetComponent<Button> ();
		talent5 = talent5.GetComponent<Button> ();
		talent6 = talent6.GetComponent<Button> ();

		talentButtons = new List<Button> ();

		talentButtons.Add (talent1);
		talentButtons.Add (talent2);
		talentButtons.Add (talent3);
		talentButtons.Add (talent4);
		talentButtons.Add (talent5);
		talentButtons.Add (talent6);

		talentDescription1 = talentDescription1.GetComponent<Text> ();
		talentDescription2 = talentDescription2.GetComponent<Text> ();
		talentDescription3 = talentDescription3.GetComponent<Text> ();
		talentDescription4 = talentDescription4.GetComponent<Text> ();
		talentDescription5 = talentDescription5.GetComponent<Text> ();
		talentDescription6 = talentDescription6.GetComponent<Text> ();

		talentDescriptionsList = new List<Text> ();

		talentDescriptionsList.Add (talentDescription1);
		talentDescriptionsList.Add (talentDescription2);
		talentDescriptionsList.Add (talentDescription3);
		talentDescriptionsList.Add (talentDescription4);
		talentDescriptionsList.Add (talentDescription5);
		talentDescriptionsList.Add (talentDescription6);

		talentImage1 = talentImage1.GetComponent<Image> ();
		talentImage2 = talentImage2.GetComponent<Image> ();
		talentImage3 = talentImage3.GetComponent<Image> ();
		talentImage4 = talentImage4.GetComponent<Image> ();
		talentImage5 = talentImage5.GetComponent<Image> ();
		talentImage6 = talentImage6.GetComponent<Image> ();

		talentImages = new List<Image>();

		talentImages.Add (talentImage1);
		talentImages.Add (talentImage2);
		talentImages.Add (talentImage3);
		talentImages.Add (talentImage4);
		talentImages.Add (talentImage5);
		talentImages.Add (talentImage6);


		currentChar = Chars.tank;

		tankDescription = "Tank\n\n" + new TankFactory().description + "\n";

		foreach (Skill s in new TankFactory().baseSkills){
			tankDescription += "\n" + s.name.Replace ("\n"," ") + " - " + s.description;
		}

		medicDescription = "Medic\n\n" + new MedicFactory().description  + "\n";

		foreach (Skill s in new MedicFactory().baseSkills){
			medicDescription += "\n" + s.name.Replace ("\n"," ") + " - " + s.description;
		}

		assassinDescription = "Assassin\n\n" + new AssassinFactory().description  + "\n";

		foreach (Skill s in new AssassinFactory().baseSkills){
			assassinDescription += "\n" + s.name.Replace ("\n"," ") + " - " + s.description;
		}
		assassinDescription += "\n" + "Shiv - " + "Use handy dandy homemade shiv to deal a bit of damage to your target";

		techDescription = "Tech\n\n" + new TechFactory().description  + "\n";

		foreach (Skill s in new TechFactory().baseSkills){
			techDescription += "\n" + s.name.Replace ("\n"," ") + " - " + s.description;
		}
	}


	//not sure how to combine these into a single function with a switch depending on which button was pressed
	//would probably be a much cleaner implementation

	public void selectTalentOne(){
		teamMembers [teamMembers.Count - 1].talentOptions [0].toggle = true;
		teamMembers [teamMembers.Count - 1].talentOptions [1].toggle = false;
		talent1Panel.color = new Color32 (3,133,241,208);
		talent2Panel.color = new Color32 (186, 41, 210, 255);
		talentButtons [0].enabled = false;
		talentButtons [1].enabled = true;
	}

	public void selectTalentTwo(){
		teamMembers [teamMembers.Count - 1].talentOptions [1].toggle = true;
		teamMembers [teamMembers.Count - 1].talentOptions [0].toggle = false;
		talent2Panel.color = new Color32 (3,133,241,208);
		talent1Panel.color = new Color32 (186, 41, 210, 255);
		talentButtons [1].enabled = false;
		talentButtons [0].enabled = true;
	}

	public void selectTalentThree(){
		teamMembers [teamMembers.Count - 1].talentOptions [2].toggle = true;
		teamMembers [teamMembers.Count - 1].talentOptions [3].toggle = false;
		talent3Panel.color = new Color32 (3,133,241,208);
		talent4Panel.color = new Color32 (186, 41, 210, 255);
		talentButtons [2].enabled = false;
		talentButtons [3].enabled = true;
	}
	
	public void selectTalentFour(){
		teamMembers [teamMembers.Count - 1].talentOptions [3].toggle = true;
		teamMembers [teamMembers.Count - 1].talentOptions [2].toggle = false;
		talent4Panel.color = new Color32 (3,133,241,208);
		talent3Panel.color = new Color32 (186, 41, 210, 255);
		talentButtons [3].enabled = false;
		talentButtons [2].enabled = true;
	}

	public void selectTalentFive(){
		teamMembers [teamMembers.Count - 1].talentOptions [4].toggle = true;
		teamMembers [teamMembers.Count - 1].talentOptions [5].toggle = false;
		talent5Panel.color = new Color32 (3,133,241,208);
		talent6Panel.color = new Color32 (186, 41, 210, 255);
		talentButtons [4].enabled = false;
		talentButtons [5].enabled = true;
	}
	
	public void selectTalentSix(){
		teamMembers [teamMembers.Count - 1].talentOptions [5].toggle = true;
		teamMembers [teamMembers.Count - 1].talentOptions [4].toggle = false;
		talent6Panel.color = new Color32 (3,133,241,208);
		talent5Panel.color = new Color32 (186, 41, 210, 255);
		talentButtons [5].enabled = false;
		talentButtons [4].enabled = true;
	}


	public void Begin(){

		GlobalManager.instance.PartyFactories = teamMembers;

	}

	public void displayTank(){
		currentChar = Chars.tank;

	}

	public void displayMedic(){
		currentChar = Chars.medic;

	}

	public void displayAssassin(){
		currentChar = Chars.assassin;

	}

	public void displayTech(){
		currentChar = Chars.tech;

	}

	public void selectChar(){
		switch (currentChar) {
		case Chars.tank:
			teamMembers.Add (new TankFactory());
			break;
		case Chars.medic:
			teamMembers.Add (new MedicFactory());
			break;
		case Chars.assassin:
			teamMembers.Add (new AssassinFactory());
			break;
		case Chars.tech:
			teamMembers.Add (new TechFactory());
			break;
		default:
			break;
		}

		for( int i = 0; i < teamMembers[teamMembers.Count - 1].talentOptions.Count; ++i) {
			//get the talent name + description
			talentDescriptionsList[i].text = teamMembers[teamMembers.Count - 1].talentOptions[i].name + "\n" + 
				teamMembers[teamMembers.Count - 1].talentOptions[i].description;
			talentImages[i].sprite = teamMembers[teamMembers.Count - 1].talentOptions[i].icon;
			//initialize left column of talents to be selected, right column unselected
			if( i % 2 == 0){
				teamMembers[teamMembers.Count - 1].talentOptions[i].toggle = true;
				talentButtons[i].enabled = false;
			}
			else{
				teamMembers[teamMembers.Count - 1].talentOptions[i].toggle = false;
				talentButtons[i].enabled = true;
			}

		}

		talent1Panel.color = new Color32 (3,133,241,208);
		talent2Panel.color = new Color32 (186, 41, 210, 255);
		talent3Panel.color = new Color32 (3,133,241,208);
		talent4Panel.color = new Color32 (186, 41, 210, 255);
		talent5Panel.color = new Color32 (3,133,241,208);
		talent6Panel.color = new Color32 (186, 41, 210, 255);


	}

	public void confirmSelectChar(){
		//update the team member image icons here
		teamMemberIcons [teamMembers.Count - 1].sprite = teamMembers [teamMembers.Count - 1].image;

		int teamCount = teamMembers.Count;

		for (int i = 0; i < 3; ++i)
			if (i < teamCount)
				teamIcons [i].alpha = 1;
			else
				teamIcons [i].alpha = 0;

		//set their alpha in Update
	}

	public void cancelSelectChar(){
		teamMembers.RemoveAt(teamMembers.Count - 1);
	}

	public void removeTeamMember1(){
		teamMembers.RemoveAt (0);

		int teamCount = teamMembers.Count;

		for (int i = 0; i < 3; ++i) {
			if(i < teamCount)
				teamMemberIcons[i].sprite = teamMembers[i].image;
			else
				teamIcons[i].alpha = 0;
		}

	}

	public void removeTeamMember2(){
		teamMembers.RemoveAt (1);

		int teamCount = teamMembers.Count;
		
		for (int i = 0; i < 3; ++i) {
			if(i < teamCount){
				teamMemberIcons[i].sprite = teamMembers[i].image;
				teamIcons[i].alpha = 1;
			}
			else
				teamIcons[i].alpha = 0;
		}

	}

	public void removeTeamMember3(){
		teamMembers.RemoveAt (2);

		int teamCount = teamMembers.Count;
		
		for (int i = 0; i < 3; ++i) {
			if(i < teamCount)
				teamMemberIcons[i].sprite = teamMembers[i].image;
			else
				teamIcons[i].alpha = 0;
		}

	
	}

	// Update is called once per frame
	void Update () {
		switch (currentChar) {
			case Chars.tank:
				charDescription.text = tankDescription;
				characterImage.sprite = new TankFactory ().image;
				break;
			case Chars.medic:
				charDescription.text = medicDescription;
				characterImage.sprite = new MedicFactory ().image;
				break;
			case Chars.assassin:
				charDescription.text = assassinDescription;
				characterImage.sprite = new AssassinFactory ().image;
				break;
			case Chars.tech:
				charDescription.text = techDescription;
				characterImage.sprite = new TechFactory ().image;
				break;
			default:
				break;
		}

		//full team or not enough team members, toggle buttons
		if (teamMembers.Count == 3) {
			beginGame.enabled = true;
			beginGameCG.alpha = 1;
			selectCharacter.enabled = false;
		} else {
			beginGame.enabled = false;
			beginGameCG.alpha = 0;
			selectCharacter.enabled = true;
		}

	}
}
