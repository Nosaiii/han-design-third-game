using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerState : State {
    private int health = 1;

    private void Start() {
        AddTransition<DeadState>(() => health <= 0);
    }

    public void Die(EnemyState enemy) {
        health = 0;
        enemy.hasKilled = true;
    }
}