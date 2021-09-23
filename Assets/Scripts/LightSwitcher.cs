using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class LightSwitcher : MonoBehaviour {
    private Camera m_camera;

    private Vector2 mousePosition;

    private void Awake() {
        m_camera = GetComponent<Camera>();
    }

    public void OnMouseMove(InputAction.CallbackContext context) {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnClickLightSwitch() {
        Ray ray = m_camera.ScreenPointToRay(mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) {
            return;
        }

        if(!hit.collider.TryGetComponent(out LightSwitch lightSwitch)) {
            return;
        }

        if(!lightSwitch.InDistance) {
            return;
        }

        lightSwitch.TurnLightOn();
    }
}
