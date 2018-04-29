/// @description  @description Movement
/// @param [object] collisionObject Object to check collision against

var collisionObject = argument0;

repeat(abs(vy)){
	if(!place_meeting(x, y + sign(vy), collisionObject)){
		y += sign(vy);	
	}
	else{
		vy = 0;
		break;
	}
}

repeat(abs(vx)){
	if(!place_meeting(x + sign(vx), y, collisionObject)){
		x += sign(vx);	
	}
	else{
		vx = 0;
		break;
	}
}
