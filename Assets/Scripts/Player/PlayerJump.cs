using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerJump : Player_Base
{

	private int airJumpCount=0;
	private int airJumpCountMax = 0;
	private float fallMultiplier = 2.5f;
	private float lowJumpMultiplier = 5f;
	private bool _canJump = true;
	private bool grounded = true;
	private PlayerMovement movement;
	Regex regex = new Regex("^Ground_|Interact");

    private void Awake()
    {
		movement = GetComponent<PlayerMovement>();
    }


    private void FixedUpdate()
    {

		JumpTimeHandler();

		if (rb.velocity.y < 0)
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

		else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
	}

    private void JumpTimeHandler()
	{
		// Test for normal jump if space is held down
		if (Input.GetButton("Jump")&&grounded)
		{
			if (airJumpCount <= airJumpCountMax)
			{
				Jump();
				SpendAirJump();
			}

            else
            {
				_canJump = false;
				ResetAirJumpCount();
			}
				
		}
			
	}


	private void Jump()
	{
		
			anim.SetTrigger("PlayerJump");
			float jumpVelocity = 12f;
			rb.velocity = Vector2.up * jumpVelocity;

	}

	private void SpendAirJump()
	{
		airJumpCount++;

	}

	private void ResetAirJumpCount()
	{
		if (airJumpCount > 0)
		{
			airJumpCount = 0;

		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (regex.IsMatch(collision.gameObject.tag))
        {
			grounded = true;
			movement.onAir = false;
		}
			


		//when player takes the double jump power
		if (collision.gameObject.CompareTag("DoubleJumpPowerUp"))
		{
			collision.gameObject.SetActive(false);
			airJumpCountMax = 1;
		}
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (regex.IsMatch(collision.gameObject.tag))
        {
			grounded = false;
			movement.onAir = true;
		}
			
	}

    #region GETTERS & SETTERS

	public int GetJumpCount()
    {
		return airJumpCountMax;
    }

	public void SetJumpCount(int jumpCountMax)
    {
		this.airJumpCountMax = jumpCountMax;
    }

    #endregion
}
