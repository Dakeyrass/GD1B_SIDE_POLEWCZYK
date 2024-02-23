using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controles : MonoBehaviour
{   
    [SerializeField] private float speed;
    [SerializeField] private float flotte_speed;
    [SerializeField] private float jump_force;
    [SerializeField] private float health = 3;
    [SerializeField] private Text coin_counter;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D col;
    [SerializeField] private BoxCollider2D atk ;

    private bool Grounded;
    private bool canDJump;
    public bool powerup = false;
    private bool invincible = false;
    private int coin = 0;

    [SerializeField] private LayerMask gzone;

    //UI
    [SerializeField] public Image[] coeur;
    [SerializeField] public Sprite coeur_rempli;
    [SerializeField] public Sprite coeur_vide;
    

    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent <Rigidbody2D>();
        anim = GetComponent <Animator>();
        col = GetComponent <BoxCollider2D>();

        atk.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //courir
        body.velocity = new Vector2(horizontal*speed,body.velocity.y);
        //saut
        if (!flotte() && Input.GetKeyDown(KeyCode.Space) && Grounded == true){
            jump();
        }else if(!flotte() && Input.GetKeyDown(KeyCode.Space) && Grounded == false && canDJump==true){
            djump();
        }
        //pour l'alteration de gravite
        if(flotte() && Input.GetKey(KeyCode.Space)){
            is_en_apensenteur();
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
        anim.SetBool("invincible", invincible);

        //attaque
        if (Input.GetKeyDown(KeyCode.E)){
            Attaque();
        }

        //UI VIE
        foreach(Image img in coeur){
            img.sprite = coeur_vide;
        }
        //equivalent de for i in range de python
        for(int i = 0; i<health; i++){
            coeur[i].sprite = coeur_rempli;
        }
    }
    
    private void Attaque(){
        atk.enabled = true;
        anim.SetTrigger("attaque");
    }

    private void is_en_apensenteur(){
        body.AddForce(Vector2.up * flotte_speed);
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
        //mort du joueur et perte de pv  
        if (collision.gameObject.tag == "Ennemi" && !invincible){
            health-=1;
            Debug.Log(health);
            if (health==0){
                restart();
            }
            else
            {
                invincible = true;
            }
        }
        if (collision.gameObject.tag == "Obstacle" && !invincible){
            health=0;
            Debug.Log(health);
            if (health==0){
                restart();
            }
            else
            {
                invincible = true;
            }
        }
    }
    //pour l'altération de la gravité
    private bool flotte(){
        return Physics2D.OverlapCircle(body.position,1.5f,gzone);
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
        if (other.CompareTag("Coin")){
            coin +=1;
            coin_counter.text = "" + coin;
            Destroy(other.gameObject);
        }
    }

    public void IFrames()
    {
        invincible = false;
    }

    public void attaqueEnd(){
        atk.enabled = false;
    }

    public void restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
