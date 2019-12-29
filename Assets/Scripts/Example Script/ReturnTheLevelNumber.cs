using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ReturnTheLevelNumber : MonoBehaviour {
	public ManagingTheGame managingTheGame;
	private Text no;
	private void Start()
	{
		no = GetComponent<Text>();
	}
	public void SendLevelNumber()
	{
		
		
			string t =  Convert.ToString(no.text);
			Debug.Log(no.text.ToString());
			Debug.Log(t.GetType());
			int Levelno = Int32.Parse(t);
			Debug.Log(Levelno);
		try
		{
			managingTheGame.GetTheLevel(Levelno);

		}
		catch (Exception e)
		{
			Debug.Log(e.Message+e.Data);
		}
		
	}
}
