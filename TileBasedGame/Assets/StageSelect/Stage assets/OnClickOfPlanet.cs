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

	}
	
	// Update is called once per frame
	void Update () {

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

		ZoomIn(cameras[planetNum]);

	}
	void OnMouseOver()
	{
		Hilight.enabled=true;
	}
	
	void OnMouseExit()
	{
		Hilight.enabled=false;
	}
}
