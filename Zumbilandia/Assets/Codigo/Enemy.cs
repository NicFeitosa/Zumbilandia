using System.Data;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System.Collections;
using System.Collections.Generic;

public class EnemyAtaque : MonoBehaviour
{
    //Variáveis//
    private Animator animadorEnemy;
    private int movHash = Animator.StringToHash("Walk");
    private int EnemyAtkHS = Animator.StringToHash("Attack");
    private SpriteRenderer flipEnemy;
    [SerializeField]private int Enemylife = 6;
    [SerializeField] private int velocidadeEnemy = 5;
    public Transform[] Rota;
    public int PontoDePartida;
    public BoxCollider2D ataqueEnemy;
    public BoxCollider2D checkAtaque;

    private void Awake()
    {
        animadorEnemy = GetComponent<Animator>();
        flipEnemy = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        transform.position = Rota[PontoDePartida].transform.position;
    }
    void Update()
    {
        if(PontoDePartida == 0)
        {
            flipEnemy.flipX = true;
            ataqueEnemy.offset = new UnityEngine.Vector2(-0.41f, 0.04f);
            checkAtaque.offset = new UnityEngine.Vector2(-0.41f, 0.04f);
        }
        else
        {
            flipEnemy.flipX = false;
            ataqueEnemy.offset = new UnityEngine.Vector2(0.41f, 0.04f);
            checkAtaque.offset = new UnityEngine.Vector2(0.41f, 0.04f);
        }

        if(Enemylife ==0){ MorteEnemy();}
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = UnityEngine.Vector2.MoveTowards(transform.position, Rota[PontoDePartida].transform.position, velocidadeEnemy * Time.deltaTime);
        
        if(ChecarAtaque.checkAtq == true)
        {
            StartCoroutine("Ataque_inimigo");
        }

        if(transform.position == Rota[PontoDePartida].transform.position)
        {
            PontoDePartida += 1;
        }

        if(PontoDePartida == Rota.Length)
        {
            PontoDePartida = 0;
        }

        if(velocidadeEnemy != 0)
        {
            animadorEnemy.SetBool(movHash, true);
        }
        else
        {
            animadorEnemy.SetBool(movHash, false);
        }
    }

    private void MorteEnemy()
    {
        Enemylife = 0;
        animadorEnemy.SetTrigger("Death");
        velocidadeEnemy = 0;
        Destroy(transform.gameObject.GetComponent<BoxCollider2D>());
        Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
        Destroy(ataqueEnemy);
        Destroy(checkAtaque);
        Destroy(this);
    }

    IEnumerator ChecarAtaque()
    {
        animadorEnemy.SetBool(EnemyAtkHS, true);
        velocidadeEnemy = 0;
        yield return new WaitForSeconds(0.85f);
        animadorEnemy.SetBool(EnemyAtkHS, false);
        velocidadeEnemy = 5;

        ChecarAtaque.checkAtq = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerAtaque")
        {
            Enemylife --;

            if(Enemylife < 1)
            {
                StopCoroutine("Attack");
                MorteEnemy();
            }
        }
    }
}