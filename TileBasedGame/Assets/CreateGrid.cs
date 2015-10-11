using UnityEngine;
using System.Collections;

public class CreateGrid : MonoBehaviour {

    public GameObject tile;

    private float tileH = 0.1039212f;
    private float HexMaxCirc = 2 / Mathf.Sqrt(3);
    private Vector3 NE,NW;

    public Material redmat;

    public GameObject cursor;

	// Use this for initialization
	void Start () {
        NE = new Vector3(1f/2f, 0, 1.5f/Mathf.Sqrt(3));
        NW = new Vector3(-1f / 2f, 0, 1.5f / Mathf.Sqrt(3));
        //NE.Normalize();

        /*
        for (int i = 0; i < 5; ++i)
        {
            Instantiate(tile, Vector3.right * i, Quaternion.identity);
        }
        for (int i = 0; i < 5; ++i)
        {
            Instantiate(tile, Vector3.right * i+NE, Quaternion.identity);
        }
        for (int i = 0; i < 5; ++i)
        {
            Instantiate(tile, Vector3.right * i + NE+NW, Quaternion.identity);
        }
        */
        Vector3 row = new Vector3();
        for(int i = 0; i < 10; ++i)
        {
            for(int j = 0; j < 10; ++j)
            {
                if (Random.value <= 0.2f)
                    continue;
                GameObject go = (GameObject)Instantiate(tile, row + Vector3.right * j, Quaternion.identity);
                if(Random.value <= 0.3f)
                {
                    go.GetComponent<MeshRenderer>().material = redmat;
                }
                if(Random.value <= 0.1f)
                {
                    GameObject g = (GameObject)Instantiate(tile, row + Vector3.right * j + Vector3.up * tileH, Quaternion.identity);
                    if (Random.value <= 0.3f)
                    {
                        g.GetComponent<MeshRenderer>().material = redmat;
                    }
                }
            }
            row += NE;
            for (int j = 0; j < 10; ++j)
            {
                if (Random.value <= 0.2f)
                    continue;
                GameObject go = (GameObject)Instantiate(tile, row + Vector3.right * j, Quaternion.identity);
                if (Random.value <= 0.3f)
                {
                    go.GetComponent<MeshRenderer>().material = redmat;
                }
                if (Random.value <= 0.1f)
                {
                    GameObject g = (GameObject)Instantiate(tile, row + Vector3.right * j + Vector3.up * tileH, Quaternion.identity);
                    if (Random.value <= 0.3f)
                    {
                        g.GetComponent<MeshRenderer>().material = redmat;
                    }
                }
            }
            row += NW;
        }

    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,11001111))
        {
            //hit.collider.gameObject.GetComponent<MeshRenderer>().material = redmat;
            cursor.transform.position = hit.collider.gameObject.transform.position + Vector3.up * 0.1f;
        }
	}
}
