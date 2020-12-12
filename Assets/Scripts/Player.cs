using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float startTimeAttack; // Tempo da animação
    public Transform point; // ponto a partir de quando a energy é criada
    public GameObject energy;
    public Transform backPoint;

    private bool isAttacking;
    private float timeAttack; // Contador
    private bool isGrounded;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Logica do personagem se movimentando
        if(Input.GetKey(KeyCode.LeftArrow) && !isAttacking)
        {
            rigidBody.velocity = new Vector2(-Speed, rigidBody.velocity.y);

            //Incluindo animação de caminhada
            animator.SetBool("isWalking", true);

            //Rotacionar player para esquerda
            sprite.flipX = true;
        }
        else {
            //Assim que soltar a seta parar ao invés de continuar deslizando
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);

            //Não tem tecla pressionada desativo walking 
            animator.SetBool("isWalking", false);
        }

        if(Input.GetKey(KeyCode.RightArrow) && !isAttacking)
        {
            rigidBody.velocity = new Vector2(Speed, rigidBody.velocity.y);

            //Incluindo animação de caminhada
            animator.SetBool("isWalking", true);

            //Rotacionar player para direita
            sprite.flipX = false;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rigidBody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }

        if(timeAttack <= 0)
        {
            isAttacking = false;

            if(Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetBool("isAttacking", true); // Iniciou animacao
                timeAttack = startTimeAttack; // tempo para colocar no unity
                isAttacking = true;
            }
        }
        else
        {
            timeAttack -= Time.deltaTime; //Decrescer em tempo real
            animator.SetBool("isAttacking", false); //Terminou animacao
        }
        
        animator.SetBool("isAttacking", isAttacking);

        if(Input.GetKeyDown(KeyCode.X)){

            //Variavel local que recebe a bala da cena
            GameObject bullet = Instantiate(energy);

            //Chamando audio
            AudioController.current.PlayMusic(AudioController.current.sfx);

            //Player está para esquerda
            if(sprite.flipX)
            {
                //Chama o point de trás
                bullet.transform.position = backPoint.transform.position;
            }else
            {
                bullet.transform.position = point.transform.position;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {

        //Personagem esta tocando o chao
        if(coll.gameObject.layer == 8)
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }

        //Inimigo
        if(coll.gameObject.tag == "Enemy"){
            coll.gameObject.GetComponent<Animator>().SetTrigger("hit");
            Destroy(coll.gameObject, 1f);
            rigidBody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

            //Chamando audio
            AudioController.current.PlayMusic(AudioController.current.anotherSfx);
        }
        
    }

}
