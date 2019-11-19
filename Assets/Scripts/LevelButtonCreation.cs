using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelButtonCreation : MonoBehaviour {
	public GameObject LevelPrefab;
	private Text LevelText;
	public int LevelAmount;
	public GameObject LockIcon;
	public ManagingTheGame ManagingTheGame;
	public GameObject StarsImage;
	public Scrollbar contentScroll;
	// Use this for initialization
	private void Awake()
	{
		contentScroll.value = 0;
	}
	/*
	private void Start()
	{
		CreateLevel();
	}*/
	public void CreateLevel()
	{
		string Levelidentifier = "Level";


		for (int i = 1; i <= LevelAmount; i++)
		{
			if (i == 1)//for level 1.level 1 has to be opened
			{   //if player is playing the first time

				GameObject Level1 = Instantiate(LevelPrefab);
				Level1.transform.SetParent(transform);
				LevelText = Level1.GetComponentInChildren<Text>();

				if (!PlayerPrefs.HasKey(Levelidentifier + i))
				{
					LevelText.text = 1.ToString();
					GameObject starsObj = Instantiate(StarsImage);
					starsObj.transform.SetParent(Level1.transform);
				}
				else
				{
					LevelText.text = 1.ToString();
					GameObject starsObj = Instantiate(StarsImage);
					starsObj.transform.SetParent(Level1.transform);
					starsObj.transform.GetChild(0).GetComponent<Image>().fillAmount
						= ManagingTheGame.GetStarImageFillAmount(Levelidentifier + i);
				}
			}
			else
			{
				GameObject Level = Instantiate(LevelPrefab);
				Level.transform.SetParent(transform);
				LevelText = Level.GetComponentInChildren<Text>();

				if (!PlayerPrefs.HasKey(Levelidentifier + i))
				{
					LevelText.text = i.ToString();
					Instantiate(LockIcon).transform.SetParent(Level.transform);
					Level.GetComponent<Button>().interactable = false;

				}
				else
				{
					LevelText.text = i.ToString();
					GameObject starsObj = Instantiate(StarsImage);
					starsObj.transform.SetParent(Level.transform);
					starsObj.transform.GetChild(0).GetComponent<Image>().fillAmount
						= ManagingTheGame.GetStarImageFillAmount(Levelidentifier + i);
					/*number = Int32.Parse(LevelText.text);
					Level.GetComponent<Button>().onClick.AddListener
						(() => ManagingTheGame.GetTheLevel(number));*/
				}

			}
		}
		Addinglistener();
	}
	
	void Addinglistener()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			int no= Int32.Parse(transform.GetChild(i).GetChild(0).GetComponent<Text>().text);
			transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() =>
	ManagingTheGame.GetTheLevel(no));
		}
	}
}
