using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WanderingState : EnemyState {
    [SerializeField]
    private Vector3[] wanderPoints;
    private int currentWanderGoal;

    public override void Start() {
        base.Start();

        AddTransition<SeekLightState>(() => FindObjectsOfType<LightSwitch>().Any(ls => ls.IsOn));
    }

    public override void Update() {
        base.Update();

        Wander();
        Seek();
    }

    private void Wander() {
        if (wanderPoints.Length == 0) {
            return;
        }

        Vector3 direction = (wanderPoints[currentWanderGoal] - transform.position).normalized * 2f * Time.deltaTime;
        direction.y = 0;
        transform.forward = direction;
        transform.position += direction;

        Vector3 normalizedPosition = transform.position;
        normalizedPosition.y = 0f;
        if (Vector3.Distance(normalizedPosition, wanderPoints[currentWanderGoal]) <= 0.2f) {
            currentWanderGoal = currentWanderGoal + 1 >= wanderPoints.Length ? 0 : currentWanderGoal + 1;
        }
    }

    private void Seek() {
        PlayerState player = FindObjectsOfType<PlayerState>()
            .Where(ps => ps.enabled)
            .FirstOrDefault();

        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;
        float angle = Vector3.Angle(transform.forward, direction.normalized);

        if(angle <= 45 && distance <= 8f) {
            player.Die(this);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        foreach(Vector3 point in wanderPoints) {
            Gizmos.DrawSphere(point, 0.2f);
        }
    }
}