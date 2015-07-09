using UnityEngine;
using System.Collections;

public enum CommandType{
	MoveToPosition, //command the player to move to a position in the world
	InteractWithObject, //go to interact with an object in the world
	InteractWithNPC, //interact with a NPC in the world
	Error //for when I don't how what the fuck is happening
}

public enum PlayerState{
	OnFoot, // player is on foot and is walking around.
	Driving, // player is dring the car
	Else // I can't decide yet.
}

public class DataController : MonoBehaviour {

	public PlayerState playerState;

	public CommandType commandType;

#region UNITY EDITOR
	[HideInInspector]public Camera mainCamera;
	
	public MainPlayerController mainPlayer;
	public CarController mainPlayerCar;
#endregion

	void Awake(){
		playerState = PlayerState.OnFoot;
		commandType = CommandType.MoveToPosition;
	}

	//public current controlling target (main player / car / farm UI ....)

	//public void switch controlling target () -> switch to main player, car or farm UI and deactive the rest.

	//This function is called whenever player click the mouse button. It gets the click position and the type of action to do after wards
	public void SetMouseClickPosition(Vector3 clickPosition,CommandType _commandType){
		switch (_commandType) {
		case CommandType.MoveToPosition:
			//What is the player doing ?
			switch(playerState){
			case PlayerState.OnFoot:
				mainPlayer.SetTargetPosition(clickPosition);
				break;
			case PlayerState.Driving:
				mainPlayerCar.SetTargetPosition(clickPosition);
				break;
			default:
				Debug.Log("<color=red>Player State Error, unidentify situation</color>");			
				break;
			}
			break;
		case CommandType.InteractWithObject:
			Debug.Log("<color=orange>Player is interacting with an object</color>");
			break;
		case CommandType.InteractWithNPC:
			break;
		case CommandType.Error:
			Debug.Log("<color=red>CommandType.Error, unIdentify situation</color>");			
			break;
		default:
			Debug.Log("<color=red>Error in getting the command type. It shouldn't happen.</color>");
			break;
		}
	}
}
