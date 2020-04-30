using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    private GameObject objToRotateAround; // position of object in worldspace to which this gameObject will rotate around.
    private Vector3 rotationAxis = new Vector3(0, 5, 0); // axis it will rotate by.
    private float rotationSpeed; // speed of rotation.
    private System.Random rnd = new System.Random();

    void Start() {
        rotationSpeed = Random.Range(1, 100);
    }
    // Sets objToRotateAround to the object inputed
    public void SetTargetObject(GameObject inObj) {
        objToRotateAround = inObj;
    }
    
    void Update()
    {
        // Gets the position of your 'Turret' and rotates this gameObject around it by the 'axis' provided at speed 'angle' in degrees per update 
        transform.RotateAround(objToRotateAround.transform.position, rotationAxis, rotationSpeed/500.5f);
    }
}
