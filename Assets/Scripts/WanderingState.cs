using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WanderingState : EnemyState {
    [SerializeField]
    private Vector3[] wanderPoints;
    private int currentWanderGoal;
    private int PreviousWanderGoal {
        get {
            int index = currentWanderGoal - 1;
            if (index < 0) {
                index = wanderPoints.Length - 1;
            }
            return index;
        }
    }

    [SerializeField]
    private float seekDistance = 5.0f;

    public override void Start() {
        base.Start();

        AddTransition<SeekLightState>(() => FindObjectsOfType<LightSwitch>().Any(ls => ls.IsOn));
    }

    private void Update() {
        Wander();
        Seek();
    }

    private void Wander() {
        if (wanderPoints.Length == 0) {
            return;
        }

        Vector3 direction = (wanderPoints[currentWanderGoal] - transform.position).normalized;
        direction.y = 0;
        transform.forward = direction;

        float totalDistance = Vector3.Distance(wanderPoints[currentWanderGoal], wanderPoints[PreviousWanderGoal]);
        float travelledDistance = Vector3.Distance(transform.position, wanderPoints[PreviousWanderGoal]);
        float interpolationTime = 1f / totalDistance * travelledDistance;
        interpolationTime += 0.01f * Time.deltaTime;
        interpolationTime = Mathf.Min(interpolationTime, 1f);

        if (interpolationTime < 1f) {
            Vector3 interpolatedPosition = Vector3.Lerp(wanderPoints[PreviousWanderGoal], wanderPoints[currentWanderGoal], interpolationTime);
            interpolatedPosition.y = transform.position.y;
            transform.position = interpolatedPosition;
        } else {
            currentWanderGoal++;
            if (currentWanderGoal == wanderPoints.Length) {
                currentWanderGoal = 0;
            }
        }
    }

    private void Seek() {
        PlayerState player = FindObjectsOfType<PlayerState>()
            .Where(ps => ps.enabled)
            .FirstOrDefault();

        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction.normalized);

        if(angle <= 45) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, seekDistance);
            if (colliders.Contains(player.GetComponent<Collider>())) {
                player.Die(this);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        foreach(Vector3 point in wanderPoints) {
            Gizmos.DrawSphere(point, 0.2f);
        }

        Gizmos.color = new Color(255, 255, 255, 0.2f);
        Gizmos.DrawSphere(transform.position, seekDistance);
    }
}