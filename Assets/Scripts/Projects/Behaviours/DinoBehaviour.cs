﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBehaviour : MonoBehaviour
{

    public float maxSpeed = 5f;
    public float maxAccel = 2f;
    private float accel = 0f;
    public float accelInc = 0.1f;
	private int SpeedMinus;
	public Vector3 RespawnPoint;
    public bool isBitting;

	private float TimeSlime;
	private float compagTime;

    Rigidbody2D rb;
    bool facingRight = true;
    float move;

	private int respawn;

    bool grounded = false;

    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    public float falling;

    public Animator anim;

    public int hp;
    public float knock;
    float takingDamage = 0f;


    LoadManager lm;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isBitting = false;
        lm = FindObjectOfType<LoadManager>();
        RespawnPoint = transform.position;

        hp = 3;
		respawn = 0;
		SpeedMinus = 0;
		TimeSlime = 0;
	}
	
	void Update ()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0)).IsName("Bite"))
        {
            isBitting = true;
        }
        else
        {
            isBitting = false;
        }
        jump();
        attack();
	}

    void FixedUpdate()
    {
        getValues();
        setAnimations();
        walk();
        accelIncrement();

		if (maxSpeed == 2){

			compagTime = Time.time;

			if ((TimeSlime + 3) < compagTime) {

				maxAccel = 2;
				maxSpeed = 4;

			}
		}
    }


    #region Collisions

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Slime")
        {
            hp--;
            rb.velocity = Vector3.zero;

            if (coll.transform.position.x > transform.position.x)
            {
                coll.rigidbody.AddForce(new Vector2(knock * (300f), 0));
                rb.AddForce(new Vector2(knock * (-300f), 300));
            }
            else
            {
                coll.rigidbody.AddForce(new Vector2(knock * (-300f), 0));
                rb.AddForce(new Vector2(knock * (300f), 300));
            }

            takingDamage = 0.15f;
        }    
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fall"))
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
			transform.position = RespawnPoint;
            //lm.LoadLevel("Menu");
        }
        if ((other.tag == "Enemy"))
        {
            if (!isBitting)
            {
                hp--;
                rb.velocity = Vector3.zero;
            }
        }

        if ((other.tag == "Bee"))
        {
            if (!isBitting)
            {
                hp--;
                rb.velocity = Vector3.zero;

                if (other.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(knock * (-200f), 50));
                }
                else
                {
                    rb.AddForce(new Vector2(knock * (200f), 50));
                }

                takingDamage = 0.15f;
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
        if (other.tag == "Hive")
        {
            if (isBitting)
            {
                Debug.Log("matando a colmeia");
                Destroy(other.gameObject);
            }
        }
		if (other.CompareTag ("Checkpoint"))
		{
			respawn = 1;	
			RespawnPoint = other.transform.position;
		}
		if (other.CompareTag ("Life")) {

			if (hp < 3) {

				hp++;
				other.gameObject.SetActive (false);

			}

		}

        if ((other.tag == "Wasp"))
        {
            if (!isBitting)
            {
                hp--;
                rb.velocity = Vector3.zero;

                if (other.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(knock * (-200f), 50));
                }
                else
                {
                    rb.AddForce(new Vector2(knock * (200f), 50));
                }

                takingDamage = 0.15f;
            }
        }

        if (other.CompareTag ("Smile Ball")) {

			other.gameObject.SetActive (false);
			TimeSlime = Time.time;
			maxSpeed = 2;
			maxAccel = 1;

		}
    }


    #endregion

    #region DinoActions

    void walk()
    {
        if (takingDamage <= 0f)
            rb.velocity = new Vector2(move * maxSpeed * accel, rb.velocity.y);
        else
            takingDamage = takingDamage - Time.deltaTime;

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip(); 
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump")) Debug.Log(grounded);
        if (grounded && Input.GetButtonDown("Jump") && takingDamage <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

            grounded = false;

            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    void attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("Bite");
        }
    }

    #endregion

    #region Actions

    void setAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Falling", falling);
        //anim.SetFloat("takingDamage", takingDamage);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    void getValues()
    {
        grounded = Physics2D.OverlapArea(groundCheck1.position, groundCheck2.position, whatIsGround);
        Debug.Log(grounded);
        move = Input.GetAxis("Horizontal");
        falling = rb.velocity.y;
    }

    void accelIncrement()
    {
        if (move != 0f && accel < maxAccel) accel += accelInc;
        else if(move == 0f) accel = 0f;
    }

    public bool getFlip()
    {
        return facingRight;
    }

    public int getHp()
    {
        return hp;
    }

    #endregion
}
