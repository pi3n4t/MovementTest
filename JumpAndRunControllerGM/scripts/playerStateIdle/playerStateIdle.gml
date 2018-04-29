/// @description @description Idle state for player

sprite_index = sPlayerIdle;

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


vx = approach(vx, 0, fric);

if(kLeft || kRight){
	stateSelect("Run");
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
	stateSelect("Jump");
	image_index = 0;
	exit;
}
