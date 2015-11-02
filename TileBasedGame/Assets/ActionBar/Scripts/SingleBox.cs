using UnityEngine;
using System.Collections;

public class SingleBox : MonoBehaviour {

	[SerializeField]
	public float r = 255;

	[SerializeField]
	public float g = 0;

	[SerializeField] 
	public float b = 0;

	[SerializeField] 
	public int x = 50;

	[SerializeField]
	public int y = 35; 

	[SerializeField]
	public int width = 20;

	[SerializeField]
	public int height = 20;

	[SerializeField]
	public float transparency = 0;

	public void SetColor(int i)
	{
		i = i % 7;
		switch (i) 
		{
			case 0: //character 1
			case 5:
				r = 255;
				g = 0;
				b = 0;
				break;
			case 1: //character 2
			case 6:
				r = 0;
				g = 255;
				b = 0;
				break;
			case 2: //character 3
			case 4:
				r = 0;
				g = 0;
				b = 255;
				break;
			case 3: //character 4
				r = 255;
				g = 0;
				b = 255;
				break;
		}
	}

	public void SetBoxSize (int newWidth, int newHeight) {
		width = newWidth;
		height = newHeight;
	}

	public void SetPosition(int newX, int newY) {
		x = newX;
		y = newY;
	}

	void RenderRect()
	{
		Texture2D rgb_texture = new Texture2D (width, height);
		Color rgb = new Color (r, g, b);
		int i, j;
		for (i = 0; i < width; i++) 
		{
			for(j = 0; j < height; j++)
			{
				rgb_texture.SetPixel(i,j,rgb);
			}
		}
		rgb_texture.Apply ();
		GUIStyle generic_style = new GUIStyle ();
		GUI.skin.box = generic_style;
		GUI.Box(new Rect(x,y,width,height), rgb_texture);
	}

	void OnGUI()
	{
		RenderRect ();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
}
