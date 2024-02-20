using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemis_Behave : MonoBehaviour
{
    private BoxCollider2D bc2d;
    private Rigidbody2D rgbd;
    [SerializeField] private float speed;
    private bool going_right = false;
    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        rgbd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!going_right){
            rgbd.velocity = new Vector2(-speed,rgbd.velocity.y);
        }
        else if (going_right){
            rgbd.velocity = new Vector2(speed,rgbd.velocity.y);
        }
        
    }
    void OnCollisionEnter2D (Collision2D collision){
        if (collision.gameObject.tag == "Wall"){
            if (!going_right){
                going_right = true;
            }
            else if (going_right){
                going_right = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Limite_ennemi")){
            if (!going_right){
                going_right = true;
            }
            else if (going_right){
                going_right = false;
            }  
        }
    }
}
