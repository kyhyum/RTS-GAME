using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnFunction : MonoBehaviour
{
    
    public void spawn(int n)
    {
        Color color = this.gameObject.GetComponent<Image>().color;
        if (color.a != 1f)
        {
            color.a = 1f;
            this.gameObject.GetComponent<Image>().color = color;
        }
        else
        {
            color.a = 0.8f;
            this.gameObject.GetComponent<Image>().color = color;
        }
         UnitSpawn.instance.spawn(n);
        
    }
}
