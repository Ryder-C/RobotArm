using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerBlender : MonoBehaviour {
    public GameObject topCam;
    public GameObject mainCam;

    public float radius;
    private float xPos;
    private float yPos;

    private void Start() {
        yPos = mainCam.transform.position.y;
    }

    private void Update() {
        if (Input.GetMouseButton(1)) {
            topCam.SetActive(false);
            mainCam.SetActive(true);
            xPos = (xPos + Input.GetAxis("Mouse X") * 10) % 360;
            yPos += Input.GetAxis("Mouse Y") * 2;
            yPos = Mathf.Clamp(yPos, 0.3f, 8.5f);
            // Debug.Log(yPos);
        }
        radius += Input.mouseScrollDelta.y;
        radius = Mathf.Clamp(radius, 1.5f, 15f);
        Vector3 desiredPos = new Vector3(radius * Mathf.Cos(xPos * 2 * Mathf.PI / 360), yPos, radius * Mathf.Sin(xPos * 2 * Mathf.PI / 360));
        // Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, 0.9f * Time.deltaTime);
        mainCam.transform.position = desiredPos;
    }
}
