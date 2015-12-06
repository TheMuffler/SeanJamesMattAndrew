using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class TalkingScript : MonoBehaviour {

	// Whether the MainCanvas is visible
	public bool isShowing;

	public CanvasGroup thisGroup;
	public GameObject menuCanvas;
	public Animator animator;
	public Animator initalScene;

	// The Number of the next Scene to load
	public int nextScene;
	public int currPart;

	public Image portrait;
	public Text charName;
	public Button continueText;
	public Button convoText;
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

		//menuCanvas.SetActive(false);

		animator = animator.GetComponent<Animator>();

		portrait = portrait.GetComponent<Image>();

		animator.GetBehaviour <TransClass> ().transition=nextScene;



		for(int i=0; i< charsToLoad; ++i)
			charImg[i]=charImg[i].GetComponent<Image>();

		charName = charName.GetComponent<Text>();
		continueText=continueText.GetComponent<Button>();
		convoText= convoText.GetComponent<Button>();


		portrait.sprite=charImg[person[currPart]].sprite;
		charName.text=charImg[person[currPart]].tag;
		convoText.GetComponentInChildren<Text>().text=speech[currPart++];
		//menuCanvas.SetActive();


	}
	private void delay(int delay)
	{
		int t = Environment.TickCount;
		while ((Environment.TickCount - t) < delay) ;
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
		// Start the level
		currPart= person.Length;
		animator.SetTrigger("EndConversation");
		menuCanvas.SetActive(false);

		Debug.Log ("Finished Conversation and Starting next Scene");

	}
	
	// Update is called once per frame
	void fadeInOut(float i)
	{
//		int c=0;
//		i<=0.0f ? c=1 : c=-1;
	

	}
	void Update () {

		if(currPart> person.Length || !animator.GetBehaviour <InitalStart> ().passed  )
		{	
			thisGroup.interactable=false;
			thisGroup.alpha=0;
		}
		else 
		{
			thisGroup.alpha=1;
			thisGroup.interactable=true;
		}
//		menuCanvas.SetActive(isShowing);
		//menuCanvas.SetActive(initalScene.GetBehaviour <InitalStart> ().passed);
	}
	IEnumerator Delayed(int arg=5)
	{
	
		yield return new WaitForSeconds (arg);
	}
}
