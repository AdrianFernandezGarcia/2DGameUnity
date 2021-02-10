using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZoneController : MonoBehaviour
{
    [SerializeField]
    private BossDoor[] doors = new BossDoor[2];
    [SerializeField]
    private Boss boss;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss.DisableAnim();
        boss.SetCurrentHealth(0);
        boss.ResetSprite();
        boss.enabled = false;
    }

    private void Update()
    {
       
        if (boss.isDead )
        {
            foreach (BossDoor door in doors)
            {
                door.OpenDoor();
            }
        }

        else if (player.died)
        {
            foreach (BossDoor door in doors)
            {
                door.OpenDoor();
                
            }
            boss.SetCurrentHealth(boss.GetMaxHealth());
        }
    }


    IEnumerator WaitToClose()
    {
        player.died = false;
        yield return new WaitForSeconds(1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //esperar para procesar correctamente si el jugador ha muerto antes o no
        StartCoroutine(WaitToClose());

        if (collision.gameObject.CompareTag("Player") && !boss.isDead &&!player.died)
        {
            
            foreach (BossDoor door in doors)
            {
                
                door.CloseDoor();
                
                if (!boss.enabled)
                {
                    boss.enabled = true;
                    boss.EnableAnim();
                }
               
                

            }
        }
       
    }
}
