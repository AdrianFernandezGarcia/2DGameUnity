using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMenuButtonActions:MonoBehaviour
{
    

	
	void OnMouseEnter()
    {
        HighlightButton();
    }

     void OnMouseExit()
    {
        UnHighlightButton();
    }

    void HighlightButton()
    {

        GetComponent<Button>().transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        GetComponent<Button>().image.color = GetComponent<Button>().colors.highlightedColor;

    }

    void UnHighlightButton()
    {

        GetComponent<Button>().transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Button>().image.color = this.gameObject.GetComponent<Button>().colors.normalColor;
    }
}
