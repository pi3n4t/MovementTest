using UnityEngine;

public class PlayerMovement : Creature
{
    private float movementSpeed = 30; //((movementSpeed-maxSpeed)/2)m/s² = amount of time it takes to reach maxSpeed in seconds; if less than maxSpeed, can't move
    private float currentMovementSpeed;
    private float speedSmoothTime = 0.1f;
    private float turnSmoothTime = 0.1f;
    private float inputHorizontal;
    private float inputVertical;
    private float jumpPower = 10;
    private float airControlPercentage = 0.2f;
    private bool isGrounded;
    private bool jumpPressed;
    private float turnSmoothVelocity;
    private float speedSmoothVelocity;
    Rigidbody playerRigid;
    Transform cameraTransform;

    private float rotationSpeed = 0.20f;
    private float maxSpeed = 10;

    private void Start()
    {
        playerRigid = gameObject.GetComponent<Rigidbody>();
        cameraTransform = Camera.main.gameObject.transform;
    }

    private void Update()
    {
        if (Input.GetButtonDown(StringCollection.JUMP))
            jumpPressed = true;

        isGrounded = GroundCheck();
        Debug.Log(playerRigid.velocity);

        Vector3 extents = new Vector3(transform.localScale.x / 2 - 0.01f, transform.localScale.y / 2, transform.localScale.z / 2 - 0.01f);
        ExtDebug.DrawBoxCastBox(transform.position, extents, transform.rotation, Vector3.down, 0.1f, Color.red);
    }

    private void FixedUpdate()
    {

        /*
        Vector2 inputDirection = (new Vector2(Input.GetAxisRaw(StringCollection.HORIZONTAL), Input.GetAxisRaw(StringCollection.VERTICAL))).normalized;

        if (isGrounded && jumpPressed)
            playerRigid.velocity = new Vector3(playerRigid.velocity.x, jumpPower, playerRigid.velocity.z);

        jumpPressed = false;

        if (inputDirection != Vector2.zero)
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));

        currentMovementSpeed = Mathf.SmoothDamp(currentMovementSpeed, movementSpeed * inputDirection.magnitude, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        playerRigid.velocity = transform.forward * currentMovementSpeed + Vector3.up * playerRigid.velocity.y;

        currentMovementSpeed = (new Vector2(playerRigid.velocity.x, playerRigid.velocity.z)).magnitude;
        */



        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;

        Vector3 newDirection = (forward * Input.GetAxis(StringCollection.VERTICAL)) + (right * Input.GetAxis(StringCollection.HORIZONTAL));

        Vector3 movement = newDirection.normalized;
        //newDirection.y = playerRigid.velocity.y;
        movement = new Vector3(movement.x, newDirection.y, movement.z);

        if (movement.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(transform.forward + new Vector3(movement.x, 0, movement.z).normalized * rotationSpeed);


        playerRigid.AddForce(movement * movementSpeed, ForceMode.Acceleration);
        playerRigid.velocity = new Vector3(Mathf.Clamp(playerRigid.velocity.x, -maxSpeed, maxSpeed), playerRigid.velocity.y, Mathf.Clamp(playerRigid.velocity.z, -maxSpeed, maxSpeed));

        /*
        playerRigid.AddForceAtPosition(movement * movementSpeed, transform.position, ForceMode.Force);

        if(playerRigid.velocity.magnitude > maxSpeed)
        {
            playerRigid.velocity = playerRigid.velocity.normalized * maxSpeed;
        }
        */
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
        Vector3 extents = new Vector3(transform.localScale.x / 2 - 0.01f, 0, transform.localScale.z / 2 - 0.01f);
        return Physics.BoxCast(transform.position, extents, Vector3.down, transform.rotation);
    }
}