isGrounded = place_meeting(x, y + 1, oBlock);
wallLeft = place_meeting(x - 1, y, oBlock);
wallRight = place_meeting(x + 1, y, oBlock);

kLeft = keyboard_check(vk_left);
kRight = keyboard_check(vk_right);
kRun = keyboard_check(vk_lshift);
kJump = keyboard_check(vk_space);
kJumpPressed = keyboard_check_pressed(vk_space);
kJumpReleased = keyboard_check_released(vk_space);

if(kRun){
	vxMaxCur = vxMaxRun;
} 
else {
	vxMaxCur = vxMax;
}

stateMachineRun();
event_inherited();