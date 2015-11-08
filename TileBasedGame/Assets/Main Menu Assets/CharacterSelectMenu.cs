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
	public Text charDescription;			 //character description shown to player before they choose to add to team
	public Image charImage;
	public Button tankIcon, medicIcon, assassinIcon, techIcon, selectCharacter;

	List<Button> talentButtons;
	List<ClassFactory> teamMembers;			 //maintain a list of the team members for display
	List<Image> teamMemberIcons;
	public Image teamMember1, teamMember2, teamMember3;
	Chars currentChar;
	string tankDescription, medicDescription, assassinDescription, techDescription;

	public Text talentDescription1, talentDescription2, talentDescription3, talentDescription4, talentDescription5, talentDescription6; 
	List<Text> talentDescriptionsList;


	// Use this for initialization
	void Start () {
	
		charMenu = charMenu.GetComponent<CanvasGroup> ();
		charDescription = charDescription.GetComponent<Text> ();
		charImage = charImage.GetComponent<Image> ();

		tankIcon = tankIcon.GetComponent<Button> ();
		medicIcon = medicIcon.GetComponent<Button> ();
		assassinIcon = assassinIcon.GetComponent<Button> ();
		techIcon = techIcon.GetComponent<Button> ();
		selectCharacter = selectCharacter.GetComponent<Button> ();

		teamMembers = new List<ClassFactory>();
		teamMemberIcons = new List<Image> ();

		teamMember1 = teamMember1.GetComponent<Image> ();
		teamMember2 = teamMember2.GetComponent<Image> ();
		teamMember3 = teamMember3.GetComponent<Image> ();

		teamMemberIcons.Add (teamMember1);
		teamMemberIcons.Add (teamMember2);
		teamMemberIcons.Add (teamMember3);

		talentDescription1 = talentDescription1.GetComponent<Text> ();
		talentDescription2 = talentDescription2.GetComponent<Text> ();
		talentDescription3 = talentDescription3.GetComponent<Text> ();
		talentDescription4 = talentDescription4.GetComponent<Text> ();
		talentDescription5 = talentDescription5.GetComponent<Text> ();
		talentDescription6 = talentDescription6.GetComponent<Text> ();

		talentDescriptionsList = new List<Text>();

		talentDescriptionsList.Add (talentDescription1);
		talentDescriptionsList.Add (talentDescription2);
		talentDescriptionsList.Add (talentDescription3);
		talentDescriptionsList.Add (talentDescription4);
		talentDescriptionsList.Add (talentDescription5);
		talentDescriptionsList.Add (talentDescription6);

		currentChar = Chars.tank;

		tankDescription = "The Tank is a rough, tough melee brawler. He is capable of taking big hits as well as dishing them out.";
		medicDescription = "The Medic is";
		assassinDescription = "The Assassin is";
		techDescription = "The Tech is";
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
			teamMembers.Add (new TankFactory());
			//charImage.mainTexture = medicImage;
			break;
		case Chars.assassin:
			teamMembers.Add (new TankFactory());
			//charImage.mainTexture = assassinImage;
			break;
		case Chars.tech:
			teamMembers.Add (new TankFactory());
			//charImage.mainTexture = techImage;
			break;
		default:
			break;
		}

		for( int i = 0; i < teamMembers[teamMembers.Count - 1].talentOptions.Count; ++i) {
			talentDescriptionsList[i].text = teamMembers[teamMembers.Count - 1].talentOptions[i].description;		
		}

	}

	public void confirmSelectChar(){
	
	}

	public void cancelSelectChar(){
		teamMembers.RemoveAt(teamMembers.Count - 1);
	}

	public void displayTeamMembers(){

	}

	public void removeTeamMember(){
	
	
	}

	// Update is called once per frame
	void Update () {
		switch (currentChar) {
			case Chars.tank:
				charDescription.text = tankDescription;
				//charImage.mainTexture = tankImage;
				break;
			case Chars.medic:
				charDescription.text = medicDescription;
				//charImage.mainTexture = medicImage;
				break;
			case Chars.assassin:
				charDescription.text = assassinDescription;
				//charImage.mainTexture = assassinImage;
				break;
			case Chars.tech:
				charDescription.text = techDescription;
				//charImage.mainTexture = techImage;
				break;
			default:
				break;
		}
		


	}
}
