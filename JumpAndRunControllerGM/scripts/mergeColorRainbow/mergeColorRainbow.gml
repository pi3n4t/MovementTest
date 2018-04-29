/// @description merge_color_rainbow()
/// @param color1
/// @param color2
/// @param amount
var color1 = [color_get_hue(argument0),color_get_saturation(argument0),color_get_value(argument0)];
var color2= [color_get_hue(argument1),color_get_saturation(argument1),color_get_value(argument1)];
var newColor = [lerp(color1[0],color2[0],argument2),lerp(color1[1],color2[1],argument2),lerp(color1[2],color2[2],argument2)];
 
return make_color_hsv(newColor[0],newColor[1],newColor[2]);