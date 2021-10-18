using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class evenmoreclass : MonoBehaviour {

    [SerializeField] private List<GameObject> clickableGO = new List<GameObject>();
    [SerializeField] private List<IPointerClickHandler> clickableObjects = new List<IPointerClickHandler>();
    private int activated = 0;
    private int souls = 0;

    private void Start() {
        foreach (var obj in clickableGO) {
            foreach (MonoBehaviour MB in obj.GetComponents<MonoBehaviour>()) {
                if(MB is IPointerClickHandler)
                    clickableObjects.Add(MB.GetComponent<IPointerClickHandler>());
            }
            
        }
    }

    public void died() {
        souls++;
    }

    public int changeactivated {
        set {
            activated += value;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            foreach (IPointerClickHandler target in clickableObjects) {
                target.OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
    }
}
