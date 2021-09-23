using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State {
    public override void OnEnable() {
        Destroy(gameObject);
    }
}
