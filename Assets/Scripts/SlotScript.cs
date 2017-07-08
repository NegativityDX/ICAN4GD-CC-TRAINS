using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour {

	public Transform Box;
	public Transform Train;
	public Text destText;
	public Text baseText;
	public Text typeText;
	public Text scoreText;
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
		destText.text = train.destinationStation._stationName;
		typeText.text = train.trainType.ToString ();
		scoreText.text = Mathf.RoundToInt(train.Score).ToString (); //ici
	}

	void Update()
	{
		if (!isTrainHere) {
			destText.text = " ";
			baseText.text = " ";
			scoreText.text = " ";
			typeText.text = " ";
		}
		isTrainHere = false;
	}
}
