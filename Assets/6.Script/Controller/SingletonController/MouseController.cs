using UnityEngine;
using System.Collections;

public class MouseController : Singleton<MouseController> {
//	public DataController MainDataController;

#region UNITY EDITOR
//	[HideInInspector]public CameraController mainCamera;
	private Camera camera;
//	public MainPlayerController MainPlayer;
//	public CarController MainCar;
#endregion

//	Vector3 RaycastHitPosition;

	void Awake(){
//		mainCamera = GameObject.FindObjectOfType<CameraController>();
		camera = CameraController.Instance.gameObject.GetComponent<Camera>();
//		RaycastHitPosition = Vector3.zero;
//		MainDataController = GameObject.FindObjectOfType<DataController> ();
	}

	void Update() {
		//Let's check if the player press the mouse button
		if (Input.GetMouseButton (0)) {
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			//layer 8 : road and terrain , layer 9 : cars , layer 10 = farm stack
			int layerMask = (1 << 8 | 1 << 9 | 1 << 10);

			//if the raycase hit something
			if (Physics.Raycast (ray,out hitInfo,100f,layerMask)){
				//return if player is touching on a game UI.
				try{
					if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()){
						//if player is click on the UI button
						return;
					}
				}
				catch{
					//why there is no event system ????
					Debug.Log("<color=red>No event system</color>");
				}

				//The command type, to be pass to the Data controller
				CommandType PlayerCommandType = CommandType.MoveToPosition;

				//check what the raycast hit
				switch(hitInfo.transform.tag){
				case "Untagged": //should usually be a terrain
					if (hitInfo.transform.name == "Terrain"){
						PlayerCommandType = CommandType.MoveToPosition;
					}
					else{
						Debug.Log("<color=green>The raycast hit an Untagged GameObject name : </color>" + hitInfo.transform.name);
					}
					break;
				case "TerrainAndRoad": //move to the position
					PlayerCommandType = CommandType.MoveToPosition;
					break;
				case "Cars": //move to car and drive it, maybe
					PlayerCommandType = CommandType.InteractWithObject;
					DataController.Instance.interactableGameObject = hitInfo.transform.gameObject;
					break;
				case "Farms":
					PlayerCommandType = CommandType.InteractWithObject;
					DataController.Instance.interactableGameObject = hitInfo.transform.gameObject;
					break;
				default:
					Debug.Log("<color=red>Something wrong happen at the Mouse controller, please check here</color>");
					PlayerCommandType = CommandType.Error;
					break;
				}

				DataController.Instance.SetMouseClickPosition(hitInfo.point,PlayerCommandType);

			}
		}
	}


//	void MoveToPosition(Vector3 targetPosion){
//		//this codes give a warning ...
//		if (GlobalStatic.playerState == null) {
//			GlobalStatic.playerState = PlayerState.Walking;
//		}
//
//		switch (GlobalStatic.playerState) {
//		case PlayerState.Walking:
//			MainPlayer.SetTargetPosition(targetPosion);
//			break;
//		case PlayerState.MovingToCar:
//			littleBear.MoveToCar();
//			break;
//		case PlayerState.Driving:
//			break;
//		default:
//			break;
//		}
//	}

}
