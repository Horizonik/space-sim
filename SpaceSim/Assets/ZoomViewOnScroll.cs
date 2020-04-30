using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomViewOnScroll : MonoBehaviour
{
    public float newZoomAmount;

    // Min and Max amounts zoom can go to
    public float min_zoom = 20;
    public float max_zoom = 700;

    // Amount to zoom per scroll
    public float zoomAmountPerScroll = 10;

    private bool isEnabled = false;
    private Camera cam;

    // Called when switching to top view camera
    public void EnableScript()
    {
        cam = Camera.current; // set camera to top view camera
        newZoomAmount = cam.orthographicSize; // get orthographic size from top view camera
        isEnabled = true;
    }

    // Called when switching to first person camera
    public void DisableScript()
    {
        isEnabled = false;
    }

    void Update()
    {
        if (isEnabled)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && newZoomAmount <= max_zoom)
                newZoomAmount += zoomAmountPerScroll;

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && newZoomAmount > min_zoom)
                newZoomAmount -= zoomAmountPerScroll;

            cam.orthographicSize = newZoomAmount;
        }
    }
}
