using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerBehaviour : MonoBehaviour
{

    private Rigidbody2D         rb;
    private Transform           tr;
    private Animator            an;

    public Transform            limite1;
    public Transform            limite2;

    private bool                viradoParaDireita;
    public bool                 atacando;

    private float               altura;
    public float                velocidade;

    // Use this for initialization
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        an = GetComponent<Animator>();

        viradoParaDireita = true;
        atacando = false;
        altura = tr.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyMoviment();
    }

    void EnemyMoviment()
    {
        if (!atacando)
        {
            if (limite1.position.x < tr.position.x && viradoParaDireita)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(limite2.position.x, altura), velocidade * Time.deltaTime);
            }
            if (tr.position.x < limite2.position.x && !viradoParaDireita)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(limite1.position.x, altura), velocidade * Time.deltaTime);
            }
            if (limite2.position.x <= tr.position.x && viradoParaDireita)
            {
                Flip();
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(limite1.position.x, altura), velocidade * Time.deltaTime);
            }
            if (tr.position.x <= limite1.position.x && !viradoParaDireita)
            {
                Flip();
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(limite2.position.x, altura), velocidade * Time.deltaTime);
            }
            if (tr.position.y < altura || tr.position.y > altura)
            {
                returnToCourse();
            }
        }
    }
    void Flip()
    {
        viradoParaDireita = !viradoParaDireita;
        tr.localScale = new Vector2(-tr.localScale.x, tr.localScale.y);
    }
    void returnToCourse()
    {
        
        if (tr.position.x <= limite1.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limite1.position.x+0.5f, altura), velocidade * Time.deltaTime);
        }
        if (limite2.position.x <= tr.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limite2.position.x-0.5f, altura), velocidade * Time.deltaTime);
        }
    }
}