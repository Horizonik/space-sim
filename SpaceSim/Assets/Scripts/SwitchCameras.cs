using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    [Header("First Person Camera")]
    public Camera firstPerson;

    [Header("Top View Camera")]
    public Camera topView;

    void Start()
    {
        firstPerson.enabled = true;
        topView.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            firstPerson.enabled = !firstPerson.enabled;
            topView.enabled = !topView.enabled;

            // Enable scroll wheel zooming when in top view camera
            if (topView.enabled) {
                topView.GetComponent<ZoomViewOnScroll>().EnableScript();
                Debug.Log("SwitchCameras>> Enabled ZoomViewOnScroll script!");
            } else {
                topView.GetComponent<ZoomViewOnScroll>().DisableScript();
                Debug.Log("SwitchCameras>> Disabled ZoomViewOnScroll script!");
            }
        }
    }
}
