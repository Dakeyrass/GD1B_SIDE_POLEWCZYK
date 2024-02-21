using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    [SerializeField] public float follow_speed = 1f;
    [SerializeField] public float y_offset = -1f;
    [SerializeField] public Transform target; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + y_offset,-10f);
        //lerp utilise les coordonnees du debut et de fin afin de faire un truc smooth
        transform.position = Vector3.Slerp(transform.position, newPos, follow_speed*Time.deltaTime);
    }
}
