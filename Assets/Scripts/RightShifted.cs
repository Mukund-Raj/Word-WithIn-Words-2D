using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightShifted : MonoBehaviour {

	List<Transform> childs = new List<Transform>();
	public GameObject TransparentWordPrefab;
	[HideInInspector]
	public Transform TransparentWordRef;
	[HideInInspector]
	private int numberOfChilds;

	public int NumberOfChilds
	{
		get
		{
			return numberOfChilds;
		}
		set
		{
			numberOfChilds = value;
		}
	}

	void GetChildCount()
	{
		childs.Clear();
		NumberOfChilds = transform.childCount;
		for (int i = 0; i < NumberOfChilds; i++)
		{
			childs.Add(transform.GetChild(i));
		}
	}

	void getTransparentWordReference()
	{
		TransparentWordRef = Instantiate(TransparentWordPrefab).GetComponent<Transform>();
		TransparentWordRef.SetParent(transform);
		
	}

	public void ShiftingOut(int CurrentIndex)
	{
		GetChildCount();
		if (TransparentWordRef == null)
			getTransparentWordReference();

		else
		{	if (TransparentWordRef.gameObject.activeSelf == false)
				TransparentWordRef.gameObject.SetActive(true);

			for (int i = CurrentIndex; i < NumberOfChilds; i++)
			{
				childs[i].SetSiblingIndex(i + 1);
			}
			Debug.Log("current index" + CurrentIndex);
			TransparentWordRef.SetSiblingIndex(CurrentIndex);
			GetComponent<ElementBeingDropped>().TransformIndex = CurrentIndex;
			GetComponent<ElementBeingDropped>().IsOverOtherElements = true;
		}
	}
	
	public  void ShiftingIn(int CurrentIndex,GameObject currentObject)
	{
		currentObject.transform.SetParent(transform.parent);
		currentObject.transform.SetAsLastSibling();
		GetChildCount();
		if (TransparentWordRef == null)
			getTransparentWordReference();

		else
		{
			if (TransparentWordRef.gameObject.activeSelf == false)
				TransparentWordRef.gameObject.SetActive(true);

			for (int i = CurrentIndex; i < NumberOfChilds; i++)
			{
				childs[i].SetSiblingIndex(i + 1);
			}

			Debug.Log("current index" + CurrentIndex);
			TransparentWordRef.SetSiblingIndex(CurrentIndex);
			GetComponent<ElementBeingDropped>().TransformIndex = CurrentIndex;
			GetComponent<ElementBeingDropped>().IsOverOtherElements = true;

			//GetComponent<ElementBeingDropped>().SettingUpThePlaceHolderTransaprentPanel ();
		}
		
	}
}
