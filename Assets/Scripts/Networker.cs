using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networker : MonoBehaviour {
    private Transform target;
    private Stream s;
    private Vector3 coords;

    void Start() {
        target = GetComponent<Transform>();
        coords = target.localPosition;

        TcpClient client = new TcpClient("localhost", 1023);
        s = client.GetStream();
        
        Vector3 newPos = UpdateArm();
    }

    void Update() {
        coords = UpdateArm();
    }

    Vector3 UpdateArm() {
        s.Write(BitConverter.GetBytes(coords.x * 10f), 0, 4);
        s.Write(BitConverter.GetBytes(coords.z * 10f), 0, 4);
        s.Write(BitConverter.GetBytes(-80f * (coords.y - 0.75f) + 160), 0, 4);

        byte[] xbuff = new byte[4];
        byte[] ybuff = new byte[4];
        byte[] zbuff = new byte[4];
        s.Read(xbuff, 0, 4);
        s.Read(zbuff, 0, 4);
        s.Read(ybuff, 0, 4);
        return new Vector3(BitConverter.ToSingle(xbuff, 0), BitConverter.ToSingle(ybuff, 0), BitConverter.ToSingle(zbuff, 0));
   }
}
