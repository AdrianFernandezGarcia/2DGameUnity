using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue;
    private Transform playerPos;
    private SpriteRenderer npcRenderer;

    private void Awake()
    {
       
        npcRenderer = GetComponent<SpriteRenderer>();
    }

    //GETTERS Y SETTERS


    private void FixedUpdate()
         
    {
        
        //playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //TODO cambiar Vector3.up por playerPos.position;
        if (Vector2.Distance(transform.position, Vector3.up) > 0)
        {
            npcRenderer.flipX = true;
        }
        else if(Vector2.Distance(transform.position, Vector3.up) < 0)
        {

            npcRenderer.flipX = false;
        }
            
    }

	

	public Dialogue GetDialogue()
    {
        return this.dialogue;
    }
}
