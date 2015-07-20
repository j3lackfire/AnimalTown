/// <summary>
/// Farm panel button.
/// This button display the current state of a single tile at the farm stack.
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PlantState{
	Empty, //There doesn't exists a vegetable in this slot
	JustPlanted, //The plant is just planted, used for fertilizing
	Growing, //The plant is growing and doesn't need water.
	NeedWater, //The plant needs water
	NeedHarvest, //The plant is harvestable
	Else //This is an error
}

[RequireComponent(typeof(Button))]
public class FarmPanelButton : MonoBehaviour {

#region FARM INTERACTION

	public PlantState referencePlantState;

	public Vegetable referenceVegetable;

#endregion

#region DISPLAY_CONTENT

	public Text VegetableText;

#endregion

	public int indexInFarm;

	void OnEnable(){
		this.gameObject.GetComponent<Button> ().onClick.AddListener (OnFarmButtonClicked);
	}

	void OnDisable(){
		this.gameObject.GetComponent<Button> ().onClick.RemoveListener (OnFarmButtonClicked);
	}

	public void SetUpFarmPanelButton(Vegetable _reference){
		if (_reference == null) {
			this.referenceVegetable = null;
			this.referencePlantState = PlantState.Empty;
			this.VegetableText.text = "NOTHING";
		}
		else {
			this.referenceVegetable = _reference;
			this.referencePlantState = _reference.plantState;
			this.VegetableText.text = referenceVegetable.transform.name + "\n" + referencePlantState.ToString();
		}
	}

	public void OnFarmButtonClicked(){
//		Debug.Log ("Yo!");
		switch (referencePlantState) {
		case PlantState.Empty:
			PlantNewPlant();
			//plant a new plant
			break;
		case PlantState.JustPlanted:
			Debug.Log("<color=green>The cabbage is just planted </color>");
			break;
		case PlantState.Growing:
			break;
		case PlantState.NeedWater:
			break;
		case PlantState.NeedHarvest:
			FarmController.Instance.HarvestVegetable(referenceVegetable);
			FarmController.Instance.SelectedFarmStack.RemoveVegetable(indexInFarm / 3, indexInFarm % 3);
			referenceVegetable = null;
			this.referencePlantState = PlantState.Empty;
			this.VegetableText.text = "NOTHING";
			break;
		case PlantState.Else:
		default:
			Debug.Log("<color=red>Something is definitely wrong in the farm panel button </color>");
			break;

		}
	}

	void PlantNewPlant(){
		FarmController.Instance.SelectedFarmStack.AddVegetable
			(FarmController.Instance.CabbagePrefab, indexInFarm / 3, indexInFarm % 3);
		
		this.referenceVegetable = FarmController.Instance.SelectedFarmStack.VegetableBoard[indexInFarm / 3, indexInFarm % 3];
//		referenceVegetable.plantState = PlantState.JustPlanted;
		referenceVegetable.plantState = PlantState.NeedHarvest;
		
		this.referencePlantState = referenceVegetable.plantState;

		//this isn't really necessary
		this.VegetableText.text = referenceVegetable.transform.name + "\n" + referencePlantState.ToString();

	}
}
