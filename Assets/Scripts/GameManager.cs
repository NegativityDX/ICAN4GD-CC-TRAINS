using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text scoreText;
	public Text TimerText;
	public Transform Stations;
	public Transform TrainList;
	[Range(0f,100f)]
	public float TrainTime;
	public int maxTrains;

	public bool pressingUp = false;
	public bool pressingDown = false;
	public bool pressingLeft = false;
	public bool pressingRight = false;
	public bool pressingButton = false;
	public static int score;
	public AudioSource DingSound;
	public AudioSource HonkSound;
	public AudioSource BoomSound;

	float timeLeft;



	void Start () {
		score = 0;
		Initialize ();
		GV.MaxWaitingTrains = 6;
		GV.TotalTrains = 0;
	}
	
	void Update () {

		if (IsGameOver ())
		{
			Debug.Log ("Game over!");
		}

		if (Input.GetKeyDown ("t")) {
			AddRandomTrainToBaseStation ();
		}
		if (Input.GetKeyDown ("y")) {
			Add10Trains ();
		}

		if (Input.GetMouseButtonDown (0)) {
			pressingButton = true;
			if (pressingDown && pressingUp)
				PressUpDown();
			if (pressingDown && pressingLeft)
				PressDownLeft();
			if (pressingDown && pressingRight)
				PressDownRight();
			if (pressingUp && pressingRight)
				PressUpRight();
			if (pressingUp && pressingLeft)
				PressUpLeft();
			if (pressingLeft && pressingRight)
				PressLeftRight();
			if (pressingDown && !pressingUp && !pressingLeft && !pressingRight)
				PressDown ();
			if (pressingLeft && !pressingUp && !pressingRight && !pressingDown)
				PressLeft ();
			if (pressingUp && !pressingDown && !pressingLeft && !pressingRight)
				PressUp ();
			if (pressingRight && !pressingUp && !pressingLeft && !pressingDown)
				PressRight ();
		}
		if (Input.GetMouseButtonUp (0)) {
			pressingButton = false;
		}


		if (Input.GetKeyDown ("up")) {
			pressingUp = true;
		}
		if (Input.GetKeyUp ("up")) {
			pressingUp = false;
		} 

		if (Input.GetKeyDown ("down")) {
			pressingDown = true;
		}
		if (Input.GetKeyUp ("down")) {
			pressingDown = false;
		}


		if (Input.GetKeyDown ("left")) {
			pressingLeft = true;
		}
		if (Input.GetKeyUp ("left")) {
			pressingLeft = false;
		}

		if (Input.GetKeyDown ("right")) {
			pressingRight = true;
		}
		if (Input.GetKeyUp ("right")) {
			pressingRight = false;
		}

		scoreText.text = ("Score = " + score);

		CountTrains ();
		TimerTick ();
	}

	public void TimerTick()
	{
		timeLeft -= Time.deltaTime;
		TimerText.text = Mathf.RoundToInt(timeLeft).ToString ();

		if (timeLeft < 0)
		{
			if (GV.TotalTrains >= GV.MaxWaitingTrains) {
				//it's game over!
				Debug.Log ("Game over!");
			}
			AddRandomTrainToBaseStation ();
			timeLeft = TrainTime;
		}
	}

	public bool IsGameOver()
	{
		foreach (Station sta in GV.allStations) {
			if (sta._trains.Count < sta._maxTrains) {
				break;
			}
			return true;
		}
		return false;
	}



	public void CountTrains ()
	{
		int i=0;
		foreach (Station station in GV.allStations) {
			i = i + station._trains.Count;
		}
		GV.TotalTrains = i;
	}

	public void AddRandomTrainToRandomStation()
	{
		if (GV.TotalTrains >= GV.MaxWaitingTrains)
			return;

		int s = Random.Range (0, 5);
		int t = s;
		while (t==s) t = Random.Range (0, 4);

		Train train = new Train ();
		train.currentStation = GV.allStations[s];
		train.destinationStation = GV.allStations[t];

		GV.allStations [s]._trains.Add (train);
		HonkSound.Play ();
	}

	public void AddRandomTrainToNonbaseStation()
	{
		if (GV.TotalTrains >= GV.MaxWaitingTrains)
			return;

		int s = Random.Range (0, 4);
		int t = s;
		while (t==s && GV.allStations [t]._trains.Count < GV.allStations [t]._maxTrains) t = Random.Range (0, 4); //cond. station non pleine

		Train train = new Train ();
		train.currentStation = GV.allStations [s];
		train.destinationStation = GV.allStations [t];
		
		GV.allStations [s]._trains.Add (train);
		HonkSound.Play ();
	}
	public void AddScore ()
	{
		score++;
		DingSound.Play ();
	}

	public void RemoveScore()
	{
		score--;
		BoomSound.Play ();
	}
		

	void Initialize (){

		timeLeft = TrainTime;

		GV.allStations = new List<Station> ();
		GV.TrainList = new List<Train> ();

		Station station = new Station ();
		station._stationNum = 0;
		station._stationName = "Gare Rouge";
		station._trains = new List<Train>();
		station._maxTrains = maxTrains;
		GV.allStations.Add (station);

		station = new Station ();
		station._stationNum = 1;
		station._stationName = "Gare Bleue";
		station._trains = new List<Train>();
		station._maxTrains = maxTrains;
		GV.allStations.Add (station);

		station = new Station ();
		station._stationNum = 2;
		station._stationName = "Gare Violette";
		station._trains = new List<Train>();
		station._maxTrains = maxTrains;
		GV.allStations.Add (station);

		station = new Station ();
		station._stationNum = 3;
		station._stationName = "Gare Verte";
		station._trains = new List<Train>();
		station._maxTrains = maxTrains;
		GV.allStations.Add (station);

		station = new Station ();
		station._stationNum = 4;
		station._stationName = "Exterieur";
		station._trains = new List<Train>();
		station._maxTrains = GV.MaxWaitingTrains;
		GV.allStations.Add (station);

		Stations.BroadcastMessage ("Initialize");

	}

	public void Add10Trains ()
	{
		for (int i=0; i<10; i++)
			AddRandomTrainToNonbaseStation ();

	}

	public void AddRandomTrainToBaseStation()
	{
		if (GV.TotalTrains >= GV.MaxWaitingTrains)
			return;
		int s = 4;
		int t = s;
		while (t == s)
			t = Random.Range (0, 4);

		Train train = new Train ();
		train.currentStation = GV.allStations [s];
		train.destinationStation = GV.allStations [t];

		HonkSound.Play ();


		GV.allStations [s]._trains.Add (train);
		TrainList.SendMessage ("AddTrain", train);

	}



	public void PressUp()
	{
		Station baseStation = GV.allStations [4];
		Station destStation = GV.allStations [0];

		Train[] TempList = new Train[6];
		baseStation._trains.CopyTo (TempList);

		foreach (Train train in TempList) {
			if (train.destinationStation == destStation) {
				AddScore ();
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				destStation._trains.Add (train);
				baseStation._trains.Remove (train);
			}
		}
	}

	public void PressLeft()
	{
		Station baseStation = GV.allStations [4];
		Station destStation = GV.allStations [1];

		Train[] TempList = new Train[6];
		baseStation._trains.CopyTo (TempList);

		foreach (Train train in TempList) {
			if (train.destinationStation == destStation) {
				AddScore ();
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				destStation._trains.Add (train);
				baseStation._trains.Remove (train);
			}
		}
	}

	public void PressDown()
	{
		Station baseStation = GV.allStations [4];
		Station destStation = GV.allStations [2];

		Train[] TempList = new Train[6];
		baseStation._trains.CopyTo (TempList);

		foreach (Train train in TempList) {
			if (train.destinationStation == destStation) {
				AddScore ();
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				destStation._trains.Add (train);
				baseStation._trains.Remove (train);
			}

		}
	}

	public void PressRight()
	{
		Station baseStation = GV.allStations [4];
		Station destStation = GV.allStations [3];

		Train[] TempList = new Train[6];
		baseStation._trains.CopyTo (TempList);

		foreach (Train train in TempList) {
			if (train.destinationStation == destStation) {
				AddScore ();
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				destStation._trains.Add (train);
				baseStation._trains.Remove (train);
			}

		}

	}


	public void PressUpLeft()
	{
		Station baseStation = GV.allStations [0];
		Station destStation = GV.allStations [1];

		List<Train> TempList = new List<Train>();

		foreach (Train train in baseStation._trains.ToArray()) {
			if (train.destinationStation == destStation) {
				AddScore ();
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains) {
				train.currentStation = destStation;
				TempList.Add(train);
				baseStation._trains.Remove (train);
			}
		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
				destStation._trains.Remove (train);
			} else if (baseStation._trains.Count < baseStation._maxTrains){
				train.currentStation = baseStation;
				baseStation._trains.Add (train);
				destStation._trains.Remove (train);
			}
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
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				TempList.Add(train);
				baseStation._trains.Remove (train);
			}

		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
				destStation._trains.Remove (train);
			} else if (baseStation._trains.Count < baseStation._maxTrains){
				train.currentStation = baseStation;
				baseStation._trains.Add (train);
				destStation._trains.Remove (train);
			}
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
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				TempList.Add(train);
				baseStation._trains.Remove (train);
			}

		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
				destStation._trains.Remove (train);
			} else if (baseStation._trains.Count < baseStation._maxTrains){
				train.currentStation = baseStation;
				baseStation._trains.Add (train);
				destStation._trains.Remove (train);
			}

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
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				TempList.Add(train);
				baseStation._trains.Remove (train);
			}

		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
				destStation._trains.Remove (train);
			} else if (baseStation._trains.Count < baseStation._maxTrains){
				train.currentStation = baseStation;
				baseStation._trains.Add (train);
				destStation._trains.Remove (train);
			}

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
				baseStation._trains.Remove (train);
			} else if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				TempList.Add(train);
				baseStation._trains.Remove (train);
			}

		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
				destStation._trains.Remove (train);
			} else if (baseStation._trains.Count < baseStation._maxTrains){
				train.currentStation = baseStation;
				baseStation._trains.Add (train);
				destStation._trains.Remove (train);
			}

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
				baseStation._trains.Remove (train);
			} else  if (destStation._trains.Count < destStation._maxTrains){
				train.currentStation = destStation;
				TempList.Add(train);
				baseStation._trains.Remove (train);
			}

		}


		foreach (Train train in destStation._trains.ToArray()) {
			if (train.destinationStation == baseStation) {
				AddScore ();
				destStation._trains.Remove (train);
			} else if (baseStation._trains.Count < baseStation._maxTrains){
				train.currentStation = baseStation;
				baseStation._trains.Add (train);
				destStation._trains.Remove (train);
			}

		}

		destStation._trains.AddRange(TempList);

	}


	
}
