using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour {

#region DATA_CONTROLLER

	[SerializeField]public bool isDrivable;



#endregion


	Rigidbody r;

	[SerializeField]public float carSpeed;

	Vector3 targetPosition;

	void Awake(){
		r = this.gameObject.GetComponent<Rigidbody> ();
		targetPosition = this.transform.position;
	}

	void Update () {
		MoveToTargetedPosition ();
		CarStablizer ();
	}

	void MoveToTargetedPosition(){
		
		Vector3 distance = new Vector3
			(targetPosition.x - this.transform.position.x,0,targetPosition.z - this.transform.position.z);

		if (distance.magnitude < 0.2f) {
			r.velocity = Vector3.zero;
			return;
		} 
		else {
//			r.AddForce(distance.normalized* Time.deltaTime * carSpeed,ForceMode.Force);	
			r.velocity = distance.normalized* Time.deltaTime * carSpeed;	
		}
		
	}	

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

	}


	void CarStablizer(){
		Vector3 carRotation = transform.eulerAngles;
		transform.localRotation = Quaternion.Euler (new Vector3(0,carRotation.y,0));
	}

}
