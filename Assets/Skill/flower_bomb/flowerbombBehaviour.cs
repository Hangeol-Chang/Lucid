using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerbombBehaviour : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision) //1타 판정
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) //2타 판정
    {

    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("da", 2f);
    }
    void da()
    {
        gameObject.SetActive(false);
    }
}
