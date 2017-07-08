using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationScript : MonoBehaviour {

	public Transform TrainSlot1;
	public Transform TrainSlot2;
	public Transform TrainSlot3;
	public Transform TrainSlot4;


	public Material RefMaterial;
	public string stationName;
	public int stationNumber;

	public Transform GameTransform;
	public Transform ArrowTransform;

	public void Initialize()
	{
		Renderer ren;
		ren = TrainSlot1.gameObject.GetComponent<Renderer>();
		ren.material = RefMaterial;
		ren = TrainSlot2.gameObject.GetComponent<Renderer>();
		ren.material = RefMaterial;
		ren = TrainSlot3.gameObject.GetComponent<Renderer>();
		ren.material = RefMaterial;
		ren = TrainSlot4.gameObject.GetComponent<Renderer>();
		ren.material = RefMaterial;

		foreach (Station station in GV.allStations) {
			if (station._stationNum == stationNumber)
				station._material = RefMaterial;
		}

	}

	void Update ()
	{
		foreach (Station station in GV.allStations) {
			if (station._stationNum == stationNumber) { //if this station

				//Initialize ();

				for (int i = 0; i < station._trains.Count; i++) {
					ChangeSlotColor (station._trains [i], i);
				}
			}
		}
	}

	void ChangeSlotColor (Train train, int index){
		Renderer ren = null;

		switch (index) {
		case 0:
			ren = TrainSlot1.gameObject.GetComponent<Renderer> ();
			ren.material = train.destinationStation._material;
			break;
		case 1:
			ren = TrainSlot2.gameObject.GetComponent<Renderer> ();
			ren.material = train.destinationStation._material;
			break;
		case 2:
			ren = TrainSlot3.gameObject.GetComponent<Renderer> ();
			ren.material = train.destinationStation._material;
			break;
		case 3:
			ren = TrainSlot4.gameObject.GetComponent<Renderer> ();
			ren.material = train.destinationStation._material;
			break;
		}

		
	}


}
