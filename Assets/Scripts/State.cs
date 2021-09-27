using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {
    public List<(Func<bool>, State)> Transitions;

    public virtual void Awake() {
        Transitions = new List<(Func<bool>, State)>();
    }

    public void AddTransition<StateType>(Func<bool> condition) where StateType : State {
        var target = GetComponent<StateType>();
        Transitions.Add((condition, target));
    }


    public void LateUpdate() {
        foreach ((var condition, var target) in Transitions) {
            if (condition()) {
                target.enabled = true;
                enabled = false;
                return;
            }
        }
    }
}