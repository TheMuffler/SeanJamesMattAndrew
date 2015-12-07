using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OnClickOfPlanet : MonoBehaviour {
	public string planetName;
	public string planetDescription;

	public AudioSource audio ;
	public  Text planetNameCanvas;
	public  Text planetDescriptionCanvas;

	public CanvasGroup start;
	public CanvasGroup select;
	public CanvasGroup deny;

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
	private static bool denyingLevel=false;
	//How the planet progress is kept track of.
	private bool [] completedPlanet= new bool[4];

	public void startStage()
	{
		// Open the cutscene based on what planet is clicked
		if(currPlanet==1)
		{

		}
		if(currPlanet==2)
		{
			
		}
		if(currPlanet==3)
		{
			
		}
	}
	public void ZoomIn(Camera cameraFreeWalk)
	{
		if(!zoomedIn && !doingZoomIn && !doingZoomOut)
		{
			currPlanet=planetNum;
			audio.PlayOneShot((AudioClip)Resources.Load("zoom"));
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
	
		//audio = audio.GetComponent<AudioClip>();
		audio = audio.GetComponent<AudioSource >();

		planetDescriptionCanvas= planetDescriptionCanvas.GetComponent<Text>();
		planetNameCanvas= planetNameCanvas.GetComponent<Text>();

		start = start.GetComponent<CanvasGroup>();
		select = select.GetComponent<CanvasGroup>();
		deny = deny.GetComponent<CanvasGroup>();

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
		//0 - Cantina
		//1 - Junk Yard
		//2 - Space Ship
		completedPlanet[1]=GlobalManager.instance.victories[0];
		completedPlanet[2]=GlobalManager.instance.victories[2];
		completedPlanet[3]=GlobalManager.instance.victories[1];

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
		if(denyingLevel)
		{
			if(deny.alpha<=1)
				deny.alpha+= zoomSpeed/2000;
			else 
			{
				deny.alpha=1;
				denyingLevel=false;
			}

		}
//		else if(!denyingLevel)
//		{
//			if(deny.alpha>=0)
//				deny.alpha-= zoomSpeed/2000;
//			else 
//			{
//				deny.alpha=0;
//			
//			}
//		}

	}
	void OnMouseDown() 
	{
		deny.alpha=0;

		if(!completedPlanet[planetNum])
		{	
		
			ZoomIn(cameras[planetNum]);


		}
		else 
			
		{
			denyingLevel=true;
			audio.PlayOneShot((AudioClip)Resources.Load("occur_sound"));
		}

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
