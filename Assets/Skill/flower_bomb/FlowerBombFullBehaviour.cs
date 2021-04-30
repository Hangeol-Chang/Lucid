using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBombFullBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("deactive", 2.4f);

    }
    void deactive()
    {
        gameObject.SetActive(false);
    }

}
