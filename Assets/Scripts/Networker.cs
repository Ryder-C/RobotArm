using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networker : MonoBehaviour {
    private GameObject target;
    private Stream s;
    private Vector3 coords;

    private float rot1;
    private float rot2;
    private float z;

    void Start() {
        coords = target.GetComponent<Transform>().localPosition;

        TcpClient client = new TcpClient("localhost", 1023);
        s = client.GetStream();
    }

    void Update() {
        UpdateArm(target.GetComponent<DragTarget>().getDragging());
    }

    public Vector3 getRoatations() {
        return new Vector3(rot1, rot2, z);
    }

    void UpdateArm(bool dragging) {
        if(dragging) {
            s.Write(BitConverter.GetBytes(coords.x * 10f), 0, 4);
            s.Write(BitConverter.GetBytes(coords.z * 10f), 0, 4);
            s.Write(BitConverter.GetBytes(-80f * (coords.y - 0.75f) + 160), 0, 4);
        } else {
            for(int i = 0; i < 4; i++) {
                s.Write(new byte[4], 0, 4);
            }
        }

        byte[] m1 = new byte[4];
        byte[] m2 = new byte[4];
        byte[] zbuff = new byte[4];
        s.Read(m1, 0, 4);
        s.Read(m2, 0, 4);
        s.Read(zbuff, 0, 4);
        rot1 = BitConverter.ToSingle(m1, 0);
        rot2 = BitConverter.ToSingle(m2, 0);
        z = -BitConverter.ToSingle(zbuff, 0) / 80f + 2.75f;
   }
}
