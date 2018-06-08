using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class MosquitoController : MonoBehaviour
{
    // speed
    public float speed = 0.5f;

    // minimum distance from us
    public float minDistance = 0.55f;

    // VR interactive item
    VRInteractiveItem vrIntItem;

    // rigidbody
    Rigidbody rb;

    // flag to keep track whether this  mosquito is moving
    bool isMoving = true;

    // target
    Vector3 target;

    // Use this for initialization
    void Awake()
    {
        // grab my VR interactive component
        vrIntItem = GetComponent<VRInteractiveItem>();

        // grab the rigidbody component
        rb = GetComponent<Rigidbody>();

        // set target
        target = Camera.main.transform.position;

        // make the mosquito look at us
        transform.LookAt(target);
    }

    // when our game object is enabled
    void OnEnable()
    {
        vrIntItem.OnClick += HandleClick;
    }

    // when our game object is disabled
    void OnDisable()
    {
        vrIntItem.OnClick -= HandleClick;
    }

    // this is called when the mosquito is clicked on
    private void HandleClick()
    {
        if (rb.isKinematic)
        {
            // rotate it's trasform
            transform.Rotate(Vector3.forward, 180);

            // disabled kinematic property
            rb.isKinematic = false;

            // set the falg to false
            isMoving = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check that we are moving
        if (isMoving)
        {
            // calculate distance from target
            float distance = Vector3.Distance(transform.position, target);

            // check min distance
            if (distance <= minDistance)
            {

            }
            else
            {
                // calculate movement step: v = d / t --> d = v * t
                float movementStep = speed = Time.deltaTime;

                // move on that step
                transform.position = Vector3.MoveTowards(transform.position, target, movementStep);
            }
        }
    }
}
