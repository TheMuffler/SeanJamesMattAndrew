using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Task_MoveToTile : Task
{
    private Unit unit;
    private Tile tile;
    private List<Tile> path;


    public Task_MoveToTile(Unit unit, Tile tile)
    {
        this.unit = unit;
        this.tile = tile;
    }

    public override void OnEnter()
    {
        path = GameManager.instance.FindPath(unit.tile, tile);
    }
    public override bool OnUpdate()
    {
        if (path == null || path.Count == 0)
            return true;
        Vector3 delta = path[0].transform.position - unit.transform.position + Vector3.up;
        if (delta.magnitude < 0.01)
        {
            path.RemoveAt(0);
            if (path.Count == 0)
            {
                tile.SetUnit(unit);
                unit.transform.position = tile.transform.position + Vector3.up;
                return true;
            }
        }
        else
        {
            unit.transform.position += delta.normalized * Mathf.Clamp(0f,Time.deltaTime,delta.magnitude);
        }

        return false;
    }
    public override void OnExit() { }
}