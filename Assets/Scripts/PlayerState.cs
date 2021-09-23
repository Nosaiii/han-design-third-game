using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerState : State {
    private int health = 1;
    private Camera mainCamera;
    private Vector3 objectHit;
    private Vector3 position;

    private void Start() {
        AddTransition<DeadState>(() => health <= 0);
        mainCamera = Camera.main;
    }

    public void Die(EnemyState enemy) {
        enemy.hasKilled = true;
        health = 0;
    }
    public override void Update(){
        transform.forward = position;
        transform.position += position;

        Vector3 normalizedPosition = transform.position;
        normalizedPosition.y = 0f;
        
        if (Vector3.Distance(normalizedPosition, objectHit) <= 0.2f) {
            position = Vector3.zero;
        }
    }

    void OnMouse(InputValue input){
        RaycastHit hit;

        Ray ray = mainCamera.ScreenPointToRay(input.Get<Vector2>());
        
        if (Physics.Raycast(ray, out hit)) {
            objectHit = hit.point;
        }

    }

    void OnFire(InputValue input){
        Vector3 direction = (objectHit - transform.position).normalized * 2f * Time.deltaTime;
        direction.y = 0;
        position = direction;
    }
}