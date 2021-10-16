using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YRenderSorter : MonoBehaviour {
    private Renderer renderer;
    private int sortingOrderBase = 5000;
    [SerializeField] private bool runOnce = true;
    [SerializeField] private int offset = 0;

    private void Awake() {
        renderer = gameObject.GetComponent<Renderer>();
    }

    private void LateUpdate() {
        renderer.sortingOrder = (int) (sortingOrderBase - transform.position.y - offset);
        if(runOnce)
            Destroy(this);
    }
}
