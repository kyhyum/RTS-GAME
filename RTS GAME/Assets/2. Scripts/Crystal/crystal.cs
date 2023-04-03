using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class crystal : MonoBehaviour
{
    public int now_crystal = 100;
    public static crystal instance = null;
    public TMP_Text tmp;
    Color color;
    bool Lock = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        color = tmp.color;
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = now_crystal.ToString();
        if (tmp.color == Color.red && !Lock)
        {
            StartCoroutine(ColorRollback());
        }
    }

    IEnumerator ColorRollback()
    {
        yield return new WaitForSeconds(1f);
        tmp.color = color;
    }
}