using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCoordinates : MonoBehaviour {
    public GameObject target;
    public GameObject origin;
    public GameObject carriage;
    public GameObject leftClaw;
    public GameObject rightClaw;

    public GameObject firstSpin;
    public GameObject secondSpin;

    // [Range(0f, 0.03f)]
    public float clawValue;

    private Vector3 originPos;

    void Start() {
        clawValue = 0f;
        originPos = origin.transform.position;
    }

    void Update() {
        Networker network = target.GetComponent<Networker>();

        firstSpin.transform.localRotation = Quaternion.Euler(0, network.getRoatations().x, 0);
        secondSpin.transform.localRotation = Quaternion.Euler(0, network.getRoatations().y - 32, 0);
        carriage.transform.position = new Vector3(0, network.getRoatations().z + 1, 0);

        leftClaw.transform.localPosition = new Vector3(-clawValue + 0.03599095f, 0.0276095f, 0.03376855f);
        rightClaw.transform.localPosition = new Vector3(clawValue + 0.04085615f, 0.02767568f, 0.03384988f);
    }

    public void setClawValue(float value) {
        clawValue = value;
    }
}
