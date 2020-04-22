using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamController : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 60.0f;

    private Camera cam;

    [Header("Sensitivities")]
    public float sensitivityX = 4.0f;
    public float sensitivityY = 4.0f;

    [Header("Others")]
    public float distance = 4.0f;
    public Transform lookAt;
    public Transform camTransform;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Get Cam stuff
        camTransform = transform;
        cam = Camera.main;
    }

    private void Update()
    {
        // Get Mouse Inputs
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY += Input.GetAxis("Mouse Y") * sensitivityY;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        ChangeDistance();
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }

    // Change cam distance with mouse wheel
    private void ChangeDistance()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance <= 8)
            distance += 0.25f;

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && distance > 2)
            distance -= 0.25f;
    }
}
