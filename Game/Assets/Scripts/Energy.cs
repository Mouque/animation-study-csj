using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float speed;
    private Player player;
    private SpriteRenderer spritePlayer;
    private bool direction;


    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spritePlayer = player.GetComponent<SpriteRenderer>();
        direction = spritePlayer.flipX;

        Destroy(gameObject, 3);
    }


    // Update is called once per frame
    void Update()
    {
        if (!direction)
        {
            transform.Translate(Vector2.right * Time.fixedDeltaTime * speed);
        }
        else
        {
            transform.Translate(Vector2.left * Time.fixedDeltaTime * speed);
        }
    }
}
