using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;
public class SearchingTheWord : MonoBehaviour
{
	private TextAsset Wordfile;
	private string WordFromUser = null;
	//private int CurrentChildCount;
	private string[] WordFileArray;
	private int CountPoint = 0;
	//words created by the user
	private List<string> UserCraftedWords=new List<string>();
	//Total words made by the user
	[HideInInspector]
	public int NumberOfWordsByTheUser = 0;
	//to set the count text to current number of words
	public Text Counttext;
	//made this before sign
	public GameObject MadethisBeforeSign;
	//star fill gameobject
	public Image FilliamgeArea;
	//clear level message
	public GameObject clearThelevel;
	//the next level button so that it would be disabled
	public Button NextLevelButton;
	// Use this for initialization
	//score text reference
	public Text ScoreText;
	//variable for stroing the score int
	[HideInInspector]
	public int Score;
	//total childs in the current transform
	private int count;
	//to set next level button interactable to false
	void Start()
	{
		//CurrentChildCount = transform.childCount;

		NextLevelButton.interactable = false;
		Wordfile = (TextAsset)Resources.Load("word");
		WordFileArray = Wordfile.text.Split('\n');

		for (int i = 0; i < WordFileArray.Length; i++)
		{
			WordFileArray[i] = WordFileArray[i].Trim();
		}
		Debug.Log(WordFileArray.Length);
	}

	// Update is called once per frame
	/*
	void Update()
	{
		if (transform.childCount != CurrentChildCount && transform.childCount!=0)
		{
			ChangeTheString();
		}
	}
	*/
	//the main word panel checking for the string made by the user
	public void ChangeTheString()
	{
		count = 0;
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).gameObject.activeSelf == true)
			{
				count++;
			}
		}
		char[] wordArray = new char[count];
		for (int i = 0; i < count; i++)
		{
			if (transform.GetChild(i).gameObject.activeSelf == true)
			{
				char item = Convert.ToChar(transform.GetChild(i).GetChild(0).GetComponent<Text>().text);
				wordArray[i] = item;
			}
		}

		WordFromUser = new string(wordArray);

		//if user did not created the word before then check for word in txt file
		//otherwise don't
		if (!UserCraftedWords.Contains(WordFromUser))
			FoundTheWordOrNot(WordFromUser);
	}

	void IfWordIsMadeBefore()
	{
		MadethisBeforeSign.SetActive(true);
		StartCoroutine(Wait(MadethisBeforeSign));
	}


	void FoundTheWordOrNot(string word)
	{
		string FoundWord = null;
		//founding the word in WordFileArray means the txt file
		if(word!=" ")
		FoundWord = Array.Find(WordFileArray, element => element.Equals(word));
		Debug.Log(FoundWord);

		if (FoundWord != null)
		{
			Debug.Log("word found in the file :" + FoundWord);
			//user created the word add in string list
			//so user don't keep creating the same word over again & again
			UserCraftedWords.Add(FoundWord);
			ShowThecreatedWords();
			IncreaseTheScore(FoundWord.Length);
			GetComponent<ElementBeingDropped>().enabled = false;
			HandleWord();
		}
		else
		{
			Debug.Log("word not found");
		}

	}

	void HandleWord()
	{
		Image[] WordImageParentColor = new Image[count];
		Text[] wordimageChildColor = new Text[count];
		Transform[] AllChildWord = new Transform[count];

		for (int i = 0; i < count; i++)
		{
			//getting the transform of all childs
			AllChildWord[i] = transform.GetChild(i);
			//getting image component
			WordImageParentColor[i] = AllChildWord[i].GetComponent<Image>();

			//getting the text component of image transform child or textholderchild
			wordimageChildColor[i] = AllChildWord[i].GetChild(0).GetComponent<Text>();

		}
		for (int i = 0; i < count; i++)
		{
			StartCoroutine(FadingOfTheWord(WordImageParentColor[i],
					wordimageChildColor[i]));
		}

		CountPoint = count * 3;
		Debug.Log(CountPoint);
	}

	//fading of the word
	IEnumerator FadingOfTheWord(Image WordImage, Text WordText)
	{
		Color c1, c2;
		float dec = 0.1f;

		for (float j = 1f; j > -0.2f; j -= dec)
		{
			c1 = WordImage.color;
			c2 = WordText.color;

			c1.a = j;
			c2.a = j;

			WordImage.color = c1;
			WordText.color = c2;
			
			yield return new WaitForSeconds(0.10f);
		}
		DestroyingTheWord(WordImage);
	}

	public IEnumerator Wait(GameObject ObjectToWait)
	{
		Debug.Log("working");
		yield return new WaitForSeconds(1f);
		ObjectToWait.SetActive(false);
	}
	void DestroyingTheWord(Image WordImage)
	{
		Destroy(WordImage.gameObject);
		GetComponent<ElementBeingDropped>().enabled = true;

	}
	//the list has to be cleared after completing the level
	public void ClearTheUserCraftedListAfterChangingTheLevel()
	{
		UserCraftedWords.Clear();
	}
	public void ExplicitCheckingOfWords()
	{
		if (UserCraftedWords.Contains(WordFromUser))
			IfWordIsMadeBefore();

	}
	public void ClearTheWordDraggedPanel()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	//just increasing the score count to total 
	//amount of alphabets used by the user
	void IncreaseTheScore(int WordLength)
	{
		Score += WordLength;
		ScoreText.text = Score.ToString();
	}

	public void SetScoreDetailsToZero()
	{
		Score = 0;
		NumberOfWordsByTheUser = 0;
		FilliamgeArea.fillAmount = 0f;
		ScoreText.text = 0.ToString();
		Counttext.text = 0.ToString();
		NextLevelButton.interactable = false;
	}
	void ShowThecreatedWords()
	{
		NumberOfWordsByTheUser++;
		if(NumberOfWordsByTheUser==4)
		{
			NextLevelButton.interactable = true;
			clearThelevel.SetActive(true);
			StartCoroutine(Wait(clearThelevel));
		}
		if (NumberOfWordsByTheUser == 8)
			FilliamgeArea.fillAmount = 1;
		else
			FilliamgeArea.fillAmount = (float)(NumberOfWordsByTheUser)/ 8f;

		Counttext.text = NumberOfWordsByTheUser.ToString();
	}

}