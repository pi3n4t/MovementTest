﻿using System.Collections;
using UnityEngine;

public class PlayerMovement : Creature
{
    private float baseMovementSpeed = 10;
    private float currentMovementSpeed;
    private float maxSpeed = 15;
    private float rotationSpeed = 0.65f;
    private bool canMove = true;
    private Vector3 parallelToGround;

    private float jumpPower = 20;
    private float airControlPercentage = 0.5f;
    private float gravityMultiplier = 4.5f;
    private float groundCheckSpacing = 0.1f;
    private float wallGroundcheckOffset = 0.05f;

    Rigidbody playerRigid;
    Transform cameraTransform;
    Renderer playerRenderer;
    CapsuleCollider playerCollider;

    private bool isGrounded;
    private bool jumpPressed;
    private bool isJumping;
    private bool canDoubleJump;

    private bool isDashing;
    private float chargeTime;
    private float dashSpeed = 35;
    private const float MIN_CHARGE = 0.75f;
    private const float MAX_CHARGE = 1.5f;
    private const float DASH_DURATION = 0.25f;

    private RaycastHit floorInfo;
    private float angleOfFloor;
    Quaternion playerMovementRotation;

    [SerializeField]
    bool dash = false;

    private void Start()
    {
        playerRigid = gameObject.GetComponent<Rigidbody>();
        cameraTransform = Camera.main.gameObject.transform;
        playerRenderer = gameObject.GetComponent<Renderer>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        currentMovementSpeed = baseMovementSpeed;
    }

    private void Update()
    {
        if (Input.GetButtonDown(StringCollection.JUMP))
            jumpPressed = true;
        if(Input.GetButtonUp(StringCollection.JUMP))
            jumpPressed = false;

        isGrounded = GroundCheck();
        

        if (isJumping && jumpPressed && canDoubleJump)
        {
            canDoubleJump = false;
            jumpPressed = false;
            currentMovementSpeed = baseMovementSpeed;
            Jump(jumpPower);
        }

        if (isGrounded)
        {
            currentMovementSpeed = baseMovementSpeed;
            isJumping = false;
            canDoubleJump = true;
        }
        else
        {
            isJumping = true;
        }

        if (isGrounded && jumpPressed)
        {
            Jump(jumpPower);
            currentMovementSpeed = baseMovementSpeed * airControlPercentage;
            isJumping = true;
            jumpPressed = false;
        }

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 2, 0);
            playerRigid.velocity = Vector3.zero;
        }

        Vector3 right = cameraTransform.right;
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;

        Vector3 newDirection = (forward * Input.GetAxisRaw(StringCollection.VERTICAL)) + (right * Input.GetAxisRaw(StringCollection.HORIZONTAL));

        Vector3 movement = newDirection.normalized;
        //movement = new Vector3(movement.x, newDirection.y, movement.z);
        Physics.Raycast(transform.position, -transform.up, out floorInfo, 10);

        if (canMove)
        {
            //playerRigid.velocity = new Vector3(movement.x * currentMovementSpeed, playerRigid.velocity.y, movement.z * currentMovementSpeed);
            parallelToGround = Vector3.Cross(Vector3.Cross(floorInfo.normal, movement), floorInfo.normal);
            movement = (movement + parallelToGround).normalized * currentMovementSpeed;
            movement.y = playerRigid.velocity.y;
            playerRigid.velocity = movement;

            if (movement.magnitude != 0)
            {
                transform.rotation = Quaternion.LookRotation(transform.forward + new Vector3(movement.x, 0, movement.z) * rotationSpeed);
            }
        }      
        //playerRigid.velocity = new Vector3(Mathf.Clamp(playerRigid.velocity.x, -maxSpeed, maxSpeed), playerRigid.velocity.y, Mathf.Clamp(playerRigid.velocity.z, -maxSpeed, maxSpeed));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            chargeTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            canMove = false;
            chargeTime = Mathf.Clamp(chargeTime, MIN_CHARGE, MAX_CHARGE);
            StartCoroutine(ChargedDash(chargeTime));
            chargeTime = 0;
        }

        if (dash)
        {
            dash = false;
            StartCoroutine(ChargedDash(MIN_CHARGE));
        }
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        { 
            playerRigid.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration); //this is "Physics.gravity + (Physics.gravity * gravityMultiplier)", because of the global gravity already in place
        }
    }

    private void Jump(float power)
    {
        playerRigid.velocity = new Vector3(playerRigid.velocity.x, power, playerRigid.velocity.z);
    }

    private bool GroundCheck()
    {
        Vector3 extents = new Vector3(transform.localScale.x / 3 - wallGroundcheckOffset, 0, transform.localScale.z / 3 - wallGroundcheckOffset); //On CubeCharacter do not divide by 2
        float halfHeight = transform.localScale.y; //On CubeCharacter this needs to be /2, because of the form
        ExtDebug.DrawBoxCastBox(transform.position, extents, transform.rotation, -transform.up, halfHeight, Color.red);
        return Physics.BoxCast(transform.position, extents, -transform.up, transform.rotation, halfHeight + groundCheckSpacing, -1, QueryTriggerInteraction.Ignore);
    }

    private IEnumerator ChargedDash(float charge)
    {
        float currentDuration = 0.0f;
        parallelToGround = Vector3.Cross(Vector3.Cross(floorInfo.normal, transform.forward), floorInfo.normal).normalized;
        bool isTargetSpaceOccupied = Physics.BoxCast(transform.position, transform.localScale, transform.forward, transform.rotation, charge * dashSpeed);
        Debug.Log(parallelToGround * charge * dashSpeed);

        while (currentDuration < DASH_DURATION)
        {
            ExtDebug.DrawBoxCastBox(transform.position, transform.localScale / 2, transform.rotation, transform.forward, charge * dashSpeed, Color.green);
            if (currentDuration < 0.15f * DASH_DURATION || currentDuration > 0.85f * DASH_DURATION)
            {
                playerRenderer.enabled = true;
                playerCollider.enabled = true;
            }
            else
            {
                playerRenderer.enabled = false;
                playerCollider.enabled = false;
            }
            playerRigid.velocity = Vector3.zero;
            parallelToGround = Vector3.Cross(Vector3.Cross(floorInfo.normal, transform.forward), floorInfo.normal);
            playerRigid.velocity = parallelToGround * charge * (dashSpeed / DASH_DURATION);
            currentDuration += Time.deltaTime;
            yield return null;
        }
        playerRigid.velocity = Vector3.zero;

        isDashing = false;
        canMove = true;
    }
}