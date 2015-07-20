/// <summary>
/// A farm stack is a small plot of land that is able to contain up to 9 vegetalbes.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FarmStack : MonoBehaviour {
	//there are 9 vegetables, distributed in a 3 x 3 array
	[SerializeField]public Vegetable[,] VegetableBoard = new Vegetable[3,3];

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

	public void InitializeFarmStack(){
		for (int i = 0; i < 3; i ++) {
			for (int j = 0; j < 3; j ++){
				VegetableBoard[i,j] = null;
			}
		}

		Transform objectTransform = this.gameObject.GetComponent<Transform> ();

		foreach(Transform child in objectTransform){
			VegetableBoard[(int)child.transform.localPosition.x / 3 , (int)child.transform.localPosition.z / 3] = child.GetComponent<Vegetable>();
		}
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

	public void RemoveVegetable(int positionX,int positionY){
		RemoveVegetable (new Vector2(positionX,positionY));
	}

	public void RemoveVegetable(Vector2 vegPosition){
		if (vegPosition.x < 0 || vegPosition.x > 2 || vegPosition.y < 0 || vegPosition.y > 2) {
			Debug.LogError ("<color=green>WRONG VEGETABLE POSITION</color>");
			return;
		} 
		else {
			Destroy(VegetableBoard[(int)vegPosition.x,(int)vegPosition.y].gameObject);
			VegetableBoard[(int)vegPosition.x,(int)vegPosition.y] = null;
		}
	}


}
