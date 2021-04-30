using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    Vector3 cameraposition;
    public GameObject player;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        cameraposition = this.player.transform.position;
        if(player.transform.position.x <9.3f && player.transform.position.x > -9.2f)
            this.transform.position = new Vector3(cameraposition.x / 1.5f, cameraposition.y /2, -10);    
        else if(player.transform.position.x > 9.3f)
            this.transform.position = new Vector3(6.2f, cameraposition.y / 2, -10);
        else if (player.transform.position.x < -6.2f)
            this.transform.position = new Vector3(-6.2f, cameraposition.y / 2, -10);
    }
}
