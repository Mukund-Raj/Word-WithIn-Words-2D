using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderOf : MonoBehaviour {
	private int count = 0;
	public GameObject child;
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		int childnow = transform.childCount;
		if(childnow<2)
		{
			Transform childobj = Instantiate(child, Vector2.zero, transform.rotation).transform;
			childobj.SetParent(transform);
			childobj.GetComponent<RectTransform>().localPosition = Vector2.zero;
			count++;
			//Debug.Log("count" + count);
		}
		
	}
}
