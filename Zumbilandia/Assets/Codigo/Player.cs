using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    //Variáveis//

//Var de movimentação do player//
    private Rigidbody2D rb;
    private float InputHorizontal;
    [SerializeField] private int Velocidade = 5;
    [SerializeField] private Transform PePlayer;
    [SerializeField] private LayerMask LayerChao;
    private bool TocandoChao;
    private SpriteRenderer Flip;

//Var de combate//
    [SerializeField]private int Damage;
    


//---------------------------------------------------------------------------------------------------//
    //Variáveis + Hash de animação do player//
    private Animator animador;
    private int MovendoHash = Animator.StringToHash("Movendo");
    private int PulandoHash = Animator.StringToHash("Pulando");
    private int CaindoHash = Animator.StringToHash("Caindo");

//---------------------------------------------------------------------------------------------------//
    private void Awake()    //Busca os componentes e guarda em uma variável//
    {
        rb = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        Flip = GetComponent<SpriteRenderer>();
    }

//---------------------------------------------------------------------------------------------------//

    void Update() //Executa as ações//
    {
        InputHorizontal = Input.GetAxisRaw("Horizontal"); //Detecta a tecla pressionada e guarda o valor na variável//


        //Verifica se o player está andando para "frente" ou para "atrás" e direciona o personagem na direção q está andando//
        if(InputHorizontal > 0)
        {
            Flip.flipX = false;
        } 
        else if(InputHorizontal <0)
        {
            Flip.flipX = true;
        }

        animador.SetBool(MovendoHash, InputHorizontal != 0);//Verifica se o player está andando e ativa a animação//

//---------------------------------------------------------------------------------------------------//

        TocandoChao = Physics2D.OverlapCircle(PePlayer.position, 0.2f, LayerChao); //Verifica se o player está tocando o chão

        
        //Verifica se a tecla de pular foi acionada e se o player está tocando o chão, caso seja verdadeiro, o personagem irá pular//
        if(Input.GetKeyDown(KeyCode.Space) && TocandoChao)
        {
            rb.AddForce(Vector2.up * 300);
        }        
        animador.SetBool(PulandoHash, !TocandoChao); //Ativa a animação de pular
        
        //Verifica se o player está caindo, caso seja verdade, irá executar a animação de estar caindo//
        if(rb.linearVelocity.y < 0)
        {
            animador.SetBool(CaindoHash, true);
        }
        else animador.SetBool(CaindoHash, false); //Caso pare de cair,  desativa a animação//


    }

//---------------------------------------------------------------------------------------------------//
    private void FixedUpdate()
    {
        //Deterimina a velocidade do player//
        rb.linearVelocity = new Vector2(InputHorizontal * Velocidade, rb.linearVelocity.y);
    }
}