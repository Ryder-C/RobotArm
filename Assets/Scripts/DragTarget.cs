using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTarget : MonoBehaviour {

    public Transform returnPos;
    
    private Vector3 mOffset;
    private float mZCoord;

    private void OnMouseDown() {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp() {
        gameObject.transform.position = returnPos.position;
    }

    private void OnMouseDrag() {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private Vector3 GetMouseWorldPos() {
        // Pixel coords (x, y)
        Vector3 mousePoint  = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
