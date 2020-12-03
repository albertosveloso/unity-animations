using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
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
    void FixedUpdate()
    {
        //Logica do personagem se movimentando
        if(Input.GetKey(KeyCode.LeftArrow))
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

        if(Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.velocity = new Vector2(Speed, rigidBody.velocity.y);

            //Incluindo animação de caminhada
            animator.SetBool("isWalking", true);

            //Rotacionar player para direita
            sprite.flipX = false;
        }
    }
}
