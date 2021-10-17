using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    protected Transform birdbase;
    protected List<Transform> targets = new List<Transform>();

    public void onMade(Transform birdbase, Transform enemyHQ) {
        targets.Add(enemyHQ);
    }

    public void Update() {
        if (targets.Count > 0) {
            transform.position = Vector2.Lerp(transform.position, targets[0].position, Time.deltaTime);
        }
    }
}
