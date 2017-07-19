using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class PlayerMovementController : PlayerCommonController{

    PlayerMovementData movementData;

    private void Start()
    {
        movementData = new PlayerMovementData();
        PlayerCommonData.CanMove = true;
    }

    private void Update()
    {
        movementData.IsGrounded = GroundCheck();
        //Debug.Log(movementData.IsGrounded);
        if (Input.GetKeyDown("space") && movementData.IsGrounded)
        {
            movementData.PlayerRigid.AddForce(Vector3.up * movementData.JumpPower, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        Vector3 forward = movementData.CameraObject.transform.forward;
        Vector3 right = movementData.CameraObject.transform.right;
        forward.y = 0f;

        Vector3 newDirection = (forward * movementData.InputZ) + (right * movementData.InputX);
        movementData.Movement = newDirection.normalized;
        newDirection.y = movementData.VerticalVelocity;
        movementData.Movement = new Vector3(movementData.Movement.x, newDirection.y, movementData.Movement.z);

        if (movementData.Movement.magnitude != 0 && PlayerCommonData.CanMove)
            transform.rotation = Quaternion.LookRotation(transform.forward + new Vector3(movementData.Movement.x, 0, movementData.Movement.z) * movementData.SmoothRotation);

        movementData.PlayerRigid.AddForce(movementData.Movement * movementData.MovementSpeed, ForceMode.Acceleration);
        movementData.PlayerRigid.velocity = new Vector3(Mathf.Clamp(movementData.PlayerRigid.velocity.x, -10, 10), movementData.PlayerRigid.velocity.y, 
                                                                                                                    Mathf.Clamp(movementData.PlayerRigid.velocity.z, -10, 10));
    }

    private bool GroundCheck()
    {
        return true;  
      //Vector3 extents = new Vector3(movementData.PlayerCollider.bounds.extents.x - 0.01f, movementData.PlayerCollider.bounds.extents.y + 0.01f, movementData.PlayerCollider.bounds.extents.z - 0.01f);
      //return Physics.BoxCast(transform.localPosition, extents, Vector3.down, transform.rotation, (transform.localScale.y / 2) + movementData.GroundCheckSpacing);
    }
}
*/