using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerMovement : Player_Base
{
	private float movementSpeed = 6f;
	private bool grounded = true;
	//variable modified on PlayerJump that allows player to air control. 
	public bool onAir = false;
	//protected AudioClip footSteps;
	//public AudioClip footStepGrass;
	//public AudioClip footStepGround;
	// Update is called once per frame
	void Update()
    {
		if (Input.GetAxisRaw("Horizontal") != 0 && grounded)
		{
			//SoundManager.Instance.PlayClip(footSteps);
		}
	}

    private void FixedUpdate()
    {
		if (grounded || onAir)
			Movement();
	}

    private void Movement()
	{
		int movementVelocity = (int)(movementSpeed * Input.GetAxisRaw("Horizontal"));


		rb.velocity = new Vector2(movementVelocity, rb.velocity.y);

		

		if (movementVelocity != 0 && (grounded || onAir))
		{
			anim.SetBool("isRunning", true);


			if (movementVelocity < 0)
			{

				playerRenderer.flipX = false;
			}

			else if (movementVelocity > 0)
			{
				playerRenderer.flipX = true;

			}

		}

		else if (movementVelocity == 0)
		{

			anim.SetBool("isRunning", false);
		}

	}
	

    private void OnCollisionEnter2D(Collision2D collision)
    {
		Regex regex = new Regex("^Ground|Interact");
		//check if grounded and what type of ground is
		if (regex.IsMatch(collision.gameObject.tag) )
		{
				grounded = true;
			
			
			if (collision.gameObject.CompareTag("Ground_Grass"))
			{
				//SoundManager.Instance.EffectsSource.pitch = 1f;
				//footSteps = footStepGrass;
			}

			else if (collision.gameObject.CompareTag("Ground"))
			{
				//SoundManager.Instance.EffectsSource.pitch = 0.6f;
				//footSteps = footStepGround;
			}

			else if (collision.gameObject.CompareTag("Ground_FallCheckPoint"))
			{
				GameManager.instance.SetResetPoint(transform.position);
			}
		}

	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		Regex regex = new Regex("^Ground");

		if (regex.IsMatch(collision.gameObject.tag))
			grounded = false;

	}
}
