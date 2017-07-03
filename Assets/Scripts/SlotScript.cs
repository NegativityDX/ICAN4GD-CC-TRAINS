using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour {

	public Transform Box;
	public Transform Train;
	public Text text;
	public Text baseText;
	int t;
	bool isTrainHere = false;

	public void SetTrain (Train train){
		Renderer ren = null;

		isTrainHere = true;

		ren = Train.gameObject.GetComponent<Renderer> ();
		ren.material = train.destinationStation._material;

		ren = Box.gameObject.GetComponent<Renderer> ();
		ren.material = train.currentStation._material;

		baseText.text = train.currentStation._stationName;
		text.text = train.destinationStation._stationName;
	}

	void Update()
	{
		if (!isTrainHere) {
			text.text = " ";
		}
		isTrainHere = false;
	}
}
