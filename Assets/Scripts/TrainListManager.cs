using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainListManager : MonoBehaviour {

	public Transform TrainSlot1;
	public Transform TrainSlot2;
	public Transform TrainSlot3;
	public Transform TrainSlot4;
	public Transform TrainSlot5;
	public Transform TrainSlot6;



	// Use this for initialization
	void Start () {

		Initialize ();
		
	}

	void Initialize()
	{
//		TrainSlot1.gameObject.SetActive (false);
//		TrainSlot2.gameObject.SetActive (false);
//		TrainSlot3.gameObject.SetActive (false);
//		TrainSlot4.gameObject.SetActive (false);
//		TrainSlot5.gameObject.SetActive (false);
//		TrainSlot6.gameObject.SetActive (false);
		foreach (Renderer ren in TrainSlot1.gameObject.GetComponentsInChildren<Renderer>())
			ren.enabled=false;
		foreach (Renderer ren in TrainSlot2.gameObject.GetComponentsInChildren<Renderer>())
			ren.enabled = false;
		foreach (Renderer ren in TrainSlot3.gameObject.GetComponentsInChildren<Renderer>())
			ren.enabled = false;
		foreach (Renderer ren in TrainSlot4.gameObject.GetComponentsInChildren<Renderer>())
			ren.enabled = false;
		foreach (Renderer ren in TrainSlot5.gameObject.GetComponentsInChildren<Renderer>())
			ren.enabled = false;
		foreach (Renderer ren in TrainSlot6.gameObject.GetComponentsInChildren<Renderer>())
			ren.enabled = false;
	}

	void Update()
	{
		Initialize ();
		int i = 0;

		//L'ordre de la liste dépend des stations. Devrait s'afficher en fonction de l'ordre d'apparition.
		foreach (Station station in GV.allStations){
			foreach (Train train in station._trains) {
				switch (i) {
				case 0:
					if (GV.TotalTrains > 0) {
						foreach (Renderer ren in TrainSlot1.gameObject.GetComponentsInChildren<Renderer>())
							ren.enabled = true;
						TrainSlot1.SendMessage ("SetTrain", train);
						i++;
					}
					break;
				case 1:
					if (GV.TotalTrains > 1) {
						foreach (Renderer ren in TrainSlot2.gameObject.GetComponentsInChildren<Renderer>())
							ren.enabled = true;
						TrainSlot2.SendMessage ("SetTrain", train);
						i++;
					}
					break;
				case 2:
					if (GV.TotalTrains > 2) {
						foreach (Renderer ren in TrainSlot3.gameObject.GetComponentsInChildren<Renderer>())
							ren.enabled = true;
						TrainSlot3.SendMessage ("SetTrain", train);
						i++;
					}
					break;
				case 3:
					if (GV.TotalTrains > 3) {
						foreach (Renderer ren in TrainSlot4.gameObject.GetComponentsInChildren<Renderer>())
							ren.enabled = true;
						TrainSlot4.SendMessage ("SetTrain", train);
						i++;
					}
					break;
				case 4:
					if (GV.TotalTrains > 4) {
						foreach (Renderer ren in TrainSlot5.gameObject.GetComponentsInChildren<Renderer>())
							ren.enabled = true;
						TrainSlot5.SendMessage ("SetTrain", train);
						i++;
					}
					break;
				case 5:
					if (GV.TotalTrains > 5) {
						foreach (Renderer ren in TrainSlot6.gameObject.GetComponentsInChildren<Renderer>())
							ren.enabled = true;
						TrainSlot6.SendMessage ("SetTrain", train);
						i++;
					}
					break;
				}
			}
		}
	}



	public void AddTrain (Train train)
	{
		//GV.TrainList.Add (train);
		//UpdateList ();
	}
}