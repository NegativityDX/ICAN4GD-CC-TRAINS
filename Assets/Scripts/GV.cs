using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class GV {
	public static List<Station> allStations;
	public static List<Train> TrainList;
	public static int TotalTrains;
	public static int MaxTrains;

}

public class Station {
	public int _stationNum;
	public List<Train> _trains;
	public string _stationName;
	public Material _material;
}

public class Train {

	public Station currentStation;
	public Station destinationStation;
	public float TimeLeft;
}