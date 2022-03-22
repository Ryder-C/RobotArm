using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTarget : MonoBehaviour {
    public Camera cam;
    public Transform target;

    private bool down;

    private void Start() {
        down = false;
    }

    private void Update() {
        if(Input.GetMouseButton(0)) {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Target") {
                Transform objectHit = hit.transform;
                Debug.DrawRay(ray.origin, ray.direction * 15, Color.blue);
                objectHit.position = hit.point;

            } else {
                Debug.DrawRay(ray.origin, ray.direction * 15, Color.red);
            }
        }  else if(Input.GetMouseButtonUp(0)) {
            down = false;
        }
    }
}
