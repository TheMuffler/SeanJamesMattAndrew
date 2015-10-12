using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public int MapH, MapW;
    public Dictionary<GameObject, Tile> tileMap = new Dictionary<GameObject, Tile>();
    public List<List<Tile>> tiles = new List<List<Tile>>();
    public void SetupList(int w, int h)
    {
        MapH = h;
        MapW = w;
        for(int i = 0; i < w; ++i)
        {
            List<Tile> row = new List<Tile>();
            for (int j = 0; j < h; ++j)
                row.Add(null);
            tiles.Add(row);
        }
    }

    public bool inBounds(int x, int y)
    {
        return x >= 0 && x < MapW && y >= 0 && y < MapH;
    }

    public bool tileExists(int x, int y)
    {
        return inBounds(x, y) && tiles[x][y] != null;
    }

    public Tile getTile(int x, int y)
    {
        if (tileExists(x, y))
            return tiles[x][y];
        return null;
    }

    public Tile getTile(Vector2 pos)
    {
        return getTile((int)pos.x, (int)pos.y);
    }

    public Tile getTile(Tile.TilePos pos)
    {
        return getTile(pos.x, pos.y);
    }

    public Tile.TilePos[] directions = { new Tile.TilePos(-1, 0), new Tile.TilePos(1,0), new Tile.TilePos(-1,-1),new Tile.TilePos(-1,1), new Tile.TilePos(0,1),new Tile.TilePos(0,-1)};
    public Tile.TilePos[] offsetDirections = { new Tile.TilePos(-1, 0), new Tile.TilePos(1, 0), new Tile.TilePos(0, 1), new Tile.TilePos(0, -1), new Tile.TilePos(1, 1), new Tile.TilePos(1, -1) };


    public List<Tile> getNeighbors(Tile tile)
    {
        List<Tile> list = new List<Tile>();
        bool offset = tile.IsOffset();
        foreach (Tile.TilePos dir in offset ? offsetDirections : directions)
        {
            Tile t = getTile(tile.gridPos + dir);
            if (t != null)
                list.Add(t);
        }

        return list;
    }

    public void RegisterTile(Tile tile)
    {
        tileMap[tile.gameObject] = tile;
        tiles[tile.gridX][tile.gridY] = tile;
    }

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    private class PathNode
    {
        public PathNode prev = null;
        public Tile tile;

        public float g = 0;
        public float h= 0;
        public float f
        {
            get
            {
                return g + h;
            }
        }

        public PathNode(Tile t, Tile end, PathNode pred = null)
        {
            tile = t;
            h = t.gridPos.distance(end.gridPos);
            if (pred != null)
                setPrev(pred);
            else
               this.g = 0;
        }

        public void setPrev(PathNode prev)
        {
            this.prev = prev;
            g = prev.g + 1;
        }

        public bool isImprovement(PathNode pred)
        {
            return pred.g + 1 <= g;
        }
    }

    public List<Tile> FindPath(Tile start, Tile end)
    {
        if (start == null || end == null)
            return null;

        Dictionary<Tile, PathNode> nodeMap = new Dictionary<Tile, PathNode>();
        List<PathNode> openset = new List<PathNode>();
        HashSet<Tile> closedset = new HashSet<Tile>();

        
        openset.Add(new PathNode(start, end));
        nodeMap[start] = openset[0];

        while(openset.Count > 0)
        {
            openset.Sort(delegate(PathNode a, PathNode b)
            {
                if (a.f == b.f)
                    return 0;
                return a.f < b.f ? -1 : 1;
            });
            PathNode current = openset[0];
            closedset.Add(current.tile);
            openset.RemoveAt(0);
            if(current.tile == end)
            {
                List<Tile> list = new List<Tile>();
                Stack<Tile> stack = new Stack<Tile>();
                while(current != null)
                {
                    stack.Push(current.tile);
                    current = current.prev;
                }
                while(stack.Count != 0)
                {
                    list.Add(stack.Pop());
                }

                return list;
            }

            foreach(Tile tile in getNeighbors(current.tile))
            {
                //if (closedset.Contains(tile))
                //    continue;
                if (nodeMap.ContainsKey(tile))
                {
                    if (nodeMap[tile].isImprovement(current)) {
                        nodeMap[tile].setPrev(current);
                        if (closedset.Contains(tile)) {
                            closedset.Remove(tile);
                            openset.Add(nodeMap[tile]);
                        }
                    } 
                }
                else
                {
                    PathNode node = new PathNode(tile, end, current);
                    nodeMap[tile] = node;
                    openset.Add(node);
                }
            }

        }



        return null;
    }



    public Tile selected;

}
