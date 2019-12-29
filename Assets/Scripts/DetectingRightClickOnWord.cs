using UnityEngine;
using UnityEngine.EventSystems;

public class DetectingRightClickOnWord : MonoBehaviour,
    IPointerDownHandler, IPointerExitHandler
{
   static bool IsDeleteActive = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(1))
        {
            // if (!IsDeleteActive)
            // {
            GetComponentInParent<DeleteTheWord>().
                      The_wordToBe_Deleted_Information((byte)transform.GetSiblingIndex());
            IsDeleteActive = true;
            // }
            /*
            else
            {
                GetComponentInParent<DeleteTheWord>().HideTheDeleteButton();
                IsDeleteActive = false;
            }*/

        }
        if (Input.GetMouseButtonDown(0))
        {
             if(IsDeleteActive)
            {
                GetComponentInParent<DeleteTheWord>().HideTheDeleteButton();
                     
            }
        }
    }




    public void OnPointerExit(PointerEventData eventData)
    {
       // GetComponentInParent<DeleteTheWord>().HideTheDeleteButton();
      //  IsDeleteActive = false;
        //GetComponentInParent<DeleteTheWord>().Clicking_outside_the_word();
    }
}
