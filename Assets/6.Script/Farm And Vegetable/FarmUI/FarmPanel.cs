using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FarmPanel : MonoBehaviour {

#region UNITY_EDITOR
	public FarmPanelButton[] FarmPanelButton;

	public Button BigHideUIButton, SmallHideUIButton;

#endregion

#region REFERENCE_GAME_OBJECT
	public FarmStack currentFarmStack;
#endregion

	void Awake(){
		for (int i = 0; i < 9; i ++) {
			FarmPanelButton[i].indexInFarm = i;
		}
	}

	void OnEnable(){
		BigHideUIButton.onClick.AddListener (FarmController.Instance.HideFarmUI);
		SmallHideUIButton.onClick.AddListener (FarmController.Instance.HideFarmUI);
		for (int i = 0; i < 9; i ++) {
			FarmPanelButton[i].GetComponent<Button>().onClick.AddListener(FarmController.Instance.FarmPanelButtonPressed);
		}

		StartCoroutine (DisplayFarmstack ());

	}

	void OnDisable(){
		BigHideUIButton.onClick.RemoveListener (FarmController.Instance.HideFarmUI);
		SmallHideUIButton.onClick.RemoveListener (FarmController.Instance.HideFarmUI);
		for (int i = 0; i < 9; i ++) {
			FarmPanelButton[i].GetComponent<Button>().onClick.RemoveListener(FarmController.Instance.FarmPanelButtonPressed);
		}
	}

	IEnumerator DisplayFarmstack(){
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();	
//		yield return new WaitForEndOfFrame();	
		for (int i = 0; i < 3; i ++) {
			for (int j = 0; j < 3; j ++){
				FarmPanelButton[i * 3 + j].SetUpFarmPanelButton(currentFarmStack.VegetableBoard[i,j]);

//				if (currentFarmStack.VegetableBoard[i,j] == null){
//					FarmPanelButton[i*3 + j].VegetableText.text = "NOTHING";
//				}
//				else{
//					FarmPanelButton[i*3 + j].VegetableText.text = currentFarmStack.VegetableBoard[i,j].transform.name;
//				}

			}
		}
	}


}
