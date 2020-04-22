using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float BOOST_MAX = 100; // amount of max boost player can have
    private const float BOOST_MIN = 0; // min amount of boost player can have
    private float hAxis, vAxis;
    private int pushAmount; // jump boost amount
    private Rigidbody rb;
    public ParticleSystem refillEffect;

    public float moveSpeed = 5;
    public float boostCooldown = 2; // amount of seconds for boost refill cooldown
    public float boostForce = 15;

    [Header("Debug")]
    public bool isBoosting;
    public bool isRefilling;
    public float boostAmount = 100; // amount of current boost player has
    private float boostPerSecond = 0.070f;
    private float boostDepletionPerSecond = 0.250f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        isBoosting = false;
        isRefilling = false;
    }

    // Update is called once per frame
    void Update()
    {
        // GETING WASD INPUT
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        Boost();
    }

    void FixedUpdate()
    {
        //assuming we only using the single camera:
        var camera = Camera.main;

        //camera forward and right vectors:
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * vAxis + right * hAxis;

        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * moveSpeed * Time.deltaTime);
    }

    void Boost()
    {
        // while not refilling boost
        if (!isRefilling)
        {
            // check if clicked to boost
            if (Input.GetButton("Jump"))
            {
                // deplete boost while boosting
                isBoosting = true;
                boostAmount -= boostDepletionPerSecond;
                Vector3 force = new Vector3(0, boostForce, 0);
                rb.AddForce(force, ForceMode.Acceleration);

                if (boostAmount <= BOOST_MIN)
                {
                    isRefilling = true;
                    if (refillEffect != null)
                    {
                        ParticleSystem refillEffectObject = Instantiate(refillEffect, transform.position, transform.rotation);
                        refillEffectObject.transform.parent = gameObject.transform;
                        Destroy(refillEffectObject, 2);
                    } else {
                        Debug.Log("PlayerMovement>> Refill particle missing!");
                    }

                    StartCoroutine(BoostRefill());
                }
                isBoosting = false;
            }
            else if (boostAmount < BOOST_MAX && !isRefilling)
            {
                boostAmount += boostPerSecond;
                if (boostAmount > BOOST_MAX) // fix boost overflow
                    boostAmount = BOOST_MAX;
            }
        }
    }

    IEnumerator BoostRefill()
    {
        yield return new WaitForSeconds(boostCooldown);
        Debug.Log("Finished Reloading");

        boostAmount = 100;

        isRefilling = false;
        isBoosting = false;

    }
}
