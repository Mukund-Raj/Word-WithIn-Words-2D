using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingOnWord : MonoBehaviour,IDropHandler ,IPointerEnterHandler,IPointerExitHandler{
	
	private RightShifted rightShiftedRef;
	private ElementBeingDropped elementBeingDroppedRef;
	public RightShifted RightShiftedRef
	{
		get
		{
			return rightShiftedRef;
		}

		set
		{
			rightShiftedRef = value;
		}
	}

	public ElementBeingDropped ElementBeingDroppedRef
	{
		get
		{
			return elementBeingDroppedRef;
		}

		set
		{
			elementBeingDroppedRef = value;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		//Debug.Log("working");
		if(eventData.pointerDrag.tag=="UIDraggable")
		{

			Debug.Log(eventData.pointerDrag.name+"being droped on me");
		}
		else if(eventData.pointerDrag.tag=="Word")
		{
			Debug.Log("word being droped on me");
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("pointer being hovered");
		if(eventData.dragging)
		{
			if (eventData.pointerDrag.tag == "UIDraggable")//for shiftin for outside element
			{
				//RightShiftedRef.ShiftingOut(transform.GetSiblingIndex());
				if(GetComponentInParent<ElementBeingDropped>().totalWords!=0)
			  GetComponentInParent<RightShifted>().ShiftingOut(transform.GetSiblingIndex());

				Debug.Log("something being dragged over me");
			}

			else if (eventData.pointerDrag.tag == "Word") //for shiftin of inside elment
			{
				//rightShiftedRef.ShiftingIn(transform.GetSiblingIndex(),
				//	eventData.pointerDrag.transform.gameObject);

GetComponentInParent<RightShifted>().ShiftingIn(transform.GetSiblingIndex(),eventData.pointerDrag.gameObject);
				Debug.Log("something being dragged over me");
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		/*
	if (GetComponentInParent<RightShifted>().TransparentWordRef.gameObject.activeSelf == true)
		{
			GetComponentInParent<RightShifted>().TransparentWordRef.gameObject.SetActive(false);
		}
		*/
	}
}