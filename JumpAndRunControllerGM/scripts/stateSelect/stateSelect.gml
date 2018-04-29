/// @description  @description Choose the current state of the current objects' state machine
stateToSelect = argument0

if(ds_map_exists(stateMachine, stateToSelect)){	
	currentStateName = stateToSelect;	
	currentStateScript = ds_map_find_value(stateMachine, currentStateName);
};
