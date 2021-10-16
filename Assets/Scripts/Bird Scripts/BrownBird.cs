using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird : Bird
{
    protected override void arrive() {
        return;
    }
    
    protected override void returned() {
        Destroy(gameObject);
    }
}