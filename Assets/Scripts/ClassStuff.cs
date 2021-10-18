using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ClassStuff : MonoBehaviour, IPointerClickHandler {

    private Camera maincamera;
    private bool activated = false;
    private SpriteRenderer circle;
    private evenmoreclass camerascript;

    public void Awake() {
        maincamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        camerascript = maincamera.GetComponent<evenmoreclass>();
        circle = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (Vector2.Distance(maincamera.ScreenToWorldPoint(Input.mousePosition), transform.position) < 2) {
            if (activated) {
                camerascript.changeactivated = -1;
                circle.enabled = false;
            } else {
                camerascript.changeactivated = 1;
                circle.enabled = true;
            }
            activated = !activated;
        }
    }
}
