using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bird : MonoBehaviour {
    protected Vector2 target;
    protected Transform hq;
    protected bool returning = false;
    protected float speed = 5;
    protected Teams team;

    public void makeBird(Vector2 Target, Transform HQ, Teams Team) {
        target = Target;
        hq = HQ;
        team = Team;
    }
    
    protected virtual void Update() {
        fly();
    }

    protected virtual void fly() {
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
                returned();
            else 
                arrive();
            returning = !returning;
        }
    }

    protected abstract void arrive();

    protected abstract void returned();

}
