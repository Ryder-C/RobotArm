using System;
using System.IO;
using TMPro;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Networker : MonoBehaviour {
    private DragTarget drag;
    private Stream s;
    private Vector3 coords;

    public GameObject sceneController;
    public GameObject robotController;

    public Button connectButton;
    private TextMeshProUGUI buttonText;

    private TcpClient client;
    private CalculateCoordinates calculateCoordinates;

    private float rot1 = 52;
    private float rot2;
    private float z = 2;
    private float r = 0;

    void Start() {
        drag = GetComponent<DragTarget>();
        coords = GetComponent<Transform>().localPosition;
        calculateCoordinates = robotController.GetComponent<CalculateCoordinates>();
        buttonText = connectButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartConnection() {
        connectButton.interactable = false;
        try {
            client = new TcpClient("localhost", 1023);
        } catch(SocketException) {
            buttonText.text = "Connection failed";
            connectButton.interactable = true;
            return;
        }
        s = client.GetStream();
        sceneController.GetComponent<CameraSwitcher>().Transition();
        UpdateArm(false);
    }

    void Update() {
        if(client != null && client.Connected) {
            coords = GetComponent<Transform>().localPosition;
            UpdateArm(drag.getDragging());
        }
    }

    public Vector3 getRoatations() {
        return new Vector3(rot1, rot2, r);
    }

    public float getZ() {
        return z;
    }

    void UpdateArm(bool dragging) {
        Debug.Log(dragging);
        if(dragging) {
            s.Write(BitConverter.GetBytes(true), 0, 1);
            s.Write(BitConverter.GetBytes(coords.x * 10f), 0, 4);
            s.Write(BitConverter.GetBytes(-coords.z * 10f), 0, 4);
            s.Write(BitConverter.GetBytes(-80f * (coords.y - 0.75f) + 160), 0, 4);
        } else {
            s.Write(BitConverter.GetBytes(false), 0, 1);
        }
        s.Write(BitConverter.GetBytes((int)(3333.3333 * calculateCoordinates.clawValue)), 0, 4);

        byte[] m1 = new byte[4];
        byte[] m2 = new byte[4];
        byte[] zbuff = new byte[4];
        byte[] rbuff = new byte[4];
        s.Read(m1, 0, 4);
        s.Read(m2, 0, 4);
        s.Read(zbuff, 0, 4);
        s.Read(rbuff, 0, 4);
        rot1 = BitConverter.ToSingle(m1, 0) * -180f / Mathf.PI;  
        rot2 = BitConverter.ToSingle(m2, 0) * -180f / Mathf.PI;
        z = -BitConverter.ToSingle(zbuff, 0) / 80f + 2.75f;
        r = BitConverter.ToSingle(rbuff, 0) * 180f / Mathf.PI;
   }
}
