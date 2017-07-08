using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D rb;
    private Transform tr;
    private Animator an;
    public Transform verificaChao;
    public Transform verificaParede;

    public bool estaNaParede;
    public bool estaNoChao;
    private bool viradoParaDireita;
    public bool atacar;

    public float stopTemp;
    private float temp;
    public int forceJump;
    public float velocidade;
    public float raioValidaChao;
    public float raioValidaParede;
    public float raioValidaPerseguicao;
    public Transform campoDeVisao;

    public LayerMask solido;
    public LayerMask player;

    // Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        an = GetComponent<Animator>();

        viradoParaDireita = true;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyMoviment();
        
    }

    void EnemyMoviment()
    {
        estaNoChao = Physics2D.OverlapCircle(verificaChao.position, raioValidaChao, solido);
        estaNaParede = Physics2D.OverlapCircle(verificaParede.position, raioValidaChao, solido);
        atacar = Physics2D.OverlapCircle(campoDeVisao.position, raioValidaPerseguicao, player);

        if (!estaNoChao || estaNaParede && !atacar)
        {
            Flip();
        }
        if (estaNoChao)
        {
            rb.velocity = new Vector2(velocidade, rb.velocity.y);
        }
        if (atacar)
        {
            temp += Time.deltaTime;
            if (Player.transform.position.x < tr.position.x && viradoParaDireita)
            {
                Flip();
            }
            if (Player.transform.position.x > tr.position.x && !viradoParaDireita)
            {
                Flip();
            }
            if (estaNoChao)
            {
                rb.AddForce(new Vector2(0, forceJump));
            }
            

        } 
        
    }
    void Flip()
    {
        viradoParaDireita = !viradoParaDireita;
        tr.localScale = new Vector2(-tr.localScale.x, tr.localScale.y);
        velocidade *= -1;
    }

   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(verificaChao.position, raioValidaChao);
        Gizmos.DrawWireSphere(verificaParede.position, raioValidaParede);

    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Inimigo" )
        {
            Flip();
        }

    }
    }
