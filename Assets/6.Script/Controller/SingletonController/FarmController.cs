using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//everything belong to the controller class should be a singleton, I think
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

	void Awake(){
		if (((float)Screen.height / (float)Screen.width) < 0.65f) { // wide screen
			farmPanel = FarmPanelWidescreen;
		} 
		else {
			farmPanel = FarmPanelNarrowScreen;
		}
//		farmPanel.gameObject.SetActive (false);
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
		DataController.Instance.commandType = CommandType.MoveToPosition;
		farmPanel.gameObject.SetActive (false);
	}

	public void FarmPanelButtonPressed(){
//		Debug.Log ("It's working!");
	}
}
