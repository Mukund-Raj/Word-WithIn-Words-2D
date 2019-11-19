using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class menuNavigation : MonoBehaviour, ISelectHandler,IDeselectHandler
{
	public AudioSource change;

	/*
private List<Transform> but = new List<Transform>();
private Selectable selectable;
private Transform current;
private void Start()
{
	int count = transform.childCount;
	for (int i = 0; i < count; i++)
	{
		but.Add(transform.GetChild(i));
	}
	current = but[0];
}

private void Update()
{

}*/

	public void OnSelect(BaseEventData eventData)
	{
		GetComponent<RectTransform>().position =new Vector2(63,transform.position.y);
		Color c = GetComponentInChildren<Text>().color;
		c.a = 1f;
		Debug.Log("OnSelect"+c.a);
		
		GetComponentInChildren<Text>().color = c;
		
	}

	public void OnDeselect(BaseEventData eventData)
	{
		GetComponent<RectTransform>().position = new Vector2(23, transform.position.y);
		Color c = GetComponentInChildren<Text>().color;
		c.a = 0.71f;
		Debug.Log("OndeSelect" + c.a);
		change.Play();
		GetComponentInChildren<Text>().color = c;
		
	}
}
