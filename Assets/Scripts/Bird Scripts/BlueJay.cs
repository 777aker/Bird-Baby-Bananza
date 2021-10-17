using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BlueJay : Bird {
    [SerializeField] private GameObject WhiteBuilding;
    [SerializeField] private GameObject BlueBuilding;
    [SerializeField] private GameObject GreenBuilding;
    [SerializeField] private GameObject YellowBuilding;
    [SerializeField] private GameObject GreyBuilding;

    private void Awake() {
        speed = 10;
    }

    protected override void arrive() {
        switch (team) {
            case Teams.White:
                GameObject building = Instantiate(WhiteBuilding, transform.position, Quaternion.identity);
                building.GetComponent<Building>().onCreate((int)target.x, (int)target.y, Teams.White);
                break;
            case Teams.Blue:
                GameObject building2 = Instantiate(BlueBuilding, transform.position, Quaternion.identity);
                building2.GetComponent<Building>().onCreate((int)target.x, (int)target.y, Teams.Blue);
                break;
            case Teams.Green:
                GameObject building3 = Instantiate(GreenBuilding, transform.position, Quaternion.identity);
                building3.GetComponent<Building>().onCreate((int)target.x, (int)target.y, Teams.Green);
                break;
            case Teams.Grey:
                GameObject building4 = Instantiate(GreyBuilding, transform.position, Quaternion.identity);
                building4.GetComponent<Building>().onCreate((int)target.x, (int)target.y, Teams.Grey);
                break;
            case Teams.Yellow:
                GameObject building5 = Instantiate(YellowBuilding, transform.position, Quaternion.identity);
                building5.GetComponent<Building>().onCreate((int)target.x, (int)target.y, Teams.Yellow);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    protected override void returned() {
        Destroy(gameObject);
    }
}
