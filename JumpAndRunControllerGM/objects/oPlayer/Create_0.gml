event_inherited();

//Movement/Physics
	g = 0.5;
	gWall = 0.1;
	acc = 0.35;
	jumpHeight = 8;
	
	vxMax = 3;
	vxMaxRun = 5;
	vxMaxCur = vxMax;
		
	vyMax = 9;
	
	fric = 0.75;
	airFriction = 0.2;
		
	stickyBuffer = 12;
	jumpBuffer = 6;
	wallJumpBuffer = 6;
	
	isGrounded = false;
	wallLeft = false;
	wallRight = false;
	
//Important vars

	dir = 1;
	facing = 1;
	sticky = false;
	wall = 0;
	lastWall = 0;
	
	kTimeLeft = 0;
	kTimeRight = 0;
//StateMachine
	stateMachineCreate();

	stateAdd("Idle", playerStateIdle);
	stateAdd("Run", playerStateRun);
	stateAdd("Jump", playerStateJump);
	stateAdd("WallSlide", playerStateWallSlide);

	stateSelect("Idle");

//Controls

    kLeft = false;
    kRight = false;
    kRun = false;
    kJump = false;
    kJumpReleased = false;