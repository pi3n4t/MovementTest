if(instance_exists(oPlayer)){	
draw_text(0, 12, "Player.x = " + string(oPlayer.x));
draw_text(0, 24, "Player.y = " + string(oPlayer.y));
draw_text(0, 36, "Player.vx = " + string(oPlayer.vx));
draw_text(0, 48, "Player.vy = " + string(oPlayer.vy));
draw_text(0, 60, "Wall to the left = " + string(oPlayer.wallLeft));
draw_text(0, 72, "Wall to the right = " + string(oPlayer.wallRight));
draw_text(0, 84, "Player is grounded = " + string(oPlayer.isGrounded));
draw_text(0, 96, "Player is sticky = " + string(oPlayer.sticky));
draw_text(0, 108, "Sticky Buffer = " + string(oPlayer.alarm[0]));
draw_text(0, 120, "Jump Buffer = " + string(oPlayer.alarm[1]));
draw_text(0, 132, "WallJumpBuffer = " + string(oPlayer.alarm[2]));
}

