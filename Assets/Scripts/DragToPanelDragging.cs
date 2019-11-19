using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragToPanelDragging : MonoBehaviour, IBeginDragHandler, IEndDragHandler
	, IDragHandler
{
	
	private Transform actualParent;
	private Vector2 OriginalPos;
	public void OnBeginDrag(PointerEventData eventData)
	{
		OriginalPos = transform.position;	
		transform.GetComponent<Image>().raycastTarget = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		
		if(eventData.pointerCurrentRaycast.gameObject.tag=="WordPanel")
			transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.position = OriginalPos;
		transform.GetComponent<Image>().raycastTarget = true;
	}

}
