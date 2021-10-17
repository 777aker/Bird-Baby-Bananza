using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit : MonoBehaviour {
    protected Transform birdbase;
    protected List<Transform> targets = new List<Transform>();

    private Vector3Int[,] walkableArea;
    private Astar astar;
    private BoundsInt bounds;

    private Tilemap walkableTilemap;

    private float movespeed = .1f;

    private List<Spot> path;

    Vector2Int Tileposition {
        get {
            return (Vector2Int) walkableTilemap.WorldToCell(transform.position);
        }
    }

    Vector2Int TargetTilePosition {
        get {
            if(targets.Count > 0)
                return (Vector2Int) walkableTilemap.WorldToCell(targets[0].position);
            return (Vector2Int) walkableTilemap.WorldToCell(birdbase.position);
        }
    }

    void awaken() {
        walkableTilemap = GameObject.FindWithTag("walkableTM").GetComponent<Tilemap>();
        walkableTilemap.CompressBounds();
        bounds = walkableTilemap.cellBounds;

        CreateGrid();
        astar = new Astar(walkableArea, bounds.size.x, bounds.size.y);
        path = astar.CreatePath(walkableArea, Tileposition, (Vector2Int)walkableTilemap.WorldToCell(targets[0].position), false);
        StartCoroutine(move());
    }

    IEnumerator move() {
        for (int i = 0; i < path.Count; i++) {
            //while (Tileposition.x != path[i].X || Tileposition.y != path[i].Y) {
            transform.position = new Vector2(path[i].X, path[i].Y);
            yield return new WaitForSeconds(movespeed);
            //}
        }
    }

    private void CreateGrid() {
        walkableArea = new Vector3Int[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                if (walkableTilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    walkableArea[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    walkableArea[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }
    
    public void onMade(Transform birdbase, Transform enemyHQ) {
        targets.Add(enemyHQ);
        StartCoroutine(start());
    }

    IEnumerator start() {
        yield return new WaitForSeconds(.01f);
        awaken();
    }

    /*
    public void Update() {
        
        if (targets.Count > 0) {
            transform.position = Vector2.Lerp(transform.position, targets[0].position, Time.deltaTime);
        }
    }*/
}
