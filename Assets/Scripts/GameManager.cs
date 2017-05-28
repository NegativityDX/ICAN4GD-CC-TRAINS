﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text scoreText;
	public Transform Stations;

	public bool pressingUp = false;
	public bool pressingDown = false;
	public bool pressingLeft = false;
	public bool pressingRight = false;
	public static int score;
	public AudioSource DingSound;
	public AudioSource HonkSound;



	void Start () {
		score = 0;
		Initialize ();
	}
	
	void Update () {

		if (Input.GetKeyDown ("t")) {
			AddRandomTrainToBaseStation ();
		}


		if (Input.GetKeyDown ("up")) {
			
			Invoke ("PressUp", (float)1);

			pressingUp = true;
			if (pressingDown) {
				Invoke ("PressUpDown", (float)1);
			}
			if (pressingLeft) {
				Invoke ("PressUpLeft", (float)1);
			}
			if (pressingRight) {
				Invoke ("PressUpRight", (float)1);
			}
		}
		if (Input.GetKeyUp ("up")) {
			pressingUp = false;
			CancelInvoke ();
		}


		if (Input.GetKeyDown ("down")) {
			Invoke ("PressDown", (float)1);

			pressingDown = true;
			if (pressingRight) {
				Invoke ("PressDownRight", (float)1);
			}
			if (pressingLeft) {
				Invoke ("PressDownLeft", (float)1);
			}
			if (pressingUp) {
				Invoke ("PressUpDown", (float)1);
			}
		}
		if (Input.GetKeyUp ("down")) {
			pressingDown = false;
			CancelInvoke ();
		}


		if (Input.GetKeyDown ("left")) {
			Invoke ("PressLeft", (float)1);

			pressingLeft = true;
			if (pressingRight) {
				Invoke ("PressLeftRight", (float)1);
			}
			if (pressingUp) {
				Invoke ("PressUpLeft", (float)1);
			}
			if (pressingDown) {
				Invoke ("PressDownLeft", (float)1);
			}
		}
		if (Input.GetKeyUp ("left")) {
			pressingLeft = false;
			CancelInvoke ();
		}


		if (Input.GetKeyDown ("right")) {
			Invoke ("PressRight", (float)1);

			pressingRight = true;
				if (pressingUp) {
				Invoke ("PressUpRight", (float)1);
				}
				if (pressingDown) {
				Invoke ("PressingDownRight", (float)1);
				}
				if (pressingLeft) {
				Invoke ("PressingLeftRight", (float)1);
				}
		}
		if (Input.GetKeyUp ("right")) {
			pressingRight = false;
		}

		scoreText.text = ("Score = " + score);


	}

	public void AddRandomTrainToRandomStation()
	{
		int s = Random.Range (0, 5);
		int t = s;
		while (t==s) t = Random.Range (0, 4);

		Train train = new Train ();
		train.currentStation = GV.allStations[s];
		train.destinationStation = GV.allStations[t];




		GV.allStations [s]._trains.Add (train);

	}
	public void AddScore ()
	{
		score++;
		DingSound.Play ();
		Invoke ("AddRandomTrainToBaseStation", (float)1.5);
	}
		

	void Initialize (){

		GV.allStations = new List<Station> ();

		Station station = new Station ();
		station._stationNum = 0;
		station._stationName = "Up";
		station._trains = new List<Train>();
		GV.allStations.Add (station);

		station = new Station ();
		station._stationNum = 1;
		station._stationName = "Left";
		station._trains = new List<Train>();
		GV.allStations.Add (station);

		station = new Station ();
		station._stationNum = 2;
		station._stationName = "Down";
		station._trains = new List<Train>();
		GV.allStations.Add (station);

		station = new Station ();
		station._stationNum = 3;
		station._stationName = "Right";
		station._trains = new List<Train>();
		GV.allStations.Add (station);

		station = new Station ();
		station._stationNum = 4;
		station._stationName = "Base";
		station._trains = new List<Train>();
		GV.allStations.Add (station);

		Stations.BroadcastMessage ("Initialize");

	}

	public void Add10Trains ()
	{
		for (int i=0; i<10; i++)
			AddRandomTrainToRandomStation ();

	}

	public void AddRandomTrainToBaseStation()
	{
		int s = 4;
		int t = s;
		while (t == s)
			t = Random.Range (0, 4);

		Train train = new Train ();
		train.currentStation = GV.allStations [s];
		train.destinationStation = GV.allStations [t];

		HonkSound.Play ();


		GV.allStations [s]._trains.Add (train);
	}



	public void PressUp()
	{
		Station baseStation = GV.allStations [4];
		Station destStation = GV.allStations [0];

		foreach (Train train in baseStation._trains) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				destStation._trains.Add (train);
			}

		}
		baseStation._trains.Clear ();
	}

	public void PressLeft()
	{
		Station baseStation = GV.allStations [4];
		Station destStation = GV.allStations [1];

		foreach (Train train in baseStation._trains) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				destStation._trains.Add (train);
			}

		}
		baseStation._trains.Clear ();
	}

	public void PressDown()
	{
		Station baseStation = GV.allStations [4];
		Station destStation = GV.allStations [2];

		foreach (Train train in baseStation._trains) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				destStation._trains.Add (train);
			}

		}
		baseStation._trains.Clear ();
	}

	public void PressRight()
	{
		Station baseStation = GV.allStations [4];
		Station destStation = GV.allStations [3];

		foreach (Train train in baseStation._trains) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				destStation._trains.Add (train);
			}

		}
		baseStation._trains.Clear ();
	}


	public void PressUpLeft()
	{
		Station baseStation = GV.allStations [0];
		Station destStation = GV.allStations [1];

		List<Train> TempList = new List<Train>();

		foreach (Train train in baseStation._trains.ToArray()) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				TempList.Add(train);
			}
			baseStation._trains.Remove (train);
		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
			} else {
				baseStation._trains.Add (train);
			}
			destStation._trains.Remove (train);
		}

		destStation._trains.AddRange(TempList);

	}

	public void PressUpDown()
	{
		Station baseStation = GV.allStations [0];
		Station destStation = GV.allStations [2];

		List<Train> TempList = new List<Train>();

		foreach (Train train in baseStation._trains.ToArray()) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				TempList.Add(train);
			}
			baseStation._trains.Remove (train);
		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
			} else {
				baseStation._trains.Add (train);
			}
			destStation._trains.Remove (train);
		}

		destStation._trains.AddRange(TempList);

	}

	public void PressUpRight()
	{
		Station baseStation = GV.allStations [0];
		Station destStation = GV.allStations [3];

		List<Train> TempList = new List<Train>();

		foreach (Train train in baseStation._trains.ToArray()) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				TempList.Add(train);
			}
			baseStation._trains.Remove (train);
		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
			} else {
				baseStation._trains.Add (train);
			}
			destStation._trains.Remove (train);
		}

		destStation._trains.AddRange(TempList);

	}

	public void PressDownRight()
	{
		Station baseStation = GV.allStations [2];
		Station destStation = GV.allStations [3];

		List<Train> TempList = new List<Train>();

		foreach (Train train in baseStation._trains.ToArray()) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				TempList.Add(train);
			}
			baseStation._trains.Remove (train);
		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
			} else {
				baseStation._trains.Add (train);
			}
			destStation._trains.Remove (train);
		}

		destStation._trains.AddRange(TempList);

	}

	public void PressDownLeft()
	{
		Station baseStation = GV.allStations [2];
		Station destStation = GV.allStations [1];

		List<Train> TempList = new List<Train>();

		foreach (Train train in baseStation._trains.ToArray()) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				TempList.Add(train);
			}
			baseStation._trains.Remove (train);
		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
			} else {
				baseStation._trains.Add (train);
			}
			destStation._trains.Remove (train);
		}

		destStation._trains.AddRange(TempList);

	}

	public void PressLeftRight()
	{
		Station baseStation = GV.allStations [1];
		Station destStation = GV.allStations [3];

		List<Train> TempList = new List<Train>();

		foreach (Train train in baseStation._trains.ToArray()) {
			if (train.destinationStation == destStation) {
				AddScore ();
			} else {
				TempList.Add(train);
			}
			baseStation._trains.Remove (train);
		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
			} else {
				baseStation._trains.Add (train);
			}
			destStation._trains.Remove (train);
		}

		destStation._trains.AddRange(TempList);

	}


	
}
