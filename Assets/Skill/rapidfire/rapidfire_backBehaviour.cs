using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rapidfire_backBehaviour : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        this.transform.position = player.transform.position;
        //transform.localScale = new Vector3(player.transform.localScale.x, 1,1);
        if (Input.GetKeyUp(KeyCode.LeftControl)) gameObject.SetActive(false);
    }
}
