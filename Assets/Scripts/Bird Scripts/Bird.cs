using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
    public Vector2 target = Vector2.negativeInfinity;
    public Transform hq = null;
    public bool returning = false;
    public float speed = 5;

    public virtual void makeBird(Vector2 Target, Transform HQ) {
        if (target == Vector2.negativeInfinity && hq == null) {
            target = Target;
            hq = HQ;
        }
    }
    
    public virtual void Update() {
        fly();
    }

    public virtual void fly() {
        Vector2 flyto;
        if (returning)
            flyto = hq.position;
        else
            flyto = target;
        Quaternion myRot = transform.rotation;
        Quaternion rot =
            Quaternion.LookRotation(Vector3.forward, flyto - new Vector2(transform.position.x, transform.position.y));
        Quaternion newRotation = Quaternion.Slerp(myRot, rot, Time.deltaTime*speed/2);
        transform.rotation = newRotation;
        transform.position += transform.up * Time.deltaTime*speed;
        if (Vector2.Distance(flyto, transform.position) < 1) {
            if(returning)
                Destroy(gameObject);
            returning = !returning;
        }
    }

}
