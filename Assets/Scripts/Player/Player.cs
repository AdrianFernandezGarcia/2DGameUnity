using System.Collections;
using System.Security.Policy;
using UnityEngine;

public class Player : Player_Base
	{ 

	private int maxHealth = 200;
	private int currentHealth;
	//UI
	[SerializeField]
	private PlayerHealthBar healthBar;
	[SerializeField]
	private PlayerInventory inventory;
	[SerializeField]
	protected Sprite deathSprite;
	[SerializeField]
	protected GameObject deathCanvas;
	public bool died = false;
	protected bool isDead = false;
	private readonly int healAmount = 20;

	private void Awake()
		{

		healthBar.SetMaxHealth(maxHealth);
		currentHealth = maxHealth;
		
		}

    private void Update()
    {
		if (Input.GetButtonDown("Heal") && inventory.LifePotionsAmount > 0)
		{
			Heal(healAmount);
			inventory.SpendLifePotion();
		}
	}

	public void InitUI()
	{
		healthBar.gameObject.SetActive(true);
		inventory.gameObject.SetActive(true);
	}

	public void DisableUI()
    {
		healthBar.gameObject.SetActive(false);
		inventory.gameObject.SetActive(false);
	}
		
	private void Heal(int healAmount)
    {
		AddCurrentHealth(healAmount);
    }

	public void TakeDamage(int dmg)
	{
		anim.SetTrigger("PlayerHurt");
		ReduceCurrentHealth(dmg);
		healthBar.SetHealth(currentHealth);
		playerCollider.enabled = false;

		if (GetCurrentHealth() <= 0)
			Die();
	}


	private IEnumerator WaitToDie()
	{
		isDead = true;
		deathCanvas.SetActive(true);
		anim.enabled = false;
		anim.SetBool("playerDead", true);
		died = true;
		GameManager.instance.DisablePlayer();
		//disable player
		GetComponent<SpriteRenderer>().sprite = deathSprite;
		yield return new WaitForSeconds(2f);
		GameManager.instance.ResetPlayer();
		isDead = false;
		anim.enabled = true;
		deathCanvas.SetActive(false);

	}

	private void Die()
	{

		StartCoroutine(WaitToDie());


	}

	void OnCollisionEnter2D(Collision2D collision)
    {
		//si se cae
        if (collision.gameObject.CompareTag("KillBox"))
        {
			GameManager.instance.Respawn();
			currentHealth -= 20;
        }

		//si coge una pocion
		if (collision.gameObject.CompareTag("LifePotion"))//mover a PlayerHeal
		{
			collision.gameObject.SetActive(false);
			inventory.AddLifePotion();
		}

		if (collision.gameObject.CompareTag("GoToZone2"))
		{
			//guardar la posición para que luego a volver el jugador reaparezca ahí
			GameManager.instance.SetZone1Spawn(transform);
			GameManager.instance.GoZone2();
		}

		if (collision.gameObject.CompareTag("GoToZone1"))
		{
			GameManager.instance.GoZone1();
		}
	}

	
	public void AddCurrentHealth(int adition)
    {
		currentHealth += adition;
    }


	public void ReduceCurrentHealth(int reduction)
	{
		currentHealth -= reduction;
	}

	public void ResetCurrentHealth()
    {
		currentHealth = maxHealth;
    }


	//GETTERS Y SETTERS



	public int GetCurrentHealth()
	{
		return currentHealth;
	}
	
	public void SetCurrentHealth(int currentHealth)=> this.currentHealth = currentHealth;

	public int GetMaximumHealth()
    {
		return maxHealth;
    }
	public int GetPotionAmount()
	{
		return inventory.LifePotionsAmount;
	}
	public void  SetPotionAmount(int potionAmount  )=> this.inventory.LifePotionsAmount = potionAmount;


}

