using UnityEngine;
using UnityEngine.UI;
/*
 *This script  contains centralise methods for deleting the word
 * and showing the delete button on textholder component
 * why we did it is because i want to centralise the method calls so
 * that it will be handled in one script and would be easy to refactor it maybe

*/
public class DeleteTheWord : MonoBehaviour {
    [SerializeField]
    private GameObject deleteobj = null;
    private Transform refdeleteobj = null;
    private byte theWordToBeDeleted;

    //method to be called when any word is to be deleted by the user
    //we are centralising the code so that if any word is right clicked
    //then every OnPointerDown called this function
    
    //we are getting the child index of the textolder component on which
    //right click event occurs and that method sends the index in the hierarchy 
    //and we will get the transfrom of that child and show the button there
    //no need to send the child transform to this method just its index is sufficient
    public void The_wordToBe_Deleted_Information(byte wordToBeDeletedIndex)
    {
            if (refdeleteobj == null)
            {
                refdeleteobj = Instantiate(deleteobj).transform;
            refdeleteobj.GetComponent<Button>().onClick.AddListener(Deleting_The_Word);
            }
            if (refdeleteobj.gameObject.activeSelf == false)
                refdeleteobj.gameObject.SetActive(true);
            
/*
        else
                refdeleteobj.gameObject.SetActive(false);
                */
            refdeleteobj.SetParent(transform.GetChild(wordToBeDeletedIndex));
        //assigning word to be deleted to a field which is gonna be used 
        //when the user presses the button and Deleting_The_Word method called 
            theWordToBeDeleted = wordToBeDeletedIndex;
            refdeleteobj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 73f, 0f);
            refdeleteobj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            
            //Debug.Log("right mouse button clicked " + wordToBeDeleted.name);
        }
	
    //to be called by the delete button whenever the user clicks the delete button
    void Deleting_The_Word()
    {
        //refdeleteobj.gameObject.SetActive(false);
        Destroy(transform.GetChild(theWordToBeDeleted).gameObject);
    }

    //method called when the user don't want to delete the word and click outside 
    //the the word area and on the other parts of the screen,it is called by the 
    //OnPointerExit method that is on each textholder component
  
    public void HideTheDeleteButton()
    {
        refdeleteobj.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
