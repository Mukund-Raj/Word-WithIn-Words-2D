using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DetectingRightClickAndDelete : MonoBehaviour,IPointerEnterHandler,
    IPointerDownHandler,IPointerExitHandler
{
    [SerializeField]
    private GameObject deleteobj;
    private Transform refdeleteobj=null;

    

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(refdeleteobj==null)
            {
                refdeleteobj = Instantiate(deleteobj).transform;
            }
            if (refdeleteobj.gameObject.activeSelf == false)
                refdeleteobj.gameObject.SetActive(true);
            else
                refdeleteobj.gameObject.SetActive(false);


            Transform goclicked = eventData.pointerEnter.transform;
            
            refdeleteobj.SetParent(goclicked);
            refdeleteobj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            refdeleteobj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            Debug.Log("right mouse button clicked "+goclicked.name);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("mouse entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        refdeleteobj.gameObject.SetActive(false);
        refdeleteobj.SetParent(transform.parent);
    }

    public void DeleteTheWord()
    {

    }
}
