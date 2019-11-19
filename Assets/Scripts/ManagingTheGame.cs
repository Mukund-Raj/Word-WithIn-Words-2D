using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;

public class ManagingTheGame : MonoBehaviour {
	public CreatingTheWordOnRunTime CreatingTheWordOnRunTime;
	public SearchingTheWord searchingTheWord;
	private LevelData levelData;

	//main game area panel
	public GameObject mainLevelPanel;
	//main menu panel
	public GameObject MainMenuPanel;
	//level selector panel
	public GameObject LevelSelectorpanel;
	//ask when leaveing the level
	public GameObject exitpanel;
	//text for the current level
	public Text LevelNoText;
	//to set scroll bar value to zero
	public Scrollbar scrollbar;
	//exit ask panel for the game
	public GameObject exitaskpanel;
	//content where levels are located so that we could delete it and later update it
	public GameObject content;
	//saving panel and loading panel
	public GameObject loading;
	private void Awake()
	{
		levelData = new LevelData();
	}


	public void ChangeTheLevel()
	{
        //when changing the level a fake loading screen will appear
		loading.SetActive(true);
		Debug.Log("current level"+LevelInfo.CurrentLevel);
		Debug.Log("last level played" + PlayerPrefs.GetInt("LastLevelPlayed"));
        //fetch the previously played level
		FetchThelevelDetails();
		//to store the last level played by the user in playerprefs
		//next level
		PlayerPrefs.SetInt("LastLevelPlayed", LevelInfo.CurrentLevel);
		LevelInfo.CurrentLevel++;
        //clear the transform of the actual word panel
		CreatingTheWordOnRunTime.ClearTheTransform();
        //clear the word dragged panel because we are not changing the scene so
        //we have to clear the word dragged panel
		searchingTheWord.ClearTheWordDraggedPanel();
		

		CreatingTheWordOnRunTime.CreateHolder(LevelInfo.CurrentLevel);
		searchingTheWord.SetScoreDetailsToZero();
		searchingTheWord.ClearTheUserCraftedListAfterChangingTheLevel();
		LevelNoText.text = LevelInfo.CurrentLevel.ToString();
		PlayerPrefs.Save();
		StartCoroutine(searchingTheWord.Wait(loading));

	}

	public void SaveTheLevelData()
	{
        //using the xml serializer to save the class data 
        //LevelData is the class defined below at the end of the script 
		XmlSerializer InputxmlSerializer = new XmlSerializer(typeof(LevelData));
        //using will close the xml stream as soon as it will done with it
        //using stringwriter for writing the data to the xml serializer
		using (StringWriter Input = new StringWriter())
		{
			InputxmlSerializer.Serialize(Input, levelData);
			Debug.Log(Input.ToString());

			string LevelStringKey = "Level" + LevelInfo.CurrentLevel;
			Debug.Log(LevelStringKey);
			PlayerPrefs.SetString(LevelStringKey, Input.ToString());
		}
	}

	public float GetStarImageFillAmount(string LevelToCheck)
	{
		string LevelSpecificData;

		if (PlayerPrefs.HasKey(LevelToCheck))
		{
			LevelSpecificData = PlayerPrefs.GetString(LevelToCheck);
			Debug.Log(LevelSpecificData);
			XmlSerializer output = new XmlSerializer(typeof(LevelData));
			using (StringReader reader = new StringReader(LevelSpecificData))
			{
				LevelData ForFillAmount = new LevelData();
				ForFillAmount = output.Deserialize(reader) as LevelData;
				//Debug.Log(ForFillAmount.TotalWords);
				return (ForFillAmount.TotalWords / 8f);
			}
		}
		else
		{
			throw new Exception("Level data not exist");
		}
	}

	void FetchThelevelDetails()
	{
		
		levelData.LevelNo = LevelInfo.CurrentLevel;
		levelData.Score = searchingTheWord.Score;
		levelData.TotalWords = searchingTheWord.NumberOfWordsByTheUser;
		SaveTheLevelData();
	}
	public void GetTheLevel(int LevelNumber)
	{
		loading.SetActive(true);
		Debug.Log(LevelNumber);
		LevelSelectorpanel.SetActive(false);
		mainLevelPanel.SetActive(true);
		LevelNoText.text = LevelNumber.ToString();
		CreatingTheWordOnRunTime.CreateHolder(LevelNumber);
		StartCoroutine(searchingTheWord.Wait(loading));
	}

	public void Play()
	{
		loading.SetActive(true);
		int LastlevelPlayedByTheUser;
		CreatingTheWordOnRunTime.ClearTheTransform();
		searchingTheWord.ClearTheWordDraggedPanel();
		searchingTheWord.SetScoreDetailsToZero();
		if (PlayerPrefs.HasKey("LastLevelPlayed"))
		{
			LastlevelPlayedByTheUser = PlayerPrefs.GetInt("LastLevelPlayed", 0);
			MainMenuPanel.SetActive(false);

			mainLevelPanel.SetActive(true);

			CreatingTheWordOnRunTime.CreateHolder(LastlevelPlayedByTheUser + 1);
			LevelNoText.text = (LastlevelPlayedByTheUser+1).ToString();
		}
		else
		{
			MainMenuPanel.SetActive(false);
			mainLevelPanel.SetActive(true);
			LevelNoText.text = (1).ToString();
			CreatingTheWordOnRunTime.CreateHolder(1);
		}
		StartCoroutine(searchingTheWord.Wait(loading));
	}

	public void LoadingLevelSelectorPanel()
	{
		MainMenuPanel.SetActive(false);
		LevelSelectorpanel.SetActive(true);
		scrollbar.value = 1;
		content.GetComponent<LevelButtonCreation>().CreateLevel();
	}

	public void BackToMainMenuFromlevel()
	{
		exitpanel.SetActive(true);
	}

	public void ExitDecision(string decision)
	{
		if(decision=="yes")
		{
			mainLevelPanel.SetActive(false);
			exitpanel.SetActive(false);
			MainMenuPanel.SetActive(true);
		}
		else
		{
			exitpanel.SetActive(false);
		}
	}
	//from levelselector menu to main menu screen
	public void BackToMainMenuFromLevelSelector()
	{
		LevelSelectorpanel.SetActive(false);
		MainMenuPanel.SetActive(true);
		for (int i = 0; i < content.transform.childCount; i++)
		{
			Destroy(content.transform.GetChild(i).gameObject);
		}
		
	}

	public void ExitFromTheGame()
	{
		exitaskpanel.SetActive(true);
	}
	public void OnClickYes()
	{
		Application.Quit();
	}

	public void OnclickNo()
	{
		exitaskpanel.SetActive(false);
	}
}
 static class LevelInfo
{
	public static int CurrentLevel = PlayerPrefs.GetInt("LastLevelPlayed",0)+1;
}

[System.Serializable]
public class LevelData
{
	public int LevelNo;
	public int Score;
	public int TotalWords;
}