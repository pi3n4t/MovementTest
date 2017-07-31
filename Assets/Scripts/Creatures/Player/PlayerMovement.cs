using UnityEngine;

public class PlayerMovement : Creature
{
    private enum States
    {
        Idle,
        Move,
        Jump
    }
    private States state;

    private float baseMovementSpeed = 40; //((movementSpeed-maxSpeed)/2)m/s² = amount of time it takes to reach maxSpeed in seconds; if less than maxSpeed, can't move; Don't know yet how slopes work
    private float currentMovementSpeed;
    private float maxSpeed = 10;
    private float jumpPower = 10;
    private float airControlPercentage = 0.5f;
    private bool isGrounded;
    private bool jumpPressed;
    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    Rigidbody playerRigid;
    Transform cameraTransform;

    private float rotationSpeed = 0.20f;

    private float groundCheckSpacing = 0.1f;
    RaycastHit floorInfo;
    float gravityMultiplier = 0.5f;
    float angleOfFloor = 0f;
    float wallGroundcheckOffset = 0.01f;

    RaycastHit wallInfo;

    private void Start()
    {
        playerRigid = gameObject.GetComponent<Rigidbody>();
        cameraTransform = Camera.main.gameObject.transform;
        currentMovementSpeed = baseMovementSpeed;
    }

    private void Update()
    {
        Debug.Log(transform.rotation.eulerAngles.y);

        if (Input.GetButtonDown(StringCollection.JUMP))
            jumpPressed = true;
        if(Input.GetButtonUp(StringCollection.JUMP))
            jumpPressed = false;

        isGrounded = GroundCheck();
        angleOfFloor = (int)Vector3.Angle(floorInfo.normal, Vector3.up);

        switch (state){
            case States.Idle:
                //Code für Idleanimation etc.
                break;
            case States.Jump:
                //Code für Jumpanimation, movement *= 0.5f, canAttack = false, etc.
                break;
            default:
                //Idleanimation
                break;
        }

    }

    private void FixedUpdate()
    {
        Vector3 right = cameraTransform.right;
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;

        Vector3 newDirection = (forward * Input.GetAxis(StringCollection.VERTICAL)) + (right * Input.GetAxis(StringCollection.HORIZONTAL));

        Vector3 movement = newDirection.normalized;
        movement = new Vector3(movement.x, newDirection.y, movement.z);

        if (movement.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(transform.forward + new Vector3(movement.x, 0, movement.z) * rotationSpeed);
        }

        playerRigid.AddForce(movement * (currentMovementSpeed + angleOfFloor), ForceMode.Acceleration);

        if (!isGrounded)
            playerRigid.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration); //this is "Physics.gravity + (Physics.gravity * gravityMultiplier)", because of the global gravity already in place

        playerRigid.velocity = new Vector3(Mathf.Clamp(playerRigid.velocity.x, -maxSpeed, maxSpeed), playerRigid.velocity.y, Mathf.Clamp(playerRigid.velocity.z, -maxSpeed, maxSpeed));

        if (isGrounded && jumpPressed)
        {
            playerRigid.velocity = new Vector3(playerRigid.velocity.x, jumpPower, playerRigid.velocity.z);
            currentMovementSpeed *= airControlPercentage;
        }
        else if(isGrounded)
        {
            currentMovementSpeed = baseMovementSpeed;
        }
        
    }

    private float GetModifiedSmoothTime(float smoothTime)
    {
        if (isGrounded)
            return smoothTime;

        if (airControlPercentage == 0)
            return float.MaxValue;

        return smoothTime / airControlPercentage;
    }

    private bool GroundCheck()
    {
        Vector3 extents = new Vector3(transform.localScale.x / 2 - wallGroundcheckOffset, 0, transform.localScale.z / 2 - wallGroundcheckOffset);
        return Physics.BoxCast(transform.position, extents, Vector3.down, out floorInfo, transform.rotation, (transform.localScale.y / 2) + groundCheckSpacing);
    }
}