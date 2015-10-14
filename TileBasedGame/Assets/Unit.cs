using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public int MoveRange = 3;

    Tile tile;
    List<Tile> list = new List<Tile>();

    HashSet<Tile> reachableTiles = new HashSet<Tile>();

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (tile == null)
        {
            int i = 0;
            while (tile == null)
                tile = GameManager.instance.tiles[i++][0];
            transform.position = tile.gameObject.transform.position + Vector3.up;
            reachableTiles = GameManager.instance.TilesInRange(tile, MoveRange);
        }

        if (Input.GetMouseButtonDown(0) && GameManager.instance.selected != null)
        {
            if(reachableTiles.Contains(GameManager.instance.selected))           
              move(GameManager.instance.selected);
        }
        if(list != null && list.Count > 0)
        {
            if ((list[0].gameObject.transform.position + Vector3.up - transform.position).sqrMagnitude < 0.05f)
            {
                tile = list[0];
                list.RemoveAt(0);
                if(list.Count == 0)
                {
                    /*
                    foreach(Tile t in GameManager.instance.TilesInRange(tile, 3))
                    {
                        t.transform.position += Vector3.up * 3;
                    }*/
                    reachableTiles = GameManager.instance.TilesInRange(tile, MoveRange);
                }
            }
            else
            {
                Vector3 dist = list[0].gameObject.transform.position + Vector3.up - transform.position;
                dist = dist.normalized * Time.deltaTime;
                transform.position += dist;
               // transform.position = Vector3.Lerp(transform.position, list[0].gameObject.transform.position + Vector3.up, Time.deltaTime*10);
            }
        }
	}

    void move(Tile t)
    {
        list = GameManager.instance.FindPath(tile, t);
    }
}
