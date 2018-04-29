/// @description @description Jump state for player

sprite_index = sPlayerJump;


dir = kRight - kLeft;
if(dir != 0){
	facing = dir;
}


if(wallLeft || wallRight){
	stateSelect("WallSlide");
	exit;
}

if(kLeft || kRight){
	if(sign(dir) != sign(vx) && kRun){
		vx *= 0.8;
		vx = approach(vx, vxMaxCur * dir, acc);
	}
	else {
		vx = approach(vx, vxMaxCur * dir, acc);	
	}
} 
else {
	vx = approach(vx, 0, airFriction);	
}

vy = approach(vy, vyMax, g);

if(kJumpPressed && alarm[0] >= 0){
	vy = -jumpHeight;	
}
if(kJumpPressed && alarm[2] >= 0){
		vy = -jumpHeight * 1.1;
		vx = vxMax * 1.2 * lastWall;	
}
if(kJumpReleased && vy < 0){
	vy *= 0.5;
	exit;
}

if(isGrounded){
	if(kLeft || kRight){
		stateSelect("Run");	
	}
	else {
		stateSelect("Idle");	
	}
	exit;
}
