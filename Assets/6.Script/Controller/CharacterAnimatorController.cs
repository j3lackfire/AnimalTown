using UnityEngine;
using System.Collections;

public enum PlayerAnimationType{
	Idle,

	Look_around, //idle look around
	Wave, //idle wave
	Yawn, //idle yawn

	Walk,

	Crawl, //walk _ Crawl

	Run,

	Jump,

	Roll,
	Dash,

	Swim_Idle,
	Swim_Foward,

	Hurt,

	Die
};


public class CharacterAnimatorController : MonoBehaviour {
	public PlayerAnimationType characterAnimationType;

	public Animator characterAnimator;

	void Awake(){
		characterAnimator = this.gameObject.GetComponent<Animator> ();
		EnterIdleAnimation ();

	}

//	void Update(){
//		if (Input.GetKeyDown (KeyCode.A))
//			EnterIdleAnimation ();
//
//		if (Input.GetKeyDown (KeyCode.B))
//			EnterWalkAnimation ();
//
//		if (Input.GetKeyDown (KeyCode.C))
//			EnterRunAnimation ();
//
//		if (Input.GetKeyDown (KeyCode.D))
//			EnterWaveAnimation ();
//
//	}

	public void EnterIdleAnimation(){
		characterAnimationType = PlayerAnimationType.Idle;
		characterAnimator.SetTrigger ("EnterIdleAnimation");
		characterAnimator.SetInteger ("IdleAnimation",UnityEngine.Random.Range (1, 4));
	}

	//no parameter, -> normal walk
	//else -> funny Walk
	public void EnterWalkAnimation(bool funnyWalk = false){
		characterAnimationType = PlayerAnimationType.Walk;
		characterAnimator.SetTrigger("EnterWalkAnimation");
		characterAnimator.SetInteger ("WalkAnimation", funnyWalk ? 3 : UnityEngine.Random.Range(1,3));
	}

	//no parameter = normal run.
	//2 = funny run - 3 = panic run
	public void EnterRunAnimation(int runAnimation = 1){
		characterAnimationType = PlayerAnimationType.Run;
		characterAnimator.SetTrigger("EnterRunAnimation");
		characterAnimator.SetInteger ("RunAnimation",runAnimation);
	}

	public void EnterWaveAnimation(){
		characterAnimationType = PlayerAnimationType.Wave;
		characterAnimator.SetTrigger ("EnterWaveAnimation");
		Invoke ("EnterIdleAnimation", 0.2f);
	}

}
