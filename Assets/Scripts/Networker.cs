using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networker : MonoBehaviour {
    public Transform target;
    private Stream s;
    private Vector3 coords;

    void Start() {
        coords = target.localPosition;

        TcpClient client = new TcpClient("localhost", 1023);
        s = client.GetStream();
        
        UpdateArm();
    }

    void Update() {
        if(target.localPosition != coords) {
            coords = target.localPosition;
            UpdateArm();
        }
    }

    void UpdateArm(){
        s.Write(BitConverter.GetBytes(coords.x), 0, 4);
        s.Write(BitConverter.GetBytes(coords.z), 0, 4);
        s.Write(BitConverter.GetBytes(coords.y), 0, 4);
    }
}
