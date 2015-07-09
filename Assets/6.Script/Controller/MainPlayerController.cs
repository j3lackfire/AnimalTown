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
	bool isMovingToCar;
	
#endregion
	public bool isDriving;

	[SerializeField]float moveSpeed;	

	void Awake(){
		isMovingToCar = false;

		//might want to change this.
		characterController = this.gameObject.GetComponent<CharacterController> ();

		characterAnimatorController = this.gameObject.GetComponent<CharacterAnimatorController> ();

		targetPosition = this.transform.position;
	}

	void Update(){
		MoveToTargetedPosition ();
	}

	void MoveToTargetedPosition(){

		Vector3 distance = new Vector3
			(targetPosition.x - this.transform.position.x,0,targetPosition.z - this.transform.position.z);
		if (isMovingToCar) {
			Collider[] overlapObjects = Physics.OverlapSphere(this.transform.position + new Vector3(0,1,0),1.25f);
			foreach(Collider c in overlapObjects){
				if (c.gameObject.tag == "Cars"){
					EnterCar();
				}
			}
			//if PlayerPrefs is getting near the car
			if (distance.magnitude <= 0.5f){
				EnterCar();
				//get in the car and take control of the car
			}
			else{
				characterController.Move(distance.normalized* Time.deltaTime * moveSpeed);	
			}
		} 
		else {
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

	}	

	public void SetTargetPosition(Vector3 _targetPosition){
		isMovingToCar = false;

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

	public void MoveToCar(GameObject car){
		Debug.Log ("MOVE TO CAR!!!");
		isMovingToCar = true;
		targetCar = car;
		
		this.transform.LookAt(car.transform.position);
		float yRotation = this.gameObject.transform.localRotation.eulerAngles.y;
		this.transform.localRotation = Quaternion.Euler
			(new Vector3(0,yRotation,0));

		targetPosition = car.transform.position;

		Vector3 distance = new Vector3
			(targetPosition.x - this.transform.position.x,0,targetPosition.z - this.transform.position.z);

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

	}

	void EnterCar(){
		Debug.Log ("Player takes control of the car");
		isMovingToCar = false;
		MainCamera.MainPlayer = targetCar;
//		GlobalStatic.playerState = PlayerState.Driving;
		this.gameObject.SetActive (false);
	}

//	void OnCollisionEnter(Collision col){
//		if (isMovingToCar) {
//			if (col.gameObject.tag == "Cars"){
//				EnterCar();
//				isMovingToCar = false;
//			}
//		}
//	}
}
