using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BlueJay : Bird {
    [SerializeField] private GameObject WhiteBuilding;
    
    protected override void arrive() {
        switch (team) {
            case Teams.White:
                Instantiate(WhiteBuilding, transform.position, Quaternion.identity);
                break;
        }
    }

    protected override void returned() {
        Destroy(gameObject);
    }
}
