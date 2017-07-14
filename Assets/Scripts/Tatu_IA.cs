using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatu_IA : MonoBehaviour
{

    public GameObject Player;

    private Rigidbody2D rb;

    private Animator an;

    private Transform tr;
    public Transform verificaChao;
    public Transform verificaParede;
    public Transform campoDeVisao;

    private bool estaNaParede;
    private bool estaNoChao;
    private bool atacar;
    private bool rolando;
    private bool bateu;
    private bool stunado;
	private bool viradoParaDireita;

    public float raioValidaChao;
    public float raioValidaParede;
    public float raioValidaPerseguicao;
    public float velocidade;
    public float stopTemp;
	public float timeStun;
	private float stun;
    private float temp;

    public LayerMask solido;
    public LayerMask player;

    // Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        an = GetComponent<Animator>();
        viradoParaDireita = true;
        rolando = false;
        stunado = false;
        bateu = false;
        temp = 0;
		stun = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyMoviment();
        //decidirAnimacao();
    }

    void EnemyMoviment()
    {
        estaNoChao = Physics2D.OverlapCircle(verificaChao.position, raioValidaChao, solido);
        estaNaParede = Physics2D.OverlapCircle(verificaParede.position, raioValidaChao, solido);
        atacar = Physics2D.OverlapCircle(campoDeVisao.position, raioValidaPerseguicao, player);
        EnemyTimers();
        if (!atacar && !rolando)
        {
            NormalMoviment();
            AvoidWall();
        }
        else
        {
            AttackPattern();
        }
    }

    void EnemyTimers()
    {
        if (!bateu) 
		{
            if (rolando)
            {
                stun = 0;
                if (temp <= stopTemp)
                {
                    temp += Time.deltaTime;
                }
                Prepare();
            }
		} else 
		{
            temp = 0;
            rolando = false;
            if (estaNaParede)
            {
                if (stun <= timeStun)
                {
                    stun += Time.deltaTime;
                }
                else
                {
                    bateu = false;
                }
                Stunned();
            }
            else
            {
                Prepare();
            }
        }

    }

    void AttackPattern()
    {
        if (Player.transform.position.x < rb.position.x && viradoParaDireita)
        {
            if (!rolando && !stunado)
            {
                Flip();
            }
            rolando = true;
        }
        else if (Player.transform.position.x > rb.position.x && !viradoParaDireita)
        {
            if (!rolando && !stunado)
            {
                Flip();
            }
            rolando = true;
        }
        else
        {
            rolando = true;
        }

        if (estaNoChao && rolando)
        {
            rb.velocity = new Vector2(velocidade * 2, rb.velocity.y);
        }
        NormalMoviment();
        AvoidWall();
    }

    void NormalMoviment()
    {
        if (!estaNoChao && !bateu)
        {
            Flip();
        }
        if(estaNoChao && !rolando)
        {
            rb.velocity = new Vector2(velocidade, rb.velocity.y);
        }
        if(!estaNoChao && rolando)
        { 
            bateu = true;
            rolando = false;
        }
    }

    void AvoidWall()
    {
        if (estaNaParede)
        {
            if (!rolando && !bateu)
            {
                Flip();
            }
            else
            {
                bateu = true;
                rolando = false;
            }
        }
    }

    void Stunned()
    {
        if (stun < timeStun && !stunado)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            tr.localScale = new Vector3(-tr.localScale.x, -tr.localScale.y);
            viradoParaDireita = !viradoParaDireita;
            stunado = true;
        }
        if(stun >= timeStun && stunado)
        {
            tr.localScale = new Vector2(tr.localScale.x, -tr.localScale.y);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            velocidade *= -1;
            stunado = false;
        }
    }

    void Prepare()
    {
		if (temp < stopTemp && rolando)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
		if (temp >= stopTemp) 
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(verificaChao.position, raioValidaChao);
        Gizmos.DrawWireSphere(verificaParede.position, raioValidaParede);
        Gizmos.DrawWireSphere(campoDeVisao.position, raioValidaPerseguicao);

    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }*/


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