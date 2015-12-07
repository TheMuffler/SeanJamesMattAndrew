using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OnClickOfPlanet : MonoBehaviour {
	public string planetName;
	public string planetDescription;


	public  Text planetNameCanvas;
	public  Text planetDescriptionCanvas;

	public CanvasGroup start;
	public CanvasGroup select;

	public int planetNum=0;
	public Camera [] cameras;
	public Light Hilight;
	 
	public float zoomSpeed = 20.0f;
	public float minZoomFOV = 60.0f;
	public float maxZoomFOV = 175f;
	public float defaultZoomFOV=101f;

	public bool doingZoomIn=false;
	public bool doingZoomOut=false;

	public static bool  zoomedIn=false;
	private static int currPlanet=0;

	//How the planet progress is kept track of.
	private bool [] completedPlanet= new bool[4];

	public void startStage()
	{
		// Open the cutscene based on what planet is clicked
		if(currPlanet==1)
		{
            Application.LoadLevel(7);
		}
		if(currPlanet==2)
		{
            Application.LoadLevel(8);
        }
		if(currPlanet==3)
		{
            Application.LoadLevel(9);
        }
	}
	public void ZoomIn(Camera cameraFreeWalk)
	{
		if(!zoomedIn && !doingZoomIn && !doingZoomOut)
		{
			currPlanet=planetNum;

			planetNameCanvas.text=planetName;
			planetDescriptionCanvas.text=planetDescription;

			start.interactable= false;
			select.interactable= true;		

			for(int i=0; i<cameras.Length; ++i)
				cameras[i].enabled=false;
			cameras[planetNum].enabled=true;

			cameraFreeWalk.fieldOfView = maxZoomFOV;
			doingZoomIn=true;
			zoomedIn=true;
		}
	

	}

	public void ZoomOut()
	{
		if(zoomedIn && !doingZoomIn && !doingZoomOut)
		{
			start.interactable= true;
			select.interactable= false;


			for(int i=0; i<cameras.Length; ++i)
				cameras[i].enabled=false;
			cameras[0].enabled=true;

	
			cameras[0].fieldOfView = minZoomFOV;
			doingZoomOut=true;
			zoomedIn=false;
		}
		
	}
	// Use this for initialization
	void Start () {
	
		planetDescriptionCanvas= planetDescriptionCanvas.GetComponent<Text>();
		planetNameCanvas= planetNameCanvas.GetComponent<Text>();

		start = start.GetComponent<CanvasGroup>();
		select = select.GetComponent<CanvasGroup>();

		if(planetNum==3)
			planetDescription="The planet Hearth is known for its warm weather and long summers. " +
				"Hearth was once a water world, with two-thirds of the planet covered by ocean; " +
				"now it best known for being one of the first planets to lose all of its job market due to automation. " +
				"It also has the biggest cantina in the galaxy in which one of your bounties is believed to be located.";
		else if(planetNum==2)
			planetDescription="Putier- J is a mostly gaseous world consisting of hydrogen and helium. " +
				"A big feature is Big Blue, which was created by a giant storm that raged for hundreds of years; " +
				"inside Big Blue there exists the only spaceport located on Putier-J. Your bounty is likely on a ship that docked here.";
		else if(planetNum==1)
			planetDescription="N’Hoth is known for strong winds — sometimes faster than the speed of sound. " +
				"N’Hoth is a man- made planet made from space debris orbiting its solid iron core effectively making it a trash planet where all waste" +
				" eventually ends up on. One of your bounties was last seen in one of its many junkyards";
	}
	
	// Update is called once per frame
	void Update () {
		//THE PLANET COMPLETION IS SET HERE
		for(int i=0; i<completedPlanet.Length;++i)
			completedPlanet[i]=false;
		///////////////
		if(doingZoomIn)
		{
			cameras[planetNum].fieldOfView -= zoomSpeed/10;
			start.alpha -= (zoomSpeed/10)/(maxZoomFOV-minZoomFOV);
			select.alpha += (zoomSpeed/10)/(maxZoomFOV-minZoomFOV);
			if (cameras[planetNum].fieldOfView < minZoomFOV)
			{
				cameras[planetNum].fieldOfView = minZoomFOV;
				doingZoomIn=false;
			}

		}
		else if(doingZoomOut)
		{
			cameras[0].fieldOfView += zoomSpeed/10;

			start.alpha += (zoomSpeed/10)/(defaultZoomFOV-minZoomFOV);
			select.alpha -= (zoomSpeed/10)/(defaultZoomFOV-minZoomFOV);

			if (cameras[0].fieldOfView > defaultZoomFOV)
			{
				cameras[0].fieldOfView = defaultZoomFOV;
				doingZoomOut=false;
			}
			
		}


	}
	void OnMouseDown() 
	{
		if(!completedPlanet[planetNum])
			ZoomIn(cameras[planetNum]);

	}
	void OnMouseOver()
	{
		if(!completedPlanet[planetNum])
			Hilight.enabled=true;
	}
	
	void OnMouseExit()
	{
		if(!completedPlanet[planetNum])
			Hilight.enabled=false;
	}
}
