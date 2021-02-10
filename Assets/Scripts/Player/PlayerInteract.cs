using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private DialogueManager dialogueManager;
    private Player player;
    private PlayerJump playerJump;
    private bool canRest=false;
    private bool canTalk = false;
    private bool dialogStarted=false;
    private InteractableManager interactableManager;
    Regex regex = new Regex("^Interact");
    List<string> interactableNames = new List<string>();
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerJump= GameObject.Find("Player").GetComponent<PlayerJump>();
    }

    // Update is called once per frame
    void Update()
    {
        try { 
            interactableManager = GameObject.Find("InteractableManager").GetComponent<InteractableManager>();
        }
        catch (NullReferenceException) { }


        
        if (canRest)
        {
            Rest();
        }

        if (canTalk)
        {
            Talk();
        }

    }

    //player resting in a campfire
    private void Rest()
    {
        if (Input.GetButtonDown("Interact"))
        {
            player.ResetCurrentHealth();
            GameManager.instance.CheckPoint(transform.position);
            Persistence.Instance.SaveGame();
        }
    }

    //player talking with an NPC
    private void Talk()
    {
        if (Input.GetButtonDown("Interact") && !dialogStarted)
        {
            dialogueManager.StartDialogue();
            dialogStarted = true;
            playerJump.enabled = false;
        }

        else if (Input.GetButtonDown("Submit") && dialogStarted)
        {

            dialogueManager.DisplayNextSentence();
        }



        if (!dialogueManager.enabled)
        {
            dialogStarted = false;
            StartCoroutine(WaitForFinishDialog());

        }

    }

    IEnumerator WaitForFinishDialog()
    {
        yield return new WaitForSeconds(.5f);
        playerJump.enabled = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when player arrives to a campfire
        if (collision.gameObject.CompareTag("Interact_FirePlace") && interactableManager != null)
        {
            canRest = true;
            interactableManager.EnableInteractableUI(collision.gameObject.name);

        }

        //when player wants to talk with an NPC
        else if (collision.gameObject.CompareTag("Interact_NPC") && !dialogStarted)
        {
            dialogueManager.SetDialogue(collision.gameObject.GetComponent<NPC>().GetDialogue());
            canTalk = true;
            interactableManager.EnableInteractableUI(collision.gameObject.name);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //when player exits a campfire
        if (collision.gameObject.CompareTag("Interact_FirePlace"))
        {
            //if player´s outside the rest zone, it´ll not be able to rest
            canRest = false;
            interactableManager.DisableInteractableUI(collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag("Interact_NPC") && dialogStarted)
        {
            dialogStarted = false;
            canTalk = false;
            dialogueManager.EndDialog();
            interactableManager.DisableInteractableUI(collision.gameObject.name);
        }
        else if (collision.gameObject.CompareTag("Interact_NPC") && !dialogStarted)
        {
             canTalk = false;
             interactableManager.DisableInteractableUI(collision.gameObject.name);
        }
    }
}
