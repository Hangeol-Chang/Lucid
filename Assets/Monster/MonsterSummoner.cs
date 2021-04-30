using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSummoner : MonoBehaviour
{
    public GameObject[] monster = new GameObject[34];
    void Start()
    {
        InvokeRepeating("summon", 2f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void summon()
    {
        for (int i = 0; i < 34; i++)
        {
            if (monster[i].activeSelf == false)
            {
                monster[i].gameObject.SetActive(true);
                //this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
