using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenvaVR
{
    public class DragCamera : MonoBehaviour
    {
#if UNITY_EDITOR
        // rate at which camera turns
        public float speed = 1;

        // flag to keep track whether we are dragging or not
        bool isDragging = false;

        // starting point of a camera movement
        float startMouseX;
        float startMouseY;

        // Camera component
        Camera cam;

        // Use this for initialization
        void Start()
        {
            cam = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            // if we press the left button and we havnen't started dragging
            if (Input.GetMouseButtonDown(1) && !isDragging)
            {
                // set the flag to true
                isDragging = true;

                // save the mouse starting position
                startMouseX = Input.mousePosition.x;
                startMouseY = Input.mousePosition.y;
            }
            // if we are not pressing the left button, and we were dragging
            else if (Input.GetMouseButtonUp(1) && isDragging)
            {
                // set the flag to false
                isDragging = false;
            }
        }

        // LateUpdate is called once per frame after Update
        void LateUpdate()
        {
            // Check if we are dragging
            if (isDragging)
            {
                // Calculate current mouse position
                float endMouseX = Input.mousePosition.x;
                float endMouseY = Input.mousePosition.y;

                // Difference
                float diffX = (endMouseX - startMouseX) * speed;
                float diffY = (endMouseY - startMouseY) * speed;

                // New center of the screen
                float newCenterX = Screen.width / 2f + diffX;
                float newCenterY = Screen.height / 2f + diffY;

                // Get the world coordinate, this is where we should be looking at
                Vector3 lookHerePoint = cam.ScreenToWorldPoint(new Vector3(newCenterX, newCenterY, cam.nearClipPlane));

                // Make our camera look at the lookHerePoint
                transform.LookAt(lookHerePoint);

                // starting position for the next call
                startMouseX = endMouseX;
                startMouseY = endMouseY;
            }
        }
        #endif
    }
}
