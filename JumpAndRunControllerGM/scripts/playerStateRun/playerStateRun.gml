/// @description @description Run state for player

sprite_index = sPlayerRun;

//if(kLeft){ kTimeLeft++; } else { kTimeLeft = 0; }
//if(kRight){ kTimeRight++; } else { kTimeLeft = 0; }

//if(kTimeLeft && (!kTimeRight || (kTimeLeft < kTimeRight))){
//	facing = -1;
//	dir = -1;
//}
//else if(kTimeRight && (!kTimeLeft || (kTimeRight < kTimeLeft))){
//	facing = 1;
//	dir = 1;
//}

dir = kRight - kLeft;
if(dir != 0){
	facing = dir;
}
if(sign(dir) != sign(vx) && kRun){
	vx *= 0.75;
	vx = approach(vx, vxMaxCur * dir, acc);
}
else {
	vx = approach(vx, vxMaxCur * dir, acc);	
}


if(!kLeft && !kRight){
	stateSelect("Idle");
	image_index = 0;
	exit;
}
if(kJumpPressed && isGrounded){
	vy = -jumpHeight;
	stateSelect("Jump");
	image_index = 0;
	exit;
}
if(!isGrounded){
	alarm[0] = jumpBuffer;
	stateSelect("Jump");
	image_index = 0;
	exit;
}
