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
    float distanceFromTarget = 10;
    float rotationSmoothTime = 0.12f;
    Vector3 currentRotation;
    Vector3 rotationSmoothVelocity = Vector3.zero;
    GameObject playerObject;


    float currentX = 0;
    float currentY = 0;

    float walkingRadius = 0.5f;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag(StringCollection.PLAYER);
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
            

            currentX += Input.GetAxis(StringCollection.MOUSE_X) * sensitivityX;
            //currentX += Input.GetAxis(StringCollection.JOYSTICK_X) * sensitivityX;

            currentY += Input.GetAxis(StringCollection.MOUSE_Y) * -sensitivityY;
            //currentY += Input.GetAxis(StringCollection.JOYSTICK_Y) * -sensitivityY; Because Axis not setup yet

            currentY = Mathf.Clamp(currentY, yMin, yMax);
        }
    }
    

    private void LateUpdate()
    {
        /*
        if (playerObject != null)
        {
            horizontalRotation += Input.GetAxisRaw(StringCollection.MOUSE_X) * sensitivityX;
            verticalRotation -= Input.GetAxisRaw(StringCollection.MOUSE_Y) * sensitivityY;
            verticalRotation = Mathf.Clamp(verticalRotation, yMin, yMax);

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(verticalRotation, horizontalRotation), ref rotationSmoothVelocity, rotationSmoothTime);

            transform.eulerAngles = currentRotation;

            transform.position = playerObject.transform.position - transform.forward * distanceFromTarget;
        }
        */
        
        if (playerObject.transform != null)
        {
            Vector3 targetPosition = playerObject.transform.position + Quaternion.Euler(currentY, currentX, 0) * new Vector3(0, 0, -distanceFromTarget);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref rotationSmoothVelocity, 0.1f);

            transform.LookAt(playerObject.transform);
        }
        
    }
}
