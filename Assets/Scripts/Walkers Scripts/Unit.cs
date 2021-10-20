using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Unit : MonoBehaviour {
    protected Transform birdbase;
    protected Transform HQ;
    protected List<GameObject> targets = new List<GameObject>();

    private Vector3Int[,] walkableArea;
    private Astar astar;
    private BoundsInt bounds;

    private Tilemap walkableTilemap;

    [SerializeField] private float movespeed = 1f;
    [SerializeField] private float attackspeed = 1f;
    [SerializeField] private float attackdamage = 1f;
    [SerializeField] private float health = 10f;
    [SerializeField] private Animator enemyController;

    private IEnumerator moveco;
    private IEnumerator attackco;
    
    private List<Spot> path;

    [SerializeField] private Animator movementController;

    private evenmoreclass soulcollector;

    Vector2Int Tileposition {
        get {
            return (Vector2Int) walkableTilemap.WorldToCell(transform.position);
        }
    }

    Vector2Int TargetTilePosition {
        get {
            if(targets.Count > 0)
                return (Vector2Int) walkableTilemap.WorldToCell(targets[0].transform.position);
            return (Vector2Int) walkableTilemap.WorldToCell(birdbase.position);
        }
    }

    void awaken() {
        soulcollector = GameObject.FindWithTag("MainCamera").GetComponent<evenmoreclass>();
        
        walkableTilemap = GameObject.FindWithTag("walkableTM").GetComponent<Tilemap>();
        walkableTilemap.CompressBounds();
        bounds = walkableTilemap.cellBounds;

        CreateGrid();
        astar = new Astar(walkableArea, bounds.size.x, bounds.size.y);
        path = astar.CreatePath(walkableArea, Tileposition, (Vector2Int)walkableTilemap.WorldToCell(HQ.position), false);
        //StartCoroutine(move());
    }

    public void resumemove() {
        StartCoroutine(moveco);
    }

    public void stopmove() {
        StopCoroutine(moveco);
    }

    public void resumeattack() {
        StartCoroutine(attackco);
    }

    public void stopattack() {
        StopCoroutine(attackco);
    }

    IEnumerator move() {
        if (Tileposition != (Vector2Int) walkableTilemap.WorldToCell(HQ.position)) {
            for (int i = 0; i < path.Count; i++) {
                if (Tileposition.x > path[i].X)
                    movementController.SetInteger("direction", 1);
                if (Tileposition.x < path[i].X)
                    movementController.SetInteger("direction", 3);
                if (Tileposition.y > path[i].Y)
                    movementController.SetInteger("direction", 0);
                if (Tileposition.y < path[i].Y)
                    movementController.SetInteger("direction", 2);
                //while (Tileposition.x != path[i].X || Tileposition.y != path[i].Y) {
                transform.position = new Vector2(path[i].X, path[i].Y);
                yield return new WaitForSeconds(movespeed);
                //}
            }
        }
    }

    void takeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            soulcollector.died();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("sup");
        if (other.gameObject.layer != gameObject.layer) {
            //Debug.Log("collided");
            targets.Add(other.gameObject);
            enemyController.SetInteger("targets", targets.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (targets.Contains(other.gameObject)) {
            targets.Remove(other.gameObject);
            enemyController.SetInteger("targets", targets.Count);
        }
    }

    IEnumerator attack() {
        while (targets.Count > 0) {
            if(targets[0].tag == "Unit")
                targets[0].GetComponent<Unit>().takeDamage(attackdamage);
            else
                targets[0].GetComponent<Building>().takeDamage((int)attackdamage*10);
            yield return new WaitForSeconds(attackspeed);
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
        moveco = move();
        attackco = attack();
        HQ = enemyHQ;
        StartCoroutine(start());
    }

    IEnumerator start() {
        yield return new WaitForSeconds(.01f);
        awaken();
    }
    
}
