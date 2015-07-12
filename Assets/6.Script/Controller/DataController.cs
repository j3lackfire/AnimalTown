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

public enum InteractableObjectType{
	Cars, //player is interacting with cars, enter it maybe
	Farm, //Player is interacting with the farm, to crop food and water and such ...
	Houses, //PLayer is buying / selling something with the shop
	Else //Alright, this is an error. This shouldn't happen
}





//I think this class should be singleton, because there will be only one, and we will need it very much
public class DataController : MonoBehaviour {

#region ENUMERATORS
	public PlayerState playerState;

	public CommandType commandType;

	public InteractableObjectType interactableObjectType;
#endregion

#region INTERACTING OBJECTS
	[HideInInspector]public GameObject interactableGameObject;


#endregion

#region UNITY EDITOR
	[HideInInspector]public CameraController mainCamera;
	
	public MainPlayerController mainPlayer;
	public CarController mainPlayerCar;
#endregion

	//I should remove this function as well, it is pretty useless
	void Awake(){
		playerState = PlayerState.OnFoot;
		commandType = CommandType.MoveToPosition;
		interactableObjectType = InteractableObjectType.Cars;

		mainPlayer = FindObjectOfType<MainPlayerController> ();
		mainCamera = FindObjectOfType<CameraController> ();
	}

	void Update(){
		if (playerState == PlayerState.Driving) {
			if (Input.GetKeyDown(KeyCode.Space)){
				PlayerExitCar();
			}
		}
	}

	//This function is called whenever player click the mouse button. It gets the click position and the type of action to do after wards
	public void SetMouseClickPosition(Vector3 clickPosition,CommandType _commandType){
		switch (_commandType) {
		case CommandType.MoveToPosition:
			commandType = CommandType.MoveToPosition;
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
			//First, we check if the player is within the object range. If not, we have to move to the ojbect
			if (commandType != CommandType.InteractWithObject){
				commandType = CommandType.InteractWithObject;
				mainPlayer.SetTargetPosition(clickPosition);
				StartCoroutine(InteractWithObject());
			}

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

	IEnumerator InteractWithObject(){
		//check if the player is within the object range, if true, start interact with the object
		Debug.Log("<color=red>Make sure this line is run only one!!!</color>");
		
		//layer 8 : road and terrain , layer 9 : cars , layer 10 = farm stack
		int collidingLayerMask = (1 << 9 | 1 << 10);
		while (true) {
			if (commandType != CommandType.InteractWithObject){
				Debug.Log("<color=green>Quit interacting with game object!!!</color>");
//				goto EndOfCoroutine;
				break;
			}
			Collider[] surroundingColliders = Physics.OverlapSphere(mainPlayer.transform.position + new Vector3(0,1,0),1.25f,collidingLayerMask);

			foreach(Collider c in surroundingColliders){
				if(c.gameObject == interactableGameObject){
//					Debug.Log("<color=green> Player SHOULD interact with the object now </color>");
					switch(interactableGameObject.tag){
					case "Cars":
						PlayerEnterCar();
						break;
					case "Farms":
						//should pop up an UI or things like that...
						break;
					default:
						Debug.Log("<color=red>Error with interactable game object. Please check!!!</color>");
						break;
					}
					goto EndOfCoroutine;
					break;
				}
			}

			yield return null;
		}
	EndOfCoroutine:
		yield return null;
	}

	public void PlayerEnterCar(){
		//Check if the car is drivable
		if (interactableGameObject.gameObject.GetComponent<CarController> ().isDrivable) {
			Debug.Log("<color=blue> Player enters the CAR </color>");
			//first, change the player state to driving.
			playerState = PlayerState.Driving;

			mainPlayer.SetTargetPosition(mainPlayer.transform.position);

			//we should do an animation of player enter the car here		
			//When enter the car, deactive the player, or move him some where else
			mainPlayer.EnterCarAnimation();

			//Switch the main controller to the Car
			mainPlayerCar = interactableGameObject.gameObject.GetComponent<CarController>();

			//Set up the camera to focus on the car again.
			mainCamera.SwitchViewToCar(interactableGameObject);

		}
		else {
			Debug.Log("<color=blue> The CAR is NOT drivable, canceling interaction !!! </color>");
			//stop the player movement
			playerState = PlayerState.OnFoot;
			commandType = CommandType.MoveToPosition;
			mainPlayer.SetTargetPosition(mainPlayer.transform.position);
		}


	}

	public void PlayerExitCar(){
		playerState = PlayerState.OnFoot;		
		mainPlayer.gameObject.SetActive (true);

		mainPlayer.ExitCarAnimation ();
		mainPlayer.transform.position = interactableGameObject.transform.position;
		mainPlayer.SetTargetPosition (mainPlayer.transform.position);
		//make the car stop running
		mainPlayerCar.SetTargetPosition (mainPlayerCar.transform.position);
		mainCamera.SwitchViewToPlayer ();
	
	}


}
