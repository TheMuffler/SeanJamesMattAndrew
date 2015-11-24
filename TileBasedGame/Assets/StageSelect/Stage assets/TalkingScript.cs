using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TalkingScript : MonoBehaviour {


	public Image portrait;
	public Text charName;
	public Button continueText;
	public Button convoText;
//	public Text levelInfoText;
	int currPart;
	public int charsToLoad;

	public List<Image> charImg = new List<Image>();
	//Tank MED Tech ASS
	// Testing conversations with 10
	public int[] person = new []{2,3,1,2,3,0,1,0,1,3}; //These are the 
	public string[] speech=  new string [10];

	// SAMPLE TEXT  that is hardcoded for now//
	

	// -- Can be somewhat dynamic later on -- //

	// Use this for initialization
	void Start () {
		//charsToLoad=4; //Amount of chars who can talk
		/*
		speech[0]="Give me one good reason why I should wear a dress.";
		speech[1]="You make me feel like I'm not good enough."; 
		speech[2]="What a thing to say - and on my birthday!";
		speech[3]="Wait...There's something in the ship!";
		speech[4]="It's... A robot?";
		speech[5]="What's the deal with airline food?";
		speech[6]=	"Who are you and what are you doing here?!";
		speech[7]=	"I'm your daughter and this is where I live.";
		speech[8]=	"I don't want to have a baby.";
		speech[9]=	"This isn't just about you. We are keeping her; she could be useful against bandits at the cantina";
		*/
		portrait = portrait.GetComponent<Image>();


		for(int i=0; i< charsToLoad; ++i)
			charImg[i]=charImg[i].GetComponent<Image>();

		charName = charName.GetComponent<Text>();
		continueText=continueText.GetComponent<Button>();
		convoText= convoText.GetComponent<Button>();


		portrait.sprite=charImg[person[currPart]].sprite;
		charName.text=charImg[person[currPart]].tag;
		convoText.GetComponentInChildren<Text>().text=speech[currPart++];

	}
	//This function will run through all of the text and then start the level after it quits
	public void NextMove(){
		continueText.enabled=true;
		convoText.enabled=true;

		if( currPart< person.Length)
		{
			portrait.sprite=charImg[person[currPart]].sprite;
			charName.text=charImg[person[currPart]].tag;
			convoText.GetComponentInChildren<Text>().text = speech[currPart];
		}

		else 
			startLevel();
		++currPart;

	}
	public void startLevel()
	{
		Debug.Log ("Stage Select!");
		Application.LoadLevel (1);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
