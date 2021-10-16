using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class CreateBird : MonoBehaviour {
    [SerializeField] private Transform spawn;
    [SerializeField] private List<GameObject> birds;
    
    [SerializeField] private Camera maincamera;
    
    [SerializeField] private Tilemap clickableTilemap;
    private Vector3Int[,] clickableArea;
    private BoundsInt bounds;

    private void Start() {
        birdcheck();
        clickableTilemap.CompressBounds();
        bounds = clickableTilemap.cellBounds;
        CreateGrid();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (clickable(clickableArea, GridPositionOfMouse2D)) {
                GameObject bird = Instantiate(birds[UnityEngine.Random.Range(0, birds.Count)], spawn);
                Bird birdcom = bird.GetComponent<Bird>();
                birdcom.target = maincamera.ScreenToWorldPoint(Input.mousePosition);
                birdcom.hq = spawn;
            }
        }
    }
    
    private bool clickable(Vector3Int[,] grid, Vector2Int click) {
        for (int i = 0; i < bounds.size.x; i++) {
            for (int j = 0; j < bounds.size.y; j++) {
                if (grid[i, j][0] == click.x && grid[i, j][1] == click.y) {
                    return grid[i, j][2] == 0;
                }
            }
        }

        return false;
    }

    private void CreateGrid() {
        clickableArea = new Vector3Int[bounds.size.x,bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++) {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++) {
                if (clickableTilemap.HasTile(new Vector3Int(x, y, 0))) {
                    clickableArea[i, j] = new Vector3Int(x, y, 0);
                } else {
                    clickableArea[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }

    private Vector3Int GridPositionOfMouse3D {
        get {
            return clickableTilemap.WorldToCell(maincamera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    private Vector2Int GridPositionOfMouse2D => (Vector2Int) GridPositionOfMouse3D;

    void birdcheck() {
        foreach (GameObject obj in birds) {
            if (obj.GetComponent<Bird>() == null) {
                Debug.Log("Non bird in birds: " + obj.name);
            }
        }
    }
}
