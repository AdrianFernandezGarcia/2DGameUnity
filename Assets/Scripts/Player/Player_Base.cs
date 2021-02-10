using UnityEngine;

public class Player_Base : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer playerRenderer;
    protected BoxCollider2D playerCollider;
   
    protected void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
}
