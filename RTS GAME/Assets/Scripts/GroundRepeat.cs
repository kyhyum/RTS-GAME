using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRepeat : MonoBehaviour
{

    public float speed = 10.0f;
    public float startpos;
    public float endpos;


    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        if(transform.position.y >= endpos)
        {
            Repeat();
        }
    }
   
    void Repeat()
    {
        transform.Translate(0, -1 * (endpos - startpos), 0);
    }
}
