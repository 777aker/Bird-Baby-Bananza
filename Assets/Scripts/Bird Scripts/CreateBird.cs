using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Random = System.Random;

public enum Teams {
    Grey,
    Yellow,
    Green,
    Blue,
    White
}

public class CreateBird : MonoBehaviour {
    [SerializeField] private Transform spawn;
    [SerializeField] private List<GameObject> birds;
    
    [SerializeField] private Camera maincamera;
    
    [SerializeField] private Tilemap clickableTilemap;
    private Vector3Int[,] clickableArea;
    private BoundsInt bounds;

    [SerializeField] private Tilemap zones;

    private void Start() {
        birdcheck();
        clickableTilemap.CompressBounds();
        bounds = clickableTilemap.cellBounds;
        CreateGrid();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (clickable(GridPositionOfMouse2D)) {
                //Debug.Log(zones.GetSprite(GridPositionOfMouse3D));
                GameObject bird = Instantiate(birds[UnityEngine.Random.Range(0, birds.Count)], spawn);
                Bird birdcom = bird.GetComponent<Bird>();
                Teams team;
                switch (zones.GetSprite(GridPositionOfMouse3D).name) {
                    case "blowharder_341":
                        team = Teams.White;
                        break;
                    case "blowharder_1216":
                        team = Teams.Green;
                        break;
                    case "blowharder_1232":
                        team = Teams.Blue;
                        break;
                    case "blowharder_1986":
                        team = Teams.Yellow;
                        break;
                    case "blowharder_1995":
                        team = Teams.Grey;
                        break;
                    default:
                        return;
                }
                birdcom.makeBird(maincamera.ScreenToWorldPoint(Input.mousePosition), spawn, team);
            }
        }
    }

    private bool clickable(Vector2Int click) {
        for (int i = 0; i < bounds.size.x; i++) {
            for (int j = 0; j < bounds.size.y; j++) {
                if (clickableArea[i, j][0] == click.x && clickableArea[i, j][1] == click.y) {
                    if (clickable(i, j)) {
                        makeunclickable(i, j);
                        return true;
                    }
                    return false;
                }
            }
        }

        return false;
    }

    private bool clickable(int i, int j) {
        int imin;
        int imax;
        int jmin;
        int jmax;
        if (bounds.size.x - 1 != i)
            imax = 1;
        else
            imax = 0;
        if (i > 0)
            imin = -1;
        else
            imin = 0;
        if (bounds.size.y - 1 != j)
            jmax = 1;
        else
            jmax = 0;
        if (j > 0)
            jmin = -1;
        else
            jmin = 0;
        if (clickableArea[i, j][2] != 0)
            return false;

        if (clickableArea[i + imax, j][2] != 0)
            return false;
        if (clickableArea[i + imax, j + jmax][2] != 0)
            return false;
        if (clickableArea[i + imax, j + jmin][2] != 0)
            return false;

        if (clickableArea[i + imin, j][2] != 0)
            return false;
        if (clickableArea[i + imin, j + jmax][2] != 0)
            return false;
        if (clickableArea[i + imin, j + jmin][2] != 0)
            return false;

        if (clickableArea[i, j + jmax][2] != 0)
            return false;
        if (clickableArea[i, j + jmin][2] != 0)
            return false;
        return true;
    }
    
    private void makeunclickable(int i, int j) {
        int imin;
        int imax;
        int jmin;
        int jmax;
        if (bounds.size.x - 1 != i)
            imax = 1;
        else
            imax = 0;
        if (i > 0)
            imin = -1;
        else
            imin = 0;
        if (bounds.size.y - 1 != j)
            jmax = 1;
        else
            jmax = 0;
        if (j > 0)
            jmin = -1;
        else
            jmin = 0;
        clickableArea[i, j][2] = 1;
        
        clickableArea[i + imax, j][2] = 1;
        clickableArea[i + imax, j + jmax][2] = 1;
        clickableArea[i + imax, j + jmin][2] = 1;
        
        clickableArea[i + imin, j][2] = 1;
        clickableArea[i + imin, j + jmax][2] = 1;
        clickableArea[i + imin, j + jmin][2] = 1;

        clickableArea[i, j + jmax][2] = 1;
        clickableArea[i, j + jmin][2] = 1;
    }

    public void makeclickable(int i, int j) {
        int imin;
        int imax;
        int jmin;
        int jmax;
        if (bounds.size.x - 1 != i)
            imax = 1;
        else
            imax = 0;
        if (i > 0)
            imin = -1;
        else
            imin = 0;
        if (bounds.size.y - 1 != j)
            jmax = 1;
        else
            jmax = 0;
        if (j > 0)
            jmin = -1;
        else
            jmin = 0;
        clickableArea[i, j][2] = 0;
        
        clickableArea[i + imax, j][2] = 0;
        clickableArea[i + imax, j + jmax][2] = 0;
        clickableArea[i + imax, j + jmin][2] = 0;
        
        clickableArea[i + imin, j][2] = 0;
        clickableArea[i + imin, j + jmax][2] = 0;
        clickableArea[i + imin, j + jmin][2] = 0;

        clickableArea[i, j + jmax][2] = 0;
        clickableArea[i, j + jmin][2] = 0;
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
