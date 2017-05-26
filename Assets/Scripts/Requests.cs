using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Requests : MonoBehaviour {

	public Transform Game;
	public Text text;
	public Text second_text;
	public static int request;
	public static int second_request;
	public bool ScoreThreshold;
	public AudioSource sound;


	void Start()
	{
		NewRequest ();
		ScoreThreshold = false;
	}
	
	void Update () {
		
	}

	void NewRequest()
	{
		sound.Play ();
		int req = request;
		int req2 = second_request;

		while (req == request){
			req = Random.Range (0, 4);
		}

		while (req2 == second_request || req2 == req) {
			req2 = Random.Range (0, 4);
		}

		second_request = req2;
		request = req;

		switch (request) {
		case 0:
			text.text = ("Press Up!");
			break;
		case 1:
			text.text = ("Press Left!");
			break;
		case 2:
			text.text = ("Press Down!");
			break;
		case 3:
			text.text = ("Press Right!");
			break;
		default:
			text.text = ("Error :(");
			break;
		}

		if (ScoreThreshold) {
			
			switch (second_request) {
			case 0:
				second_text.text = ("Press Up!");
				break;
			case 1:
				second_text.text = ("Press Left!");
				break;
			case 2:
				second_text.text = ("Press Down!");
				break;
			case 3:
				second_text.text = ("Press Right!");
				break;
			default:
				second_text.text = ("Error :(");
				break;
			}
		} else
			second_text.text = (" ");
	}

	void PressUp ()
	{
		if (request == 0 && !ScoreThreshold) {
			Game.SendMessage("AddScore");
			NewRequest ();
		}
	}
	void PressDown()
	{
		if (request==2 && !ScoreThreshold){
			Game.SendMessage("AddScore");
			NewRequest ();
		}
	}
	void PressLeft()
	{
		if (request==1 && !ScoreThreshold){
			Game.SendMessage("AddScore");
			NewRequest ();
		}
	}
	void PressRight()
	{
		if (request==3 && !ScoreThreshold){
			Game.SendMessage("AddScore");
			NewRequest ();
		}
	}


	void PressUpDown()
	{
		if (ScoreThreshold)
		{
			if ((request == 0 && second_request == 2) || (request == 2 && second_request == 0)){
				Game.SendMessage ("AddScore");
				NewRequest ();
			}
		}
	}
	void PressUpLeft()
	{
		if (ScoreThreshold)
		{
			if ((request == 0 && second_request == 1) || (request == 1 && second_request == 0)){
				Game.SendMessage ("AddScore");
				NewRequest ();
			}
		}
	}
	void PressUpRight()
	{
		if (ScoreThreshold)
		{
			if ((request == 0 && second_request == 3) || (request == 3 && second_request == 0)){
				Game.SendMessage ("AddScore");
				NewRequest ();
			}
		}
	}

	void PressDownLeft()
	{
		if (ScoreThreshold)
		{
			if ((request == 1 && second_request == 2) || (request == 2 && second_request == 1)){
				Game.SendMessage ("AddScore");
				NewRequest ();
			}
		}
	}
	void PressDownRight()
	{
		if (ScoreThreshold)
		{
			if ((request == 3 && second_request == 2) || (request == 2 && second_request == 3)){
				Game.SendMessage ("AddScore");
				NewRequest ();
			}
		}
	}

	void PressLeftRight()
	{
		if (ScoreThreshold)
		{
			if ((request == 1 && second_request == 3) || (request == 3 && second_request ==1))
			{
				Game.SendMessage("AddScore");
				NewRequest();
			}
		}
	}


	void ChangeLevel()
	{
		ScoreThreshold = true;
	}
}
