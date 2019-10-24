using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public float jumpPower;

	private float moveValue;

	private Rigidbody2D rb2d;

	private bool facingRight = true;

	// Start is called before the first frame update
	void Start()
    {
		rb2d = GetComponent<Rigidbody2D>();
    }


	private void FixedUpdate()
	{
		moveValue = Input.GetAxis("Horizontal");
		rb2d.velocity = new Vector2(moveValue * speed, rb2d.velocity.y);

		if (facingRight == false && moveValue > 0)
		{
			FlipSprite();
		} else if (facingRight == true && moveValue < 0)
		{
			FlipSprite();
		}
	}

	void FlipSprite()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}
}
