using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State {
    private void OnEnable() {
        Destroy(gameObject);
    }
}
