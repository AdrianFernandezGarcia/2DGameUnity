using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public Interactable[] interactables;
    
    void Start()
    {
        interactables = FindObjectsOfType(typeof(Interactable)) as Interactable[];
    }

    
    public void DisableInteractableUI(string type)
    {
        foreach (Interactable interactable in interactables)
        {
            if (interactable != null)
            {


                if (interactable.GetName().Equals(type))
                {
                    interactable.SetDisabled();
                }
            }
        }

    }
    public void EnableInteractableUI(string type)
    {
        foreach (Interactable interactable in interactables)
        {
            if (interactable.name.Equals(type))
            {
                interactable.SetEnabled();
            }
        }
    }
}
