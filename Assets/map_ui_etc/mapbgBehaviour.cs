using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapbgBehaviour : MonoBehaviour
{
    public GameObject cma;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(cma.transform.position.x / 2, cma.transform.position.y / 2 +0.5f, 0);
    }
}
