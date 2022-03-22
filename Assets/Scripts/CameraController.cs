using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
    [Range(0, 360)]
    public float position;
    public float radius;
    public Slider slider;

    private void Update() {
        Vector3 desiredPos = new Vector3(radius * Mathf.Cos(position * 2 * Mathf.PI / 360), transform.position.y, radius * Mathf.Sin(position * 2 * Mathf.PI / 360));
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, 0.9f * Time.deltaTime);
        transform.position = smoothedPos;
    }

    public void updatePosition() {
        position = slider.value;
    }
}
