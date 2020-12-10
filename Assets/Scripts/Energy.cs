using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float Speed;
    private Player player;
    private SpriteRenderer spritePlayer;

    private bool direction;

    // Start is called before the first frame update
    void Start()
    {
        //Encontra o personagem e iniciar script
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //Sprite do player
        spritePlayer = player.GetComponent<SpriteRenderer>();

        //Direcao da bola
        direction = spritePlayer.flipX;

        Destroy(gameObject, 3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(direction)
        {
            transform.Translate(Vector2.left * Time.deltaTime * Speed);
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * Speed);
        }
        
    }
}
