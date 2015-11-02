using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TurnOrderUI : MonoBehaviour {
	public int numOfObjs = 7;
	Queue<int> turnOrder = new Queue<int>();
	public SingleBox[] boxArray;
	public GameObject box;

	// Use this for initialization
	void Start () {
		boxArray = new SingleBox[numOfObjs];
		CreateBoxes ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void CreateBoxes() {
		for (int i = 0; i < numOfObjs; i++) {		
			if (i == 3) {
				boxArray[i] = CreateBigBox (i);
			} else if (i > 3) {
				boxArray[i] = CreateSmallBox(i, true);
			} else {
				boxArray[i] = CreateSmallBox (i, false);
			}
		}
	} 

	SingleBox CreateBigBox(int position) {
		SingleBox bigBox = CreateSmallBox (position, false);
		bigBox.y = 17;
		bigBox.SetBoxSize (50, 50);
		return bigBox;
	}

	//called from GameManager to update the turn order
	public void TurnOrderUpdate(int a) {
		for (int i = 0; i < numOfObjs; i++) {
			boxArray[i].SetColor (a++);
		}
	}

	SingleBox CreateSmallBox(int position, bool offset)
	{
		// Create our new game object
		GameObject go = new GameObject("SingleBox");		
		// Add components
		go.AddComponent<MeshFilter>();
		go.AddComponent<MeshRenderer>();
		go.transform.parent = transform;
		int x = offset ? 80+40*position : 50+40*position;
		int y = 35;
		// Init
		SingleBox smallBox = go.AddComponent<SingleBox>();
		smallBox.SetPosition (x, y);
		smallBox.SetColor(position);
		return smallBox;
	}
}
