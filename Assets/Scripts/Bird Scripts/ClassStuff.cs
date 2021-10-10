using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClassStuff : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        transform.position += new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0);
        
    }
}
