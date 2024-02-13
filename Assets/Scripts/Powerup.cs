using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    OnTriggerEnter2D(Collider2D other){
        if (other.ComparTag("Player")){
            Controles control = player.GetComponent<Controles>();
            control.powerup = true; 

            Destroy(gameObject);  
        }
    }
    
}

