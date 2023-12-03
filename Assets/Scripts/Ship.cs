using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Bullet bullet;

    Rigidbody2D rb;
    SpriteRenderer sr;

    bool forceOn = false;
    float forceAmount = 10.0f;
    float torqueDirection = 0.0f;
    float torqueAmount = 0.5f;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		UpdateSpriteTransparency();
	}
	void UpdateSpriteTransparency()
	{
		Color currentColor = sr.color;
		sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
	}
	void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Bullet theBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            theBullet.shoot(transform.up);
        }
        forceOn = Input.GetKey(KeyCode.W);
        if(Input.GetKeyDown(KeyCode.S))
        {
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), 180f);
        }
        if(Input.GetKey(KeyCode.A))
        {
			torqueDirection = 5f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            torqueDirection = -5f;
        }
        else
        {
            torqueDirection = 0f;
        }
        wrapAroundBoundary();
    }
    void wrapAroundBoundary()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if(x > 8f) {x = x - 16f; }
        if(x < -8f) { x = x + 16f; }
        if(y > 4.5f) { y = y - 9f; }
		if(y < -4.5f) { y = y + 9f; }

        transform.position = new Vector2(x, y);
	}
    void FixedUpdate()
    {
        if(forceOn)
        {
            rb.AddForce(transform.up * forceAmount);
        }
        if (torqueDirection != 0)
        {
            rb.AddTorque(torqueDirection * torqueAmount);
        }
    }
	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Obstacle")
        {
            transform.position = new Vector2(40000f, 40000f);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            turnOffCollisions();
            Invoke("reset", 3f);
        }
	}
    void turnOffCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore");
    }
	void reset()
	{
		Color currentColor = sr.color;
        sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.2f);

        transform.position = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Invoke("turnOnCollisions", 3f);
	}
    void turnOnCollisions()
    {
		Color currentColor = sr.color;
		sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);

		gameObject.layer = LayerMask.NameToLayer("Ship");
    }
}
