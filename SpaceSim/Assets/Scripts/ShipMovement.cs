using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    KeyMapper km;
    Rigidbody rb;

    Vector3 thrusterInput;

    [Header("Rotation")]
    public float rotSpeed = 10;
    public float rotSmoothSpeed = 0.2f;

    [Header("Others")]
    public float rollSpeed = 10;
    public float thrustStrength = 5;

    Quaternion targetRot;
    Quaternion smoothRot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // km = GetComponent<KeyMapper>();
        // km.BindDefaultKeys();
    }

    void Update()
    {
        MovementHandler();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Thrusters
        Vector3 thrustDir = transform.TransformVector(thrusterInput);
        rb.AddForce(thrustDir * thrustStrength, ForceMode.Acceleration);
        rb.MoveRotation(smoothRot.normalized);
    }

    // Gets axis of two given buttons
    // int GetInputAxis(KeyCode a, KeyCode b)
    // {
    //     int result = 0;

    //     // if pressing a -> do a
    //     if (Input.GetButton(a.ToString()) & !Input.GetButton(b.ToString()))
    //         result = 1;

    //     // if pressing b -> do b
    //     if (Input.GetButton(b.ToString()) & !Input.GetButton(a.ToString()))
    //         result = -1;

    //     // if pressing both at the same time -> dont do anything
    //     if (Input.GetButton(a.ToString()) & Input.GetButton(b.ToString()))
    //         result = 0;

    //     return result;
    // }

    void MovementHandler()
    {
        // // Thruster Input with KeyMapper
        // int thrustInputX = GetInputAxis(km.leftKey, km.rightKey);
        // int thrustInputY = GetInputAxis(km.descendKey, km.ascendKey);
        // int thrustInputZ = GetInputAxis(km.backwardKey, km.forwardKey);
        // thrusterInput = new Vector3(thrustInputX, thrustInputY, thrustInputZ);

        // // Thruster Input with InputManager
        float thrustInputX = Input.GetAxis("Horizontal");
        float thrustInputY = Input.GetAxis("Vertical");
        float thrustInputZ = Input.GetAxis("ForwardBackward");
        thrusterInput = new Vector3(thrustInputX, thrustInputY, thrustInputZ);

        // Rotation Input
        float yawInput = Input.GetAxisRaw("Mouse X") * rotSpeed;
        float pitchInput = Input.GetAxisRaw("Mouse Y") * rotSpeed;
        float rollInput = Input.GetAxis("Roll") * rollSpeed * Time.deltaTime;
        // GetInputAxis(km.rollLeftKey, km.rollRightKey) ^^^

        // Calculate Rotation
        Quaternion yaw = Quaternion.AngleAxis(yawInput, transform.up);
        Quaternion pitch = Quaternion.AngleAxis(-pitchInput, transform.right);
        Quaternion roll = Quaternion.AngleAxis(-rollInput, transform.forward);

        targetRot = yaw * pitch * roll * targetRot;
        smoothRot = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotSmoothSpeed);
    }
}
