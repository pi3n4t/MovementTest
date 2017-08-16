using System.Collections;
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

    private bool isGrounded;
    private bool jumpPressed;
    private bool isJumping;
    private bool canDoubleJump;

    private bool isDashing;
    private float chargeTime;
    private float dashDistance = 25;
    private const float MIN_CHARGE = 0.5f;
    private const float MAX_CHARGE = 1.5f;
    private const float DASH_DURATION = 0.2f;

    private RaycastHit floorInfo;
    private float angleOfFloor;
    Quaternion playerMovementRotation;

    private void Start()
    {
        playerRigid = gameObject.GetComponent<Rigidbody>();
        cameraTransform = Camera.main.gameObject.transform;
        playerRenderer = gameObject.GetComponent<Renderer>();
        currentMovementSpeed = baseMovementSpeed;
    }

    private void Update()
    {
        if (Input.GetButtonDown(StringCollection.JUMP))
            jumpPressed = true;
        if(Input.GetButtonUp(StringCollection.JUMP))
            jumpPressed = false;

        isGrounded = GroundCheck();
        angleOfFloor = (int)Vector3.Angle(floorInfo.normal, Vector3.up);

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
        movement = new Vector3(movement.x, newDirection.y, movement.z);

        if (canMove)
        {
            playerRigid.velocity = new Vector3(movement.x * currentMovementSpeed, playerRigid.velocity.y, movement.z * currentMovementSpeed);
            parallelToGround = Vector3.Cross(Vector3.Cross(floorInfo.normal, playerRigid.velocity), floorInfo.normal);
            playerRigid.velocity = new Vector3(parallelToGround.x, playerRigid.velocity.y, parallelToGround.z);

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
        Vector3 extents = new Vector3(transform.localScale.x / 2 - wallGroundcheckOffset, 0, transform.localScale.z / 2 - wallGroundcheckOffset); //On CubeCharacter do not divide by 2
        float halfHeight = transform.localScale.y; //On CubeCharacter this needs to be /2, because of the form
        ExtDebug.DrawBoxCastBox(transform.position, extents, transform.rotation, -transform.up, halfHeight, Color.red);
        return Physics.BoxCast(transform.position, extents, -transform.up, out floorInfo, transform.rotation, halfHeight + groundCheckSpacing, -1, QueryTriggerInteraction.Ignore);
    }

    private IEnumerator ChargedDash(float charge)
    {
        float currentDuration = 0.0f;

        playerRigid.velocity = Vector3.zero;
       
        while (currentDuration < DASH_DURATION)
        {
            parallelToGround = Vector3.Cross(Vector3.Cross(floorInfo.normal, transform.forward), floorInfo.normal);
            playerRigid.velocity = (parallelToGround) * charge * (dashDistance / DASH_DURATION);
            currentDuration += Time.deltaTime;
            yield return null;
        }
        playerRigid.velocity = Vector3.zero;

        isDashing = false;
        canMove = true;
    }
}