using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour {

	public Transform Box;
	public Transform Train;
	public Text text;
	int t;
	bool isTrainHere = false;

	public void SetTrain (Train train){
		Renderer ren = null;

		isTrainHere = true;

		ren = Train.gameObject.GetComponent<Renderer> ();
		ren.material = train.destinationStation._material;

		ren = Box.gameObject.GetComponent<Renderer> ();
		ren.material = train.currentStation._material;

		t = Mathf.FloorToInt(train.TimeLeft);

		if (train.TimeLeft > 0.1)
			text.text = t + "s";
		else {
			text.text = " ";
		}
	}

	void Update()
	{
		if (!isTrainHere) {
			text.text = " ";
		}
		isTrainHere = false;
	}
}
