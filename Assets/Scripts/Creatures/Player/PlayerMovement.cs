using UnityEngine;

public class PlayerMovement : Creature
{
    private float baseMovementSpeed = 10;
    private float currentMovementSpeed;
    private float maxSpeed = 15;
    private float rotationSpeed = 0.65f;

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

    private float chargeTime;
    private const float MIN_CHARGE = 0.5f;
    private const float MAX_CHARGE = 1.5f;

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

        if (isJumping && jumpPressed && canDoubleJump)
        {
            canDoubleJump = false;
            jumpPressed = false;
            currentMovementSpeed = baseMovementSpeed;
            playerRigid.velocity = new Vector3(playerRigid.velocity.x, jumpPower, playerRigid.velocity.z);
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
            playerRigid.velocity = new Vector3(playerRigid.velocity.x, jumpPower, playerRigid.velocity.z);
            currentMovementSpeed = baseMovementSpeed * airControlPercentage;
            isJumping = true;
            jumpPressed = false;
        }

        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 2, 0);
        }

        Vector3 right = cameraTransform.right;
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;

        Vector3 newDirection = (forward * Input.GetAxisRaw(StringCollection.VERTICAL)) + (right * Input.GetAxisRaw(StringCollection.HORIZONTAL));

        Vector3 movement = newDirection.normalized;
        movement = new Vector3(movement.x, newDirection.y, movement.z);

        if (movement.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(transform.forward + new Vector3(movement.x, 0, movement.z) * rotationSpeed);
        }

        playerRigid.velocity = new Vector3(movement.x * currentMovementSpeed, playerRigid.velocity.y, movement.z * currentMovementSpeed);
        playerRigid.velocity = new Vector3(Mathf.Clamp(playerRigid.velocity.x, -maxSpeed, maxSpeed), playerRigid.velocity.y, Mathf.Clamp(playerRigid.velocity.z, -maxSpeed, maxSpeed));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            chargeTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            chargeTime = Mathf.Clamp(chargeTime, MIN_CHARGE, MAX_CHARGE);
            ChargedDash(chargeTime);
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

    private bool GroundCheck()
    {
        Vector3 extents = new Vector3(transform.localScale.x / 2 - wallGroundcheckOffset, 0, transform.localScale.z / 2 - wallGroundcheckOffset); //On CubeCharacter do not divide by 2
        float halfHeight = transform.localScale.y; //On CubeCharacter this needs to be /2, because of the form
        ExtDebug.DrawBoxCastBox(transform.position, extents, transform.rotation, Vector3.down, halfHeight, Color.red);
        return Physics.BoxCast(transform.position, extents, Vector3.down, transform.rotation, halfHeight + groundCheckSpacing, -1, QueryTriggerInteraction.Ignore);
    }

    private void ChargedDash(float charge)
    {
        playerRigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        transform.position = transform.position + (transform.forward * (charge * 10));
        Debug.Log(charge);
    }
}