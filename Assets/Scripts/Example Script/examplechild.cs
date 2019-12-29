using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class examplechild : MonoBehaviour {
	//public Button my1,my2,my3;
	private void Start()
	{
		
		PlayerPrefs.DeleteAll();
		if(PlayerPrefs.HasKey("Level1"))
		{
			Debug.Log("yep");
		}
		/*
		my1.onClick.AddListener( ()=>Getme(1));
		my2.onClick.AddListener(() => Getme(2));
		my3.onClick.AddListener(() => Getme(3));*/
	}
	/*
	public void Getme(int i)
	{
		Debug.Log(i);
	}*/
}
