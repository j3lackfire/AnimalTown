using UnityEngine;
using System.Collections;

public class MainPlayerController : MonoBehaviour {

#region UNITY EDITOR

	public CameraController MainCamera;



#endregion
	//might as well change the character controller to rigidbody since I want some physics
	CharacterController characterController;

	//the animator of the game object
	CharacterAnimatorController characterAnimatorController;


#region LOCAL VARIABLES
	Vector3 targetPosition;

	GameObject targetCar;

	//move speed of the player
	
#endregion

	[SerializeField]float moveSpeed;	

	void Awake(){
		//might want to change this.
		characterController = this.gameObject.GetComponent<CharacterController> ();

		characterAnimatorController = this.gameObject.GetComponent<CharacterAnimatorController> ();

		targetPosition = this.transform.position;
	}

	void Update(){
		MoveToTargetedPosition ();
	}

	//this function is called every frame. 
	void MoveToTargetedPosition(){

		Vector3 distance = new Vector3
			(targetPosition.x - this.transform.position.x,0,targetPosition.z - this.transform.position.z);

		if (distance.magnitude < 0.2f) {
			if (characterAnimatorController.characterAnimationType == PlayerAnimationType.Walk
			    		|| characterAnimatorController.characterAnimationType == PlayerAnimationType.Run){
				
				characterAnimatorController.EnterIdleAnimation();
			}
			return;
		} 
		else {
			characterController.Move(distance.normalized* Time.deltaTime * moveSpeed);	
		}

	}	

	//This function is important
	public void SetTargetPosition(Vector3 _targetPosition){
		this.transform.LookAt(_targetPosition);
		float yRotation = this.gameObject.transform.localRotation.eulerAngles.y;
		this.transform.localRotation = Quaternion.Euler
			(new Vector3(0,yRotation,0));
		targetPosition = _targetPosition;

		Vector3 distance = new Vector3
			(targetPosition.x - this.transform.position.x,0,targetPosition.z - this.transform.position.z);

		if (distance.magnitude < 0.21f) {
			return;
		}
		if (distance.magnitude < 4f) {
			moveSpeed = 2.5f;			
			if (characterAnimatorController.characterAnimationType != PlayerAnimationType.Walk) {
				characterAnimatorController.EnterWalkAnimation ();
			}
		} else {
			moveSpeed = 8;
			if (characterAnimatorController.characterAnimationType != PlayerAnimationType.Run) {
				characterAnimatorController.EnterRunAnimation ();
			}
		}
//		Vector3 moveDirection = destinatedPosition - this.transform.position;
	}


	public void EnterCarAnimation(){
		Debug.Log ("<color=orange>Player takes control of the car</color>");
//		GlobalStatic.playerState = PlayerState.Driving;
		this.gameObject.SetActive (false);
	}

	public void ExitCarAnimation(){
		Debug.Log ("<color=orange>Player exits the car</color>");
		this.gameObject.SetActive (true);
	}


}
