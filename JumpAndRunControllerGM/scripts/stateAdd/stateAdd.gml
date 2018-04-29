/// @description  @description Add a new state to the current objects' state machine
/// @param [string] name Name of the new state
/// @param [script] script Name of the script used for the new state

var stateName = string(argument0);
var scriptName = argument1;

ds_map_replace(stateMachine, stateName, scriptName);
