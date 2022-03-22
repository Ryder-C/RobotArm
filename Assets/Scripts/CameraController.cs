using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject camera;
    [Range(0, 360)]
    public float position;
    public float radius;
    public Transform origin;

    void Update() {
        Vector3 desiredPos = new Vector3(radius * Mathf.Cos(position * 2 * Mathf.PI / 360), camera.transform.position.y, radius * Mathf.Sin(position * 2 * Mathf.PI / 360));
        Vector3 smoothedPos = Vector3.Lerp(camera.transform.position, desiredPos, 0.5f * Time.deltaTime);
        camera.transform.position = smoothedPos;
    }
}
