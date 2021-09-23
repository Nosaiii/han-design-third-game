using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SeekLightState : EnemyState {
    private LightSwitch lookAtTarget;

    public override void Start() {
        base.Start();

        AddTransition<WanderingState>(() => !lookAtTarget.IsOn);

        lookAtTarget = FindObjectsOfType<LightSwitch>()
            .Where(ls => ls.IsOn)
            .FirstOrDefault();

        Vector3 lookDirection = lookAtTarget.transform.position - transform.position;
        lookDirection.y = 0;
        transform.forward = lookDirection;
    }
}