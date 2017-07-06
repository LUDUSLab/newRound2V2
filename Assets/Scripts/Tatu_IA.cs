using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatu_IA : MonoBehaviour
{

    public GameObject Player;
    private Rigidbody2D rb;
    private Transform tr;
    private Animator an;
    public Transform verificaChao;
    public Transform verificaParede;
    public Transform campoDeVisao;

    private bool estaNaParede;
    private bool estaNoChao;
    private bool viradoParaDireita;
    private bool atacar;

    public float velocidade;
    public float raioValidaChao;
    public float raioValidaParede;
    public float raioValidaPerseguicao;
    public float stopTemp;
    private float temp;

    public LayerMask solido;
    public LayerMask player;

    // Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        an = GetComponent<Animator>();
        temp = 0;
        viradoParaDireita = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyMoviment();
        Freeze();
        //decidirAnimacao();
    }

    void EnemyMoviment()
    {
        estaNoChao = Physics2D.OverlapCircle(verificaChao.position, raioValidaChao, solido);
        estaNaParede = Physics2D.OverlapCircle(verificaParede.position, raioValidaChao, solido);
        atacar = Physics2D.OverlapCircle(campoDeVisao.position, raioValidaPerseguicao, player);

        if (!estaNoChao || estaNaParede)
        {
            if (!atacar)
            {
                Flip();
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        if (estaNoChao && !atacar)
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
        }
        else
        {
            temp = 0;
        }
        if (estaNoChao && atacar)
        {
            rb.velocity = new Vector2(velocidade * 1.5f, rb.velocity.y);
        }
    }
    void Freeze()
    {
        if (temp < stopTemp && atacar)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if(temp >= stopTemp || !atacar)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }
    }
    void Flip()
    {
        viradoParaDireita = !viradoParaDireita;
        tr.localScale = new Vector2(-tr.localScale.x, tr.localScale.y);
        velocidade *= -1;
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(verificaChao.position, raioValidaChao);
        Gizmos.DrawWireSphere(verificaParede.position, raioValidaParede);
        Gizmos.DrawWireSphere(campoDeVisao.position, raioValidaPerseguicao);

    }

    /*void decidirAnimacao()
    {
        if(atacar && !estaNaParede || estaNoChao)
        {
            an.SetBool("Atacar", true);
            an.SetBool("Andar", false);
            an.SetBool("Parado", false);
        }
        if (!atacar && !estaNaParede || estaNoChao)
        {
            an.SetBool("Atacar", false);
            an.SetBool("Andar", true);
            an.SetBool("Parado", false);
        }
        if (atacar && estaNaParede || !estaNoChao)
        {
            an.SetBool("Atacar", false);
            an.SetBool("Andar", false);
            an.SetBool("Parado", true);
        }
    }*/
}