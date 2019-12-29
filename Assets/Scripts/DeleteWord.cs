using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DeleteWord : MonoBehaviour ,IPointerEnterHandler,IDropHandler,IPointerExitHandler{
	public void OnDrop(PointerEventData eventData)
	{
		if(eventData.pointerDrag.tag=="UIDraggable" || 
			eventData.pointerDrag.tag == "Word")
		{
			Color c = GetComponent<Image>().color;
			c.a = 0.5f;
			GetComponent<Image>().color = c;
			Destroy(eventData.pointerDrag.gameObject);
			c.a = 0f;
			GetComponent<Image>().color = c;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(eventData.dragging)
		{
			Color c = GetComponent<Image>().color;
			c.a = 0.5f;
			GetComponent<Image>().color = c;
		}

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Color c = GetComponent<Image>().color;
		c.a = 0f;
		GetComponent<Image>().color = c;
	}
}
