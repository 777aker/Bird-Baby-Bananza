using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class CreateBird : MonoBehaviour {
    [SerializeField] private Transform spawn;
    [SerializeField] private List<GameObject> birds;
    [SerializeField] private Camera maincamera;

    private void Start() {
        birdcheck();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject bird = Instantiate(birds[UnityEngine.Random.Range(0, birds.Count)], spawn);
            Bird birdcom = bird.GetComponent<Bird>();
            birdcom.target = maincamera.ScreenToWorldPoint(Input.mousePosition);
            birdcom.hq = spawn;
        }
    }

    void birdcheck() {
        foreach (GameObject obj in birds) {
            if (obj.GetComponent<Bird>() == null) {
                Debug.Log("Non bird in birds");
            }
        }
    }
}
