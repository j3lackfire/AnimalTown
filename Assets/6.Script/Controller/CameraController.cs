using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//useless right now
public enum CameraView{
	north,
	south,
	west,
	east
}

public class CameraController : MonoBehaviour {
	//should remove this
	public Button changeCameraViewButton;

	//this too
	CameraView cameraView;

	//this is important
	Vector3 cameraPositionOffset;

#region UNITY EDITOR
	[SerializeField]public GameObject MainPlayer;


#endregion

	void Awake(){
		//both funtions might be removed
		cameraView = CameraView.south;
		SetUpCamera ();
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
		this.transform.position = MainPlayer.gameObject.transform.position + cameraPositionOffset;
		this.transform.position = new Vector3 (this.transform.position.x,15,this.transform.position.z);
	}

	//this function might be removed in the future
	void onChangeCameraviewButtonClicked(){
		switch (cameraView) {
		case CameraView.north:
			cameraView = CameraView.east;
			break;
		case CameraView.south:
			cameraView = CameraView.west;
			break;
		case CameraView.east:
			cameraView = CameraView.south;
			break;
		case CameraView.west:
			cameraView = CameraView.north;
			break;
		default:
			Debug.LogError("ERROR CHANGING THE CAMERA VIEW!");
			return;
		}
		SetUpCamera ();
	}



	//just change the offset, not really important
	void SetUpCamera(){
		switch (cameraView) {
		case CameraView.north:
			cameraPositionOffset = new Vector3(0,14,8);
			this.transform.localRotation = Quaternion.Euler(new Vector3(60f,180,0));
			break;
		case CameraView.south:
			cameraPositionOffset = new Vector3(0,14,-8);
			this.transform.localRotation = Quaternion.Euler(new Vector3(60f,0,0));
			break;
		case CameraView.east:
			cameraPositionOffset = new Vector3(8,14,0);
			this.transform.localRotation = Quaternion.Euler(new Vector3(60f,270,0));
			break;
		case CameraView.west:
			cameraPositionOffset = new Vector3(-8,14,0);
			this.transform.localRotation = Quaternion.Euler(new Vector3(60f,90,0));
			break;
		default:
			Debug.LogError("ERROR CHANGING THE CAMERA VIEW!");
			return;
		}
	}
}
