using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragThroughInterface : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
	private Vector2 OriginalPosition;
	private int index;

	public void OnBeginDrag(PointerEventData eventData)
	{
		OriginalPosition = transform.position;
		GetComponent<Image>().raycastTarget = false;
		
	}

	public void OnDrag(PointerEventData eventData)
	{

		transform.position = Input.mousePosition;

	}
	public void OnEndDrag(PointerEventData eventData)
	{
		transform.position = OriginalPosition;
		GetComponent<Image>().raycastTarget = true;

	}
}
