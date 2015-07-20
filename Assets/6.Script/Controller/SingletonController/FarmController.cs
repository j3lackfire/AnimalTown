/// <summary>
/// The controlelr for the farm.
/// This script is responsible for player-farm interaction.
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum FarmInteractionType{
	PlantSeed, //plant in a new plant
	Water, //player waters the tree so it doesn't die
	Harvest, //player harvest the thing to gain money $$$$
	Fertilizer, //I don't even know if we should have this fertilizer or not
	Else //This must be an error
}

//This is a manager class, so it makes sense for this class to be a singleton
public class FarmController : Singleton<FarmController> {

#region ScreenSize_Interface
	[SerializeField]FarmPanel FarmPanelWidescreen; //The farm UI for the 16/9 screen ratio (normal phone and such)

	[SerializeField]FarmPanel FarmPanelNarrowScreen; //The farm UI for the 4/4 screen (iPad and tablet)

	// 3 / 4 = 0.75 , 10 / 16 = 0.625 , 9 / 16 = 0.5625

#endregion


#region USER INTERFACE
	[HideInInspector]public FarmPanel farmPanel;

	public FarmStack SelectedFarmStack;

#endregion

	public FarmInteractionType farmInteractionType;

#region Prefab

	public Cabbage CabbagePrefab;


#endregion

	void Awake(){
		if (((float)Screen.height / (float)Screen.width) < 0.65f) { // wide screen
			farmPanel = FarmPanelWidescreen;
		} 
		else {
			farmPanel = FarmPanelNarrowScreen;
		}
		farmInteractionType = FarmInteractionType.Else;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			HideFarmUI();
		}
	}

	public void ShowFarmUI(){
		farmPanel.gameObject.SetActive (true);
		farmPanel.currentFarmStack = SelectedFarmStack;
	}

	public void HideFarmUI(){
		Debug.Log("<color=red>Hide Farm UI</color>");			
		DataController.Instance.commandType = CommandType.MoveToPosition;
		farmPanel.gameObject.SetActive (false);
	}


	//this function is not necessary anymore, should probably remove it
	public void FarmPanelButtonPressed(){
//		Debug.Log ("It's working!");

	}

	public void HarvestVegetable(Vegetable _vegerable){
		Debug.Log ("<color=green>Harvest a vegetable</color> " + _vegerable.gameObject.name);
	}
}
