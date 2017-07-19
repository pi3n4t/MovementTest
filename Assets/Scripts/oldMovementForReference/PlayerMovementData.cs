using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementData : PlayerCommonData {

    public float MovementSpeed { get { return 50; } }

    public bool IsGrounded { get; set; }

    public float JumpPower { get { return 10f; } }

    public float Gravity { get { return 7.5f; } }

    public float VerticalVelocity { get; set; }

    public Collider PlayerCollider { get { return PlayerObject.transform.GetComponent<Collider>(); } }

    public float SmoothRotation { get { return 0.15f; } }

    public float GroundCheckSpacing { get { return 0.1f; } }

    private bool canDoubleJump = false;
    public bool CanDoubleJump { get { return canDoubleJump; } set { canDoubleJump = value; } }

    public float InputX { get { return Input.GetAxis("Horizontal"); } }

    public float InputZ { get { return Input.GetAxis("Vertical"); } }

    public Vector3 Movement { get; set; }

    private bool onIce = false;
    public bool OnIce { get { return onIce; } set { onIce = value; } }

    public float SlideStrength { get { return 1.0f; } }
}