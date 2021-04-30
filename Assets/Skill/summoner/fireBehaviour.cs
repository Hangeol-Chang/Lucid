using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBehaviour : MonoBehaviour
{
    public GameObject summoner;
    public GameObject fire;
    float starttime;
    Animator ani;
    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(Time.time - starttime);
        if (Input.GetKeyUp(KeyCode.Space) || Time.time - starttime > 8f)
        {
            Debug.Log("??");
            ani.SetBool("end", true);
            fire.SetActive(false);
            Invoke("da", 0.2f);

        }
    }
    void da()
    {
        summoner.SetActive(true);
        fire.SetActive(false);
        gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        ani.SetBool("shoot", true);
        Invoke("f", 0.9f);
        starttime = Time.time;
    }
    void f()
    {
        fire.SetActive(true);
        fire.transform.position = new Vector3(-10, 0, 0);
    }
}
