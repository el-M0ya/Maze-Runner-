using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    public float start;
    public float restart;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position , new Vector2(0 , restart) , Time.deltaTime/2);
        if(transform.position.y >= restart) transform.position =   new Vector3(transform.position.x , start);
    }
}
