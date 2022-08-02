using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour {
    public GameObject robotController;

    public GameObject mainCam;
    public GameObject sceneCam;
    public GameObject topCam;

    public GameObject mainCanv;
    public GameObject sceneCanv;


    void Start() {
        mainCam.SetActive(false);
        sceneCam.SetActive(true);
        mainCanv.SetActive(false);
        sceneCanv.SetActive(true);

        robotController.GetComponent<CameraControllerBlender>().enabled = false;
    }

    public void Transition() {
        mainCam.SetActive(true);
        sceneCam.SetActive(false);
        mainCanv.SetActive(true);
        sceneCanv.SetActive(false);

        robotController.GetComponent<CameraControllerBlender>().enabled = true;
    }

    public void TransitionTopCam() {
        topCam.SetActive(true);
        sceneCam.SetActive(false);
    }
}
