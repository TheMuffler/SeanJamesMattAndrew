using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    private static long idCtr = 0;

    [HideInInspector]
    public float nextTurnTime = 0;
    [HideInInspector]
    public long turnTieBreaker = 0;
    private long _idNum = -1;
    public long ID
    {
        get
        {
            return _idNum;
        }
    }

    [HideInInspector]
    public bool processingCommand = false;

    public static int turnOrderComp(Unit a, Unit b)
    {
        if (a.nextTurnTime < b.nextTurnTime)
            return -1;
        if (a.nextTurnTime > b.nextTurnTime)
            return 1;
        if (a.turnTieBreaker < b.turnTieBreaker)
            return -1;
        if (a.turnTieBreaker > b.turnTieBreaker)
            return 1;
        if (a.ID < b.ID)
            return -1;
        if (a.ID > b.ID)
            return 1;
        return 0;
    }

    public void TurnTick()
    {

    }


    public int MoveRange = 3;
    public float timeForActions = 1f;


    

    // Use this for initialization
    void Awake () {
        if(_idNum < 0)
            _idNum = idCtr++;
	}

    bool initialized = false;
    void Start()
    {
        initialized = true;
        GameManager.instance.units.Add(this);
        if (tile == null)
        {
            int i = 0;
            while (tile == null)
                tile = GameManager.instance.tiles[i++][(int)ID];
            tile.SetUnit(this);
            transform.position = tile.gameObject.transform.position + Vector3.up;
            //reachableTiles = GameManager.instance.TilesInRange(tile, MoveRange);
        }
    }

    void OnDisable()
    {
        GameManager.instance.units.Remove(this);
    }

    void OnEnable()
    {
        if(initialized)
           GameManager.instance.units.Add(this);
    }



    public void RequestCommand()
    {
        if (processingCommand)
            return;
        processingCommand = true;
        //AI controlled units will use a coroutine to decide their moves

    }


    [HideInInspector]
    public Tile tile;
    List<Tile> list = new List<Tile>();

    HashSet<Tile> reachableTiles = new HashSet<Tile>();

    public void CalculateReachableTiles()
    {
        reachableTiles = GameManager.instance.TilesInRange(tile, MoveRange);
    }
	
	// Update is called once per frame
	void Update () {
     
        
        if (!processingCommand)
            return;
      

        if (Input.GetMouseButtonDown(0) && GameManager.instance.selected != null)
        {
            if (reachableTiles.Contains(GameManager.instance.selected))
                move(GameManager.instance.selected);
        }
	}

    void move(Tile t)
    {
        //list = GameManager.instance.FindPath(tile, t);
        GameManager.instance.ProcessCommand(() =>
        {
            GameManager.instance.tasks.Add(new Task_MoveToTile(this, t));
        });
    }
}
