using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightSwitch : MonoBehaviour {
    [SerializeField]
    private Light _light;
    [SerializeField]
    private float switchDistance = 8f;
    [SerializeField]
    private float lightTime = 3f;

    public bool IsOn => _light.enabled;

    public bool InDistance {
        get {
            Transform player = FindObjectsOfType<PlayerState>()
                .Where(ps => ps.enabled)
                .Select(ps => ps.transform)
                .FirstOrDefault();
            return Vector3.Distance(player.position, transform.position) <= switchDistance;
        }
    }

    public void TurnLightOn() {
        if(_light.enabled) {
            return;
        }

        _light.enabled = true;
        StartCoroutine(TurnLightOff());
    }

    private IEnumerator TurnLightOff() {
        yield return new WaitForSeconds(lightTime);
        _light.enabled = false;
    }
}