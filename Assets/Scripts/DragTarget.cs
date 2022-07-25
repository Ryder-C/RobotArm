using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTarget : MonoBehaviour {

    public Transform returnPos;
    public Transform center;

    private float yOffset;
    private float xOffset;
    private float zOffset;
    
    private Vector3 mOffset;
    private float mZCoord;

    private void Start() {
        xOffset = transform.position.x - returnPos.position.x;
        yOffset = transform.position.y - returnPos.position.y;
        zOffset = transform.position.z - returnPos.position.z;
        // Debug.Log(string.Format("xOffset: {0}, yOffset: {1}, zOffset: {2}", xOffset, yOffset, zOffset));
    }

    private void OnMouseDown() {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp() {
        // gameObject.transform.position = new Vector3(returnPos.position.x + xOffset, returnPos.position.y + yOffset, returnPos.position.z + zOffset);
    }

    private void OnMouseDrag() {
        // Vector3 newPos = GetMouseWorldPos() + mOffset;
        // if(Vector2.Distance(new Vector2(newPos.x, newPos.z), new Vector2(center.position.x, center.position.z)) >= 2f) {
        //     gameObject.transform.position = newPos;
        // }
        gameObject.transform.position = GetMouseWorldPos() + mOffset;
    }

    private Vector3 GetMouseWorldPos() {
        // Pixel coords (x, y)
        Vector3 mousePoint  = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
