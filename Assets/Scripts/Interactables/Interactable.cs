using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Image UI_KeyBorder;
    public Text UI_text;

    private void Awake()
    {
        //set interactable`s UI disabled by default
        SetDisabled();
    }

    public string GetName()
    {
        return this.gameObject.name;
    }

   public void SetEnabled()
    {
        UI_KeyBorder.gameObject.SetActive(true);
        UI_text.gameObject.SetActive(true);
    }

   public void SetDisabled() {
        UI_KeyBorder.gameObject.SetActive(false);
        UI_text.gameObject.SetActive(false);
    }
}
