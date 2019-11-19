using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementBeingDropped : MonoBehaviour ,IDropHandler{
	[HideInInspector]
	public int TransformIndex;
	[HideInInspector]
	public bool IsOverOtherElements;
	[HideInInspector]
	public int totalWords;

	public void OnDrop(PointerEventData eventData)
	{   //For maintaining the elements outside the word panel

		if (eventData.pointerDrag.tag == "UIDraggable")
		{
			if (transform.childCount<=totalWords)
			{
				DragToPanelOutsideElement(eventData);
			}
		}
		//For maintaining the elements inside the word panel
		else if (eventData.pointerDrag.tag == "Word")
		{
			DragToPanelInsideElement(eventData);
		}
		GetComponent<SearchingTheWord>().ChangeTheString();	
	}

	void DragToPanelOutsideElement(PointerEventData eventData)
	{
		GameObject element = eventData.pointerDrag;
		element.transform.SetParent(null);
		element.transform.SetParent(transform);
	//	Debug.Log("index" + TransformIndex);
		SettingUpTheDroppedElementIndex(element, eventData);

		//Debug.Log(eventData.pointerDrag.name);
		element.tag = "Word";
		
		Destroy(element.GetComponent<DragThroughInterface>());
		element.AddComponent<DraggingOnWord>();
		element.AddComponent<DragToPanelDragging>();
		
	}
	void DragToPanelInsideElement(PointerEventData eventData)
	{
		GameObject element = eventData.pointerDrag;
		SettingUpTheDroppedElementIndex(element, eventData);
	}

	public void SettingUpTheDroppedElementIndex(GameObject element,PointerEventData eventData)
	{
		if(IsOverOtherElements)// if ui get dragged  over other ui TransformIndex!=1000)
		{
			element.transform.SetParent(transform);
			element.transform.SetSiblingIndex(TransformIndex);
		}
		else
		{
			element.transform.SetAsLastSibling();
		}
		
		element.GetComponent<Image>().raycastTarget = true;
		IsOverOtherElements = false;
		
		SettingUpThePlaceHolderTransaprentPanel();
	}
	public void SettingUpThePlaceHolderTransaprentPanel()
	{
		if (GetComponent<RightShifted>().TransparentWordRef != null)
		{    //setting TransparentWordRef to inactive
			GetComponent<RightShifted>().TransparentWordRef.gameObject.SetActive(false);
			//setting TransparentWordRef to last sibling so that
			//it won't mess up the horizontal layout group
			GetComponent<RightShifted>().TransparentWordRef.SetAsLastSibling();
		}
		//setting tranform index to 1000 just for 
		//checking element being dropped on previous element or empty are
	}
}
