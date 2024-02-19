using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controles : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float jump_force;
    private Rigidbody2D body;
    private Animator anim;
    private bool Grounded;
    private bool canDJump;
    public bool powerup=false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent <Rigidbody2D>();
        anim = GetComponent <Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //courir
        body.velocity = new Vector2(horizontal*speed,body.velocity.y);
        //saut
        if (Input.GetKeyDown(KeyCode.Space) && Grounded == true){
            jump();
        }else if(Input.GetKeyDown(KeyCode.Space) && Grounded == false && canDJump==true){
            djump();
        }
        //flip
        if (horizontal>0){
            transform.localScale = new Vector3(1f,1f);
        } else if (horizontal<0){
            transform.localScale = new Vector3(-1f,1f);
        }

        //animation
        anim.SetBool("run", horizontal !=0);
        anim.SetBool("grounded", Grounded);
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Ground"){
            Grounded=true;
            canDJump=true;
        }
        if (collision.gameObject.tag == "Wall"){
            Grounded=true;
            body.velocity = new Vector2(body.velocity.x,-2);
        }  
    }
   
    private void jump(){
        body.velocity = new Vector2 (body.velocity.x,jump_force);
        Grounded=false;
    }
    private void djump(){
        if (powerup==true){
            body.velocity = new Vector2 (body.velocity.x,jump_force);
            canDJump=false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Power_up")){
            powerup=true;  
        }
    }
}
