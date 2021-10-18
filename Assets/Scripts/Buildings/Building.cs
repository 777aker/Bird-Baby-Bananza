using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Building : MonoBehaviour {
    [SerializeField] protected int health = 100;
    protected CreateBird birdspawner;
    protected int[] clickablepoint = new int[2];
    protected Transform birdsbase;
    [SerializeField] protected GameObject unit;
    [SerializeField] protected Teams team;
    [SerializeField] protected float spawntime;
    protected List<Transform> HQs = new List<Transform>();
    [SerializeField] private bool HQ = false;

    protected void createdone() {
        GameObject birdbase = GameObject.Find("BirdSpawn");
        birdspawner = birdbase.GetComponent<CreateBird>();
        birdsbase = birdbase.transform;
        if (HQ)
            return;
        switch (team) {
            case Teams.Blue:
                GameObject base1 = GameObject.FindWithTag("GreenBase");
                if (base1 != null)
                    HQs.Add(base1.transform);
                GameObject base2 = GameObject.FindWithTag("YellowBase");
                if(base2 != null)
                    HQs.Add(base2.transform);
                break;
            case Teams.Green:
                GameObject base3 = GameObject.FindWithTag("WhiteBase");
                if (base3 != null)
                    HQs.Add(base3.transform);
                GameObject base4 = GameObject.FindWithTag("BlueBase");
                if(base4 != null)
                    HQs.Add(base4.transform);
                break;
            case Teams.Grey:
                GameObject base5 = GameObject.FindWithTag("WhiteBase");
                if (base5 != null)
                    HQs.Add(base5.transform);
                GameObject base6 = GameObject.FindWithTag("YellowBase");
                if(base6 != null)
                    HQs.Add(base6.transform);
                break;
            case Teams.White:
                GameObject greenBase = GameObject.FindWithTag("GreenBase");
                if (greenBase != null)
                    HQs.Add(greenBase.transform);
                GameObject greyBase = GameObject.FindWithTag("GreyBase");
                if(greyBase != null)
                    HQs.Add(greyBase.transform);
                break;
            case Teams.Yellow:
                GameObject base7 = GameObject.FindWithTag("GreyBase");
                if (base7 != null)
                    HQs.Add(base7.transform);
                GameObject base8 = GameObject.FindWithTag("BlueBase");
                if(base8 != null)
                    HQs.Add(base8.transform);
                break;
            default:
                Destroy(gameObject);
                break;
        }

        StartCoroutine(spawnUnits());
    }

    IEnumerator myawake() {
        yield return new WaitForSeconds(.5f);
        createdone();
    }

    public void onCreate(int i, int j, Teams Team) {
        clickablepoint[0] = i;
        clickablepoint[1] = j;
        team = Team;
        StartCoroutine(myawake());
    }

    public void takeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            if (clickablepoint[0] == -10000 || clickablepoint[1] == -10000) {
                birdspawner.gameover();
            } else {
                birdspawner.makeclickable(clickablepoint[0], clickablepoint[1]);
                Destroy(gameObject);
            }
        }
    }

    private void Awake() {
        if (HQ) {
            onCreate(-10000, -10000, team);
        }
    }

    IEnumerator spawnUnits() {
        while (true) {
            //Debug.Log("did it");
            GameObject unitSpawned = Instantiate(unit, transform.position, Quaternion.identity);
            unitSpawned.GetComponent<Unit>().onMade(birdsbase, HQs[UnityEngine.Random.Range(0, HQs.Count)]);
            yield return new WaitForSeconds(spawntime);
        }
    }
}
