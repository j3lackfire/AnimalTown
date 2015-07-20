using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum CameraFocusingTarget{
	Player, //The flayer is on foot and the camera is following the player.
	Car, //The camera is following the car.
	Else //this is wrong !!!
}


[RequireComponent(typeof(Camera))]
public class CameraController : Singleton<CameraController> {

#region CAMERA_SET_UP_VALUES

	Vector3 cameraPositionOffset;

	CameraFocusingTarget cameraFocusingTarget;

#endregion

#region UNITY EDITOR
//	private MainPlayerController mainPlayer;

	[SerializeField]public GameObject focusingTarget;

#endregion

	void Awake(){
//		mainPlayer = FindObjectOfType<MainPlayerController> ();
		SwitchViewToPlayer ();
	}


	void OnEnable(){
		//active the button, but we might not need the button anyway
//		changeCameraViewButton.onClick.AddListener (onChangeCameraviewButtonClicked);
	}

	void OnDisable(){
		//de-active the button but I plan to remove the button completely
//		changeCameraViewButton.onClick.RemoveListener (onChangeCameraviewButtonClicked);
	}

	void Update () {
		//make the camera follow the player
		this.transform.position = focusingTarget.gameObject.transform.position + cameraPositionOffset;
		this.transform.position = new Vector3 (this.transform.position.x, cameraPositionOffset.y, this.transform.position.z);
	}

	public void SwitchViewToCar(GameObject car){
		focusingTarget = car;
		cameraPositionOffset = new Vector3 (15, 20, 0);
//		cameraPositionOffset = new Vector3 (0, 20, -15);
		cameraFocusingTarget = CameraFocusingTarget.Car;
	}

	public void SwitchViewToPlayer(){
		focusingTarget = MainPlayerController.Instance.gameObject;
//		cameraPositionOffset = new Vector3 (0, 15, -8);
		cameraPositionOffset = new Vector3 (8, 15, 0);
		cameraFocusingTarget = CameraFocusingTarget.Player;
	}



}
