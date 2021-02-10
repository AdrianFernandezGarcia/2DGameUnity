using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D bc2D;
    public Sprite activatedSprite;
    private SpriteRenderer leverRenderer;
    public Elevator elevator;
    public bool elevatorGoesUp;
    void Start()
    {
        anim = GetComponent<Animator>();
        bc2D = GetComponent<BoxCollider2D>();
        leverRenderer = GetComponent<SpriteRenderer>();

        if (gameObject.tag.Equals("Lever Up"))
            elevatorGoesUp = true;

        else
            elevatorGoesUp = false;
      

        
    }


   public void ActivateLever()
    {

        anim.SetTrigger("TurnDown");
        anim.enabled = false;
        leverRenderer.sprite = activatedSprite;
        this.enabled = false;
        bc2D.enabled = false;
        
        
        if (elevatorGoesUp)
            leverUp();

        else
            leverDown();

    }


    private void leverDown()
    {
        elevator.SetGoesUp(false);
    }

    private void leverUp()
    {
        elevator.SetGoesUp(true);
    }
}
