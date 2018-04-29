/// @description WallSlide state for player

sprite_index = sPlayerJump;


dir = kRight - kLeft;
wall = wallLeft - wallRight;
if(wall != 0){
	facing = wall;
	lastWall = wall;
}


if((wallLeft - wallRight) == dir && alarm[1] < 0 && !sticky) {
	alarm[1] = stickyBuffer;
	sticky = true;
}

if((wallLeft && kRight && alarm[1] < 0) || (wallRight && kLeft && alarm[1] < 0)) {
	vx = 1 * facing;
	sticky = false;
	stateSelect("Jump")
	exit;
}

if(kJumpPressed) {
	if(sticky) {
		vy = -jumpHeight * 1.1;
		vx = vxMaxCur * 1.5 * dir;
	}
	else {
		vy = -jumpHeight * 1.1;
		vx = vxMaxCur * 1.1 * facing;
	}
	sticky = false;
	stateSelect("Jump");
	exit;
}

if(kJumpReleased && vy < 0){
	vy *= 0.5;
	exit;
}

if (vy < 0) {
	vy = approach(vy, vyMax, g);
}
else {
	vy = approach(vy, vyMax, gWall);
}

if(!wallLeft && !wallRight){
	sticky = false;	
	alarm[2] = wallJumpBuffer;
	stateSelect("Jump");
	exit;
}

if(isGrounded){
	if(kLeft || kRight){
		stateSelect("Run");	
	}
	else {
		stateSelect("Idle");	
	}
	sticky = false
	alarm[1] = -1;
	exit;
}
