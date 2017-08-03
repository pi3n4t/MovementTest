using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    float inputHorizontal;
    float inputVertical;
    float sensitivityX = 4;
    float sensitivityY = 2;
    float horizontalRotation;
    float verticalRotation;
    float yMin = 15;
    float yMax = 80;
    float distanceFromTarget = 8;
    float rotationSmoothTime = 0.05f;
    Vector3 currentRotation;
    Vector3 rotationSmoothVelocity = Vector3.zero;
    GameObject playerObject;


    float currentX = 0;
    float currentY = 0;

    float walkingRadius = 1.45f; //smaller numbers == bigger radius

    private void Start()
    {
        playerObject = Globals.GetPlayerObject();
    }
    
    private void Update()
    {
        float inputX = Input.GetAxis(StringCollection.HORIZONTAL);

        if (playerObject.transform != null)
        {
            
            if (inputX != 0)
            {
                currentX += inputX * walkingRadius;
            }
            

            currentX += Input.GetAxisRaw(StringCollection.MOUSE_X) * sensitivityX;
            //currentX += Input.GetAxis(StringCollection.JOYSTICK_X) * sensitivityX;

            currentY += Input.GetAxisRaw(StringCollection.MOUSE_Y) * -sensitivityY;
            //currentY += Input.GetAxis(StringCollection.JOYSTICK_Y) * -sensitivityY; Because Axis not setup yet

            currentY = Mathf.Clamp(currentY, yMin, yMax);
        }
        
    }
    

    private void LateUpdate()
    {
        if (playerObject != null)
        {
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(currentY, currentX), ref rotationSmoothVelocity, rotationSmoothTime);

            transform.eulerAngles = currentRotation;

            transform.position = playerObject.transform.position - transform.forward * distanceFromTarget;
        }
    }
}
