using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoBehaviour : MonoBehaviour
{
    // criando corpo e animacao
    public Animator Anime;
    public Rigidbody2D PlayerRigidbody;
    //criando e verificando pulo e correr e suas animacoes
    public int forceJump;
    public bool correr;
    public bool morder;
    public bool acao;
    public Transform GroundCheck;
    public bool grounded;
    public LayerMask whatisGround;
    public LayerMask whatisWall;
    public Transform WallCheck;
    public Transform WallCheck1;
    public Transform WallCheck2;
    public bool walled = false;
    public bool walled1 = false;
    public bool walled2 = false;
    public int hp;
    //movimentacao pra direita e esquerda
    public float speed;
    private float originalSpeed;
    private bool facingRight = true;
    private float movX;
    private float movY;
    //Tirar deslize
    //Verificar a parada
    public float stopTemp;
    public float timeTemp;
    public float stopTemp1;
    public float timeTemp1;

    //som

    
    // Use this for initialization
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        originalSpeed = speed;
        hp = 3;

    }

    void Move()
    {
        movX = Input.GetAxis("Horizontal");

        if (movX > 0 && !facingRight)
        {
            Flip();
        }
        else if (movX < 0 && facingRight)
        {
            Flip();
        }
        PlayerRigidbody.velocity = new Vector2(movX * speed, PlayerRigidbody.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = GetComponent<Transform>().localScale;
        scale.x *= -1;
        GetComponent<Transform>().localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.04f, whatisGround);
        walled = Physics2D.OverlapCircle(WallCheck.position, 0.05f, whatisWall);
        walled1 = Physics2D.OverlapCircle(WallCheck1.position, 0.2f, whatisWall);
        walled2 = Physics2D.OverlapCircle(WallCheck2.position, 0.2f, whatisWall);

        if ((grounded && walled) || (walled1 && grounded))
        {
            speed = originalSpeed;
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {

            if (walled || walled1 || walled2)
            {
                grounded = false;
            }
            PlayerRigidbody.AddForce(new Vector2(0, forceJump));
            correr = false;
            
        }
        if ((walled && !grounded) || (walled1 && !grounded) || (walled2 && !grounded))
        {
            speed = 0;
        }
        else
        {
            speed = originalSpeed;
        }
        if (Input.GetButton("Horizontal"))
        {
            Move();
            correr = true;
            timeTemp = 0;
        }
        if (correr)
        {
            timeTemp += Time.deltaTime;
            if (timeTemp >= stopTemp)
            {
                correr = false;
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            PlayerRigidbody.velocity = new Vector2(movX * speed / 4, PlayerRigidbody.velocity.y);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            morder = true;

        }

        if (morder)
        {
            timeTemp1 += Time.deltaTime;
            if (timeTemp1 >= stopTemp1)
            {
                morder = false;
            }
        }

        if (!morder)
        {
            timeTemp1 = 0;
        }

        if (correr && morder)
        {

            correr = false;

        }
        

        Anime.SetBool("morder", morder);
        Anime.SetBool("pular", !grounded);
        Anime.SetBool("correr", correr);



    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Inimigo" )
        {
            hp--;
        }
        if (other.tag=="Life")
        {

            if (hp < 3 )
            {

                hp++;
                other.gameObject.SetActive(false);


            }
        }
        if (other.tag == "ChaoMorte")
        {
            hp = 0;
        }
        if (other.tag == "Mudar")
        {
            Application.LoadLevel("Cena2");
        }

        if (hp == 0)
        {
            Application.LoadLevel("Cena2");
        }
    }



    public int getHp()
    {
        return hp;
    }

   

}
