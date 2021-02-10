using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void CloseDoor()
    {
       
      
            anim.SetTrigger("doorDown");
     
       
    }


    public void OpenDoor()
    {
         anim.SetTrigger("doorUp");
        
    }
    

}
