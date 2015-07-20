/// <summary>
/// Base class for the vegetable. Everything inherit from here.
/// </summary>

using UnityEngine;
using System.Collections;

public class Vegetable : MonoBehaviour {

	public PlantState plantState;

	void Awake(){
//		plantState = PlantState.Growing;
	}

}
