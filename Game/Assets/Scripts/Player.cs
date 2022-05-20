using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float atkCooldown = 0.4f;
    public float energyCooldown = 1f;
    private float lastPressTime;
    private bool isGrounded;
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer sprite;
    public Transform rightPoint;
    public Transform leftPoint;
    public GameObject energy;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();


        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = false;
            rig.velocity = new Vector2(speed, rig.velocity.y);
            anim.SetBool("IsWalking", true);
        }
        else 
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
            anim.SetBool("IsWalking", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sprite.flipX = true;
            rig.velocity = new Vector2(-speed, rig.velocity.y);
            anim.SetBool("IsWalking", true);
        }
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            anim.SetBool("IsJumping", true);
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        
        if (Input.GetKey(KeyCode.Z))
        {
            float currentTime = Time.time;
            float diffSecs = currentTime - lastPressTime;
            if (diffSecs >= atkCooldown)
            {
                lastPressTime = currentTime;
                Debug.Log("Atacou");
                anim.SetTrigger("IsAttacking");
            }
            
            
        }
        if(Input.GetKey(KeyCode.X))
        {
            float currentTime = Time.time;
            float diffSecs = currentTime - lastPressTime;
            if (diffSecs >= energyCooldown)
            {
                AudioController.current.PlayMusic(AudioController.current.sfx);
                if (!sprite.flipX)
                {
                    lastPressTime = currentTime;
                    GameObject bullet = Instantiate(energy);
                    bullet.transform.position = rightPoint.transform.position;
                    Debug.Log("aTIROU");
                }
                else
                {
                    lastPressTime = currentTime;
                    GameObject bullet = Instantiate(energy);
                    bullet.transform.position = leftPoint.transform.position;
                    Debug.Log("aTIROU");
                }
            }

        }
    }
        
        
    

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.layer == 3)
        {
            isGrounded = true;
            anim.SetBool("IsJumping", false);
        }
        if (coll.gameObject.tag == "Enemy")
        {
            anim.Play("Player_Jump", 0, 0f);
            anim.SetBool("IsJumping", true);
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            coll.gameObject.GetComponent<Animator>().SetTrigger("hit");
            coll.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(coll.gameObject, 2f);
            AudioController.current.PlayMusic(AudioController.current.anotherSfx);
        } 
    }

    
}
