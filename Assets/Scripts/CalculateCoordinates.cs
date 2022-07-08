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

    [Range(0f, 0.03f)]
    public float clawValue;

    private Vector3 originPos;

    private float minRadius = 20f;
    private float l1 = 15f;
    private float l2 = 16f;

    void Start() {
        originPos = origin.transform.position;
    }

    void Update() {
        Vector2 newAngles = cartesionToDualPolar(-target.transform.position.x * 10, target.transform.position.z * 10);
        firstSpin.transform.localRotation = Quaternion.Euler(0, newAngles.x, 0);
        secondSpin.transform.localRotation = Quaternion.Euler(0, newAngles.y - 31f, 0);

        carriage.transform.position = new Vector3(0, Mathf.Clamp(target.transform.position.y + 1, 1.75f, 3.75f), 0);

        leftClaw.transform.localPosition = new Vector3(-clawValue + 0.03599095f, 0.0276095f, 0.03376855f);
        rightClaw.transform.localPosition = new Vector3(clawValue + 0.04085615f, 0.02767568f, 0.03384988f);
    }

    private Vector2 cartesionToDualPolar(float y, float x) {
        float dist = Mathf.Sqrt((Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) - Mathf.Pow(originPos.x, 2) - Mathf.Pow(originPos.z, 2));
        float angle = Mathf.Atan2(y, x);

        if(dist <= minRadius) {
            return cartesionToDualPolar(
                (minRadius + 0.1f) * Mathf.Cos(angle),
                (minRadius + 0.1f) * Mathf.Sin(angle)
            );
        } else if(dist > l1 + l2) {
            return new Vector2(angle * Mathf.Rad2Deg, 0);
        }

        float alpha = Mathf.Acos((dist * dist + l1 * l1 - l2 * l2) / (2 * dist * l1));
        float t2 = Mathf.PI - Mathf.Acos((l1 * l1 + l2 * l2 - dist * dist) / (2 * l1 * l2));

        if(y >= 0) {
            alpha *= -1;
        } else {
            t2 *= -1;
        }

        return new Vector2((angle + alpha) * Mathf.Rad2Deg, t2 * Mathf.Rad2Deg);
    }
}
