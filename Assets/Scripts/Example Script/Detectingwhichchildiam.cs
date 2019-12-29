using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Detectingwhichchildiam : MonoBehaviour,IPointerDownHandler {
    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(transform.GetSiblingIndex());
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
