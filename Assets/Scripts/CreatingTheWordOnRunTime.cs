using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatingTheWordOnRunTime : MonoBehaviour {
	//holder gameobject
	private  GameObject holder = null;
	//text holder gameobject
	private  GameObject textHolder=null;

	private string word;
	//reference to ElementBeingDropped class
	public ElementBeingDropped ElementBeingDropped;
	private List<string> MainWord;
	private string[] LongWordsArray;

	private void Awake()
	{
		holder = (GameObject)Resources.Load("Holder");
		textHolder=(GameObject)Resources.Load("TextHolder");
		//creating text assest from the longwords text file
		TextAsset longWords = (TextAsset)Resources.Load("LongWords");
		//longwords txt asset split
		LongWordsArray = longWords.text.Split('\n');
		//Debug.Log(LongWordsArray.Length);
		//trimiming the extra words in the splited text
		for (int i = 0; i < LongWordsArray.Length; i++)
		{
			LongWordsArray[i] = LongWordsArray[i].Trim();
		}
		//inserting it into the list main Word
		MainWord = new List<string>(LongWordsArray);
		//Words.AddRange(LongWordsArray);
		Debug.Log(MainWord.Count);
		Debug.Log(MainWord[1]);
	}


	//for creating the word holder and the respected words text UI
	//after clearing the Transform
	public void CreateHolder(int Index)
	{
		word = MainWord[Index];
		//setting the maximum length of words the user can make
		ElementBeingDropped.totalWords = word.Length;
		for (int i = 0; i < word.Length; i++)
		{
			//instantiate the holder object
			Transform holderobj = Instantiate(holder).transform;
			//instantiate the textHolder object
			Transform textholderobj = Instantiate(textHolder).transform;
			//holder object parent to the holder object 
			textholderobj.SetParent(holderobj);
			//setting the text property to the appropriate alpabet
			textholderobj.GetComponentInChildren<Text>().text = word[i].ToString();
			holderobj.GetComponent<HolderOf>().child = textholderobj.gameObject;
			holderobj.SetParent(transform);
		
		}
	}
	//to clear the Main word panel for new word for new level 
	public void ClearTheTransform()
	{
		for (int i = 0; i < transform.childCount;i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
