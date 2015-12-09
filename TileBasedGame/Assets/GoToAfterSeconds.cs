using UnityEngine;
using System.Collections;

public class GoToAfterSeconds : MonoBehaviour {

    public float time;
    public int destination;

	// Use this for initialization
	void Start () {
        StartCoroutine(gototask(time));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator gototask(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(destination);
        yield break;
    }
}
