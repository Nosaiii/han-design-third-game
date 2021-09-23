using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State {
    [HideInInspector]
    public bool hasKilled;

    public virtual void Start() {
        AddTransition<PlayerState>(() => hasKilled);
    }
}
