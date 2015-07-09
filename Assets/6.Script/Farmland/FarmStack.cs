/// <summary>
/// A farm stack is a small plot of land that is able to contain up to 9 vegetalbes.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FarmStack : MonoBehaviour {
	//there are 9 vegetables, distributed in a 3 x 3 array
	public Vegetable[,] VegetableBoard = new Vegetable[3,3];

#region FARM PREFABS
	public Cabbage smallCabbage;
	public Cabbage bigCabbage;

	public Carrot smallCarrot;
	public Carrot bigCarrot;

	public Corn smallCorn;
	public Corn mediumCorn;
	public Corn bigCorn;

	public Pumpkin smallPumpkin;
	public Pumpkin mediumPumpkin;
	public Pumpkin bigPumpkin;

	public SunFlower smallSunflower;
	public SunFlower bigSunflower;

	public Watermelon smallWatermelon;
	public Watermelon bigWatermelon;

#endregion

	void Awake(){
		InitializeFarmStack ();
	}

	//Just for test
	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("<color=blue>Press Space key</color>");
			for (int i = 0; i < 3; i ++) {
				for (int j = 0; j < 3; j ++){
					VegetableBoard[i,j].transform.position += new Vector3(0,2,0) ;
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			Debug.Log ("<color=blue>Press Space key</color>");
			for (int i = 0; i < 3; i ++) {
				for (int j = 0; j < 3; j ++){
					RemoveVegetable(new Vector2(i,j));
				}
			}
		}
	}


	public void InitializeFarmStack(){
		for (int i = 0; i < 3; i ++) {
			for (int j = 0; j < 3; j ++){
				VegetableBoard[i,j] = null;
			}
		}
		AddVegetable (bigCarrot,0,0);
		AddVegetable (smallCorn,0,1);
		AddVegetable (smallPumpkin,0,2);
		AddVegetable (mediumCorn,1,0);
		AddVegetable (mediumPumpkin,1,1);
		AddVegetable (bigCarrot,1,2);
		AddVegetable (bigWatermelon,2,0);
		AddVegetable (bigSunflower,2,1);
		AddVegetable (smallCabbage,2,2);
	}


	public void AddVegetable(Vegetable veg,Vector2 vegPosition){
		if (vegPosition.x < 0 || vegPosition.x > 2 || vegPosition.y < 0 || vegPosition.y > 2) {
			Debug.LogError ("<color=green>WRONG VEGETABLE POSITION</color>");
			return;
		} 
		else {
			VegetableBoard[(int)vegPosition.x,(int)vegPosition.y] = (Vegetable)Instantiate(veg);
			VegetableBoard[(int)vegPosition.x,(int)vegPosition.y].transform.SetParent(this.gameObject.transform);
			VegetableBoard[(int)vegPosition.x,(int)vegPosition.y].transform.localPosition = new Vector3(vegPosition.x * 3, 0, vegPosition.y * 3);
		}
	}

	public void AddVegetable(Vegetable veg,int xPosition,int yPosition){
		AddVegetable (veg, new Vector2 (xPosition, yPosition));
	}

	public void RemoveVegetable(Vector2 vegPosition){
		if (vegPosition.x < 0 || vegPosition.x > 2 || vegPosition.y < 0 || vegPosition.y > 2) {
			Debug.LogError ("<color=green>WRONG VEGETABLE POSITION</color>");
			return;
		} 
		else {
			Destroy(VegetableBoard[(int)vegPosition.x,(int)vegPosition.y].gameObject);
		}
	}


}
