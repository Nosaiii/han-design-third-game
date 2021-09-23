using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State {
    [HideInInspector]
    public bool hasKilled;

    private void Start() {
        AddTransition<PlayerState>(() => hasKilled);
    }

    public void KillPlayer(EnemyState enemy) {
        if(enemy != this) {
            return;
        }
        hasKilled = true;
    }
}
